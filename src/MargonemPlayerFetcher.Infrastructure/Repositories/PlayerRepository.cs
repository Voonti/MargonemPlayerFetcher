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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MargoDbContext _margoDbContext;
        public PlayerRepository(MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _margoDbContext.Players.AsNoTracking().ToListAsync();
        }

        public async Task<bool> InsertPlayer(Player player)
        {
            await _margoDbContext.AddAsync(player);
            return await _margoDbContext.SaveChangesAsync() > 0;
        }
    }
}
