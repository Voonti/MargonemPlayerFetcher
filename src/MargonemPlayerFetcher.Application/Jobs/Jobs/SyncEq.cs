using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MargoFetcher.Domain.Enums;
using Hangfire;
using System.ComponentModel;
using Hangfire.States;

namespace MargoFetcher.Application.Jobs.Jobs
{
    public class SyncEq
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGarmoryApiService _garmoryApiService;
        public SyncEq(
            IItemRepository itemRepository,
            IPlayerRepository playerRepository,
            IGarmoryApiService garmoryApiService)
        {
            _itemRepository = itemRepository;
            _playerRepository = playerRepository;
            _garmoryApiService = garmoryApiService;
        }

        //[AutomaticRetry(Attempts = 1)]
        [JobDisplayName("{0}")]
        public async Task Execute(string server)
        {
            var players = (await _playerRepository.GetAllPlayersByServer(server));

            int maxCounter = players.Count();
            int conuter = 0;

            //var tasks = new List<Task>();
            foreach (var player in players)
            {
                await FetchAndSync(player, conuter, maxCounter);
                //var task = FetchAndSync(player, conuter, maxCounter);
                //tasks.Add(task);
                //conuter++;
            }

            //await Task.WhenAll(tasks);
        }

        private async Task FetchAndSync(Player player, int counter, int maxCounter)
        {
            var usersEquipment = await _garmoryApiService.FetchPlayerItems(player);

            Console.WriteLine($"[{counter}]/[{maxCounter}]");
            if (usersEquipment == null
                || !usersEquipment.Any())
                return;

            foreach (var item in usersEquipment.Values)
            {
                var rarity = getItemRarity(item);

                if (item.st == 10 || item.st == 9)
                    continue;

                if (rarity == (char)RarityEnum.Normal)
                    continue;

                if (!isEventItem(item))
                    continue;

                await AssignData(item, rarity, player.userId, player.charId);

                var itemExists = await _itemRepository.CheckIfItemExist(player.userId, player.charId, item.hid);

                ItemStrategy itemStrategy = itemExists ? new PresentItem(_itemRepository) : new NotPresentItem(_itemRepository);
                await itemStrategy.HandleItem(item);
            }
        }

        private bool isLegendItem(Item item)
        {
            var index = item.stat.IndexOf("rarity=");
            if (item.stat[index + 7] == 'l')
                return true;
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

            if (GlobalParameters.EVENT_REGEX.IsMatch(description)
                || GlobalParameters.LICYTY_REGEX.IsMatch(description))
            {
                return true;
            }
            else
            {
                return false;
            }
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
