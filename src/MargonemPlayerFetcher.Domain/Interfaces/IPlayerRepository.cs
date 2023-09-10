using MargoFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        public Task InsertPlayer(Player player);
        public Task<IEnumerable<Player>> GetAllPlayersByServer(string server);
        public Task<IEnumerable<Server>> GetServers();
        public Task<int> GetTotalPlayerCount();
        public Task<int> GetDuplicatedPlayersCount();
        public Task UpdatePlayerLevel(Player player);
        public Task UpdatePlayerName(Player player);
        public Task UpdatePlayerFetchDate(Player player);
        public Task<bool> CheckIfPlayerExists(Player player);
    }
}
