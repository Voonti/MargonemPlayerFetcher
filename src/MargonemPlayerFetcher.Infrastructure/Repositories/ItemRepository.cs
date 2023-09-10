using MargoFetcher.Domain.DTO;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly MargoDbContext _margoDbContext;

        public ItemRepository(
            MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
        }

        public async Task<bool> CheckIfItemExist(Item item)
        {
            var existingItem = await GetItem(item);


            if (existingItem == null)
                return false;
            else
                return true;
        }

        public async Task<int> GetDuplicatedItemsCount()
        {
            return await _margoDbContext.Items
                .GroupBy(m => new { m.PlayerId, m.Hid, m.Name })
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .CountAsync();
        }

        public async Task InsertItem(Item item)
        {
            await _margoDbContext.Items.AddAsync(item);
            await _margoDbContext.SaveChangesAsync();
        }

        public async Task UpdateFetchDate(Item item)
        {
            var existingItem = await GetItem(item);

            existingItem!.LastFetchDate = item.FetchDate;
            await _margoDbContext.SaveChangesAsync();
        }

        private async Task<Item?> GetItem(Item item)
        {
            return await _margoDbContext.Items.FirstOrDefaultAsync(x =>
                x.PlayerId == item.PlayerId &&
                x.Hid == item.Hid);
        }
    }
}
