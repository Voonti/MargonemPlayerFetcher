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

        public async Task<bool> CheckIfItemExist(int userId, int charId, string hid)
        {
            return await _margoDbContext.Items.AnyAsync(x => 
                x.userId == userId
                && x.charId == charId
                && x.hid == hid);
        }

        public async Task<Item> GetItemsByHid(string hid)
        {
            return await _margoDbContext.Items.FirstOrDefaultAsync(x => x.hid == hid);
        }

        public async Task InsertItem(Item item)
        {
            await _margoDbContext.Items.AddAsync(item);
            await _margoDbContext.SaveChangesAsync();
        }

        public async Task UpdateFetchDate(string hid, int charId)
        {
            var entity = await _margoDbContext.Items.FirstAsync(x =>
                x.hid == hid &&
                x.charId == charId);

            entity.lastFetchDate = DateTime.Now;
            await _margoDbContext.SaveChangesAsync();
        }
    }
}
