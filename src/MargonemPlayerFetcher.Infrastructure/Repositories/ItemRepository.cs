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

        public ItemRepository(MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
        }

        public async Task<bool> CheckIfItemExist(string hid)
        {
            return await _margoDbContext.Items.AnyAsync(x => x.hid == hid);
        }

        public async Task<Item> GetItemsByHid(string hid)
        {
            return await _margoDbContext.Items.FirstOrDefaultAsync(x => x.hid == hid);
        }

        public async Task<bool> InsertItems(IEnumerable<Item> items)
        {
            await _margoDbContext.Items.AddRangeAsync(items);
            return await _margoDbContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateFetchDate(string hid, int charId, DateTime updateDate)
        {
            var entity = await _margoDbContext.Items.FirstAsync(x =>
                x.hid == hid &&
                x.charId == charId);

            entity.lastFetchDate = DateTime.Now;
            await _margoDbContext.SaveChangesAsync();
        }
    }
}
