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
        private readonly IPrinter _printer;

        public ItemRepository(
            MargoDbContext margoDbContext,
            IPrinter printer)

        {
            _margoDbContext = margoDbContext;
            _printer = printer;
        }

        public async Task<bool> CheckIfItemExist(int userId, int charId, string hid)
        {
            return await _margoDbContext.Items.AnyAsync(x => 
                x.userId == userId
                && x.charId == charId
                && x.hid == hid);
        }

        public async Task<int> GetDuplicatedItemsCount()
        {
            return await _margoDbContext.Items
                .GroupBy(m => new { m.userId, m.charId, m.hid, m.name })
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .CountAsync();
        }

        public async Task InsertItem(Item item)
        {
            _printer.PrintNewItem(item);
            await _margoDbContext.Items.AddAsync(item);
            await _margoDbContext.SaveChangesAsync();
        }

        public async Task UpdateFetchDate(Item item)
        {
            _printer.PrintUpdatedItemFetchDate(item);

            var entity = await _margoDbContext.Items.FirstAsync(x =>
                x.hid == item.hid &&
                x.charId == item.charId &&
                x.userId == item.userId);

            entity.lastFetchDate = DateTime.Now;
            await _margoDbContext.SaveChangesAsync();
        }
    }
}
