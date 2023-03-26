using MargonemPlayerFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        public Task<bool> InsertPlayer(Player player);
        public Task<IEnumerable<Player>> GetAllPlayers();
    }
}
