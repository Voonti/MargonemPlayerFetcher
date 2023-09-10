using MargoFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Interfaces
{
    public interface IGarmoryApiService
    {
        public Task<Dictionary<string, Item>> FetchPlayerItems(Player player);
        public Task<IEnumerable<Player>> FetchPlayers(string server);
    }
}
