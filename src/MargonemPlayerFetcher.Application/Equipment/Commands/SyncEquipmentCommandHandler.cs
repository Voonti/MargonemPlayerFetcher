using Common;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Enums;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Equipment.Commands
{
    public record SyncEquipmentCommand() : IRequest<Unit>;
    public class SyncEquipmentCommandHandler : IRequestHandler<SyncEquipmentCommand>
    {
        private readonly IGarmoryApiService _garmoryApiService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPrinter _printer;

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private int totalPlayersToHandle = 0;
        public SyncEquipmentCommandHandler(
            IGarmoryApiService garmoryApiService,
            IPlayerRepository playerRepository,
            IItemRepository itemRepository,
            IPrinter printer)
        {
            _garmoryApiService = garmoryApiService;
            _playerRepository = playerRepository;
            _itemRepository = itemRepository;
            _printer = printer;

        }

        public async Task<Unit> Handle(SyncEquipmentCommand request, CancellationToken cancellationToken)
        {
            _printer.PrintStart(nameof(SyncEquipmentCommandHandler));

            totalPlayersToHandle = await _playerRepository.GetTotalPlayerCount();

            var servers = await _playerRepository.GetServers();

            var tasks = new List<Task>();
            foreach (var server in servers)
            {
                _printer.PrintServer(server.ServerName);

                var players = (await _playerRepository.GetAllPlayersByServer(server.ServerName));

                foreach (var player in players)
                {
                    var task = Task.Run(async () =>
                    {
                        await semaphore.WaitAsync();
                        try
                        {
                            await FetchAndSync(player);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });

                    tasks.Add(task);
                    //lp++;
                    //await FetchAndSync(player);
                    //Console.WriteLine(lp);
                }

            }
            await Task.WhenAll(tasks);
            return Unit.Value;
        }

        private async Task FetchAndSync(Player player)
        {
            var usersEquipment = await _garmoryApiService.FetchPlayerItems(player);

            if (usersEquipment == null
                || !usersEquipment.Any())
                return;

            foreach (var item in usersEquipment.Values)
            {
                var rarity = getItemRarity(item);

                var validationResult = Validate(item);

                if (!validationResult)
                    continue;

                await AssignData(item, rarity, player.userId, player.charId);

                var itemExists = await _itemRepository.CheckIfItemExist(player.userId, player.charId, item.hid);

                if (itemExists)
                    await _itemRepository.UpdateFetchDate(item);
                else
                    await _itemRepository.InsertItem(item);
            }
        }

        private bool Validate(Item item)
        {
            var rarity = getItemRarity(item);

            if (item.st == 10 || item.st == 9)
                return false;

            if (rarity == (char)RarityEnum.Common)
                return false;

            if (rarity == (char)RarityEnum.Unique)
                return false;

            if (rarity == (char)RarityEnum.Heroic)
                return false;

            if (rarity == (char)RarityEnum.Legend)
                return true;

            if (rarity == (char)RarityEnum.Artifact)
                return true;

            if (isEventItem(item))
                return true;

            _printer.PrintUnknownItemType(item);
            return false;
        }

        private char getItemRarity(Item item)
        {
            var index = item.stat.IndexOf("rarity=");
            return item.stat[index + 7];
        }

        private bool isEventItem(Item item)
        {
            var startIndex = item.stat.IndexOf("opis=");
            if (startIndex == -1)
                return false;


            int endIndex = item.stat.IndexOf(";", startIndex);
            var description = item.stat.Substring(startIndex + 5, endIndex - startIndex);

            return GlobalParameters.EVENT_REGEX.IsMatch(description)
                || GlobalParameters.LICYTY_REGEX.IsMatch(description);
        }

        private async Task AssignData(Item item, char rarity,
            int userId, int charId)
        {
            item.rarity = rarity;
            item.lastFetchDate = DateTime.Now;
            item.fetchDate = DateTime.Now;
            item.userId = userId;
            item.charId = charId;
        }
    }
}
