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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MargoDbContext _margoDbContext;
        public PlayerRepository(MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersByServer(string server)
        {
            return await _margoDbContext.Players
                .AsNoTracking()
                .Where(x => x.server == server)
                .ToListAsync();
        }

        public async Task<int> GetDuplicatedPlayersCount()
        {
            return await _margoDbContext.Players
                .GroupBy(m => new { m.userId, m.charId, m.server })
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .CountAsync();
        }

        public async Task<IEnumerable<Server>> GetServers()
        {
            return await _margoDbContext.Servers.ToListAsync();
        }

        public async Task<int> GetTotalPlayerCount()
        {
            return await _margoDbContext.Players.CountAsync();
        }

        public async Task InsertPlayerIfNotExist(Player player)
        {
            var currentPlayer = await _margoDbContext.Players.AnyAsync(x =>
                x.userId == player.userId &&
                x.charId == player.charId &&
                x.server == player.server);

            if(!currentPlayer)
            {
                await _margoDbContext.AddAsync(player);
                await _margoDbContext.SaveChangesAsync();
            }
        }
        public async Task UpdatePlayersLevel(Player player)
        {
            var entity = await _margoDbContext.Players.FirstAsync(x =>
                x.userId == player.userId &&
                x.charId == player.charId &&
                x.server == player.server);

            if (entity.level != player.level)
            {
                entity.level = player.level;
                await _margoDbContext.SaveChangesAsync();
            }
        }
    }
}
