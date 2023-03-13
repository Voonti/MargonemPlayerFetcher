using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MargonemPlayerFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly MargoDbContext _margoDbContext;

        public ItemRepository(MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
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
    }
}
