using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Jobs.Jobs
{
    public class SyncPlayer
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGarmoryApiService _garmoryApiService;

        public SyncPlayer(
            IPlayerRepository playerRepository,
            IGarmoryApiService garmoryApiService)
        {
            _playerRepository = playerRepository;
            _garmoryApiService = garmoryApiService;
        }

        public async Task Execute()
        {
            var servers = await _playerRepository.GetServers();

            foreach (var server in servers)
            {
                var players = (await _garmoryApiService.FetchPlayers(server.ServerName));

                //var saveTasks = new List<Task>();
                foreach (var player in players)
                {
                    await SavePlayer(player);
                    //var task = SavePlayer(player);
                    //saveTasks.Add(task);
                }

                //await Task.WhenAll(saveTasks);
            }
        }

        private async Task SavePlayer(Player player)
        {
            if (!await _playerRepository.CheckIfPlayerExist(player))
            {
                await _playerRepository.InsertPlayer(player);
            }
            else if (await _playerRepository.HasPlayerLevelChanged(player))
            {
                await _playerRepository.UpdatePlayersLevel(player);
            }
        }
    }
}
