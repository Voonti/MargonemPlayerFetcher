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
    public record SyncEquipmentCommand(Player player, int countLeft) : IRequest<Unit>;
    public class SyncEquipmentCommandHandler : IRequestHandler<SyncEquipmentCommand>
    {
        private readonly IGarmoryApiService _garmoryApiService;
        private readonly IItemRepository _itemRepository;
        private readonly IPrinter _printer;

        public SyncEquipmentCommandHandler(
            IGarmoryApiService garmoryApiService,
            IItemRepository itemRepository,
            IPrinter printer)
        {
            _garmoryApiService = garmoryApiService;
            _itemRepository = itemRepository;
            _printer = printer;

        }

        public async Task<Unit> Handle(SyncEquipmentCommand request, CancellationToken cancellationToken)
        {
            await FetchAndSync(request.player, request.countLeft);
            return Unit.Value;
        }

        private async Task FetchAndSync(Player player, int currentPlayerIndex)
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

                await AssignData(item, player, rarity);

                var itemExists = await _itemRepository.CheckIfItemExist(item);
                try
                {
                    if (itemExists)
                    {
                        _printer.PrintUpdatedItemFetchDate(currentPlayerIndex, item);
                        await _itemRepository.UpdateFetchDate(item);
                    }
                    else
                    {
                        _printer.PrintNewItem(currentPlayerIndex, item);
                        await _itemRepository.InsertItem(item);
                    }

                }
                catch (Exception ex)
                {
                    _printer.PrintException(ex.Message);
                    throw;
                }
            }
        }

        private bool Validate(Item item)
        {
            var rarity = getItemRarity(item);

            if (item.St == 10 || item.St == 9)
                return false;

            if (rarity == (char)RarityEnum.Common)
                return false;

            if (rarity == (char)RarityEnum.Unique)
                return false;

            if (isEventItem(item))
                return true;

            if (rarity == (char)RarityEnum.Heroic)
                return false;

            if (rarity == (char)RarityEnum.Legend)
                return true;

            if (rarity == (char)RarityEnum.Artifact)
                return true;

            _printer.PrintUnknownItemType(item);
            return false;
        }

        private char getItemRarity(Item item)
        {
            var index = item.Stat.IndexOf("rarity=");
            return item.Stat[index + 7];
        }

        private bool isEventItem(Item item)
        {
            var startIndex = item.Stat.IndexOf("opis=");
            if (startIndex == -1)
                return false;


            int endIndex = item.Stat.IndexOf(";", startIndex);
            var description = item.Stat.Substring(startIndex + 5, endIndex - startIndex);

            return GlobalParameters.EVENT_REGEX.IsMatch(description)
                || GlobalParameters.LICYTY_REGEX.IsMatch(description);
        }

        private async Task AssignData(Item item, Player player, char rarity)
        {
            item.PlayerId = player.Id;
            item.Rarity = rarity;
            item.LastFetchDate = DateTime.Now;
            item.FetchDate = DateTime.Now;
        }
    }
}
