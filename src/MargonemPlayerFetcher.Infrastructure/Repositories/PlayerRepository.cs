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

        public async Task<bool> CheckIfPlayerExist(Player player)
        {
            return await _margoDbContext.Players.AnyAsync(x =>
                x.userId == player.userId &&
                x.charId == player.charId);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersByServer(string server)
        {
            return await _margoDbContext.Players
                .AsNoTracking()
                .Where(x => x.server == server)
                .ToListAsync();
        }

        public async Task<IEnumerable<Server>> GetServers()
        {
            return await _margoDbContext.Servers.ToListAsync();
        }

        public async Task<bool> HasPlayerLevelChanged(Player player)
        {
            var currentPlayer = await _margoDbContext.Players.AsNoTracking().FirstAsync(x =>
                x.userId == player.userId &&
                x.charId == player.charId);
            return currentPlayer.level != player.level;
        }

        public async Task InsertPlayer(Player player)
        {
            await _margoDbContext.AddAsync(player);
            await _margoDbContext.SaveChangesAsync() ;
        }
        public async Task<bool> UpdatePlayersLevel(Player player)
        {
            var entity = await _margoDbContext.Players.FirstAsync(x =>
                x.userId == player.userId &&
                x.charId == player.charId);

            entity.level = player.level;
            return await _margoDbContext.SaveChangesAsync() > 0;
        }
    }
}
