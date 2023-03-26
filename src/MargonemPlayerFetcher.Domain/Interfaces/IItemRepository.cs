using MargonemPlayerFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Interfaces
{
    public interface IItemRepository
    {
        public Task<bool> InsertItems(IEnumerable<Item> items);

        public Task UpdateFetchDate(string hid, string charId, DateTime updateDate);
        public Task<Item> GetItemsByHid(string hid);

        public Task<bool> CheckIfItemExist(string hid);
    }
}
