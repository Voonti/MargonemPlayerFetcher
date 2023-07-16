using MargoFetcher.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Interfaces
{
    public interface IItemRepository
    {
        public Task InsertItem(Item item);
        public Task UpdateFetchDate(Item item);
        public Task<bool> CheckIfItemExist(int userId, int charId, string hid);
        public Task<int> GetDuplicatedItemsCount();
    }
}
