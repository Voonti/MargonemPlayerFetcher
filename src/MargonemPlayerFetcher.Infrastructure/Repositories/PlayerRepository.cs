using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MargoFetcher.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MargoDbContext _margoDbContext;
        public PlayerRepository(MargoDbContext margoDbContext)
        {
            _margoDbContext = margoDbContext;
        }

        public async Task<bool> CheckIfPlayerExists(Player player)
        {
            var existingPlayer = await GetPlayer(player);

            if (existingPlayer == null)
                return false;
            else
                return true;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersByServer(string server)
        {
            return await _margoDbContext.Players
                .AsNoTracking()
                .Where(x => x.Server == server)
                .ToListAsync();
        }

        public async Task<int> GetDuplicatedPlayersCount()
        {
            return await _margoDbContext.Players
                .GroupBy(m => new { m.UserId, m.CharId, m.Server })
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .CountAsync();
        }

        public async Task<IEnumerable<Server>> GetServers()
        {
            return await _margoDbContext.Servers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetTotalPlayerCount()
        {
            return await _margoDbContext.Players.CountAsync();
        }

        public async Task InsertPlayer(Player player)
        {
            using (var dbContextTransaction = await _margoDbContext.Database.BeginTransactionAsync())
            {
                await _margoDbContext.AddAsync(player);
                await _margoDbContext.SaveChangesAsync();

                var playerLevel = new PlayerLevel()
                {
                    PlayerId = player.Id,
                    Level = player.Level,
                    FetchDate = player.FirstFetchDate
                };
                await _margoDbContext.AddAsync(playerLevel);

                var playerName = new PlayerNick()
                {
                    PlayerId = player.Id,
                    Nick = player.Nick,
                    FetchDate = player.FirstFetchDate
                };
                await _margoDbContext.AddAsync(playerName);

                await _margoDbContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
            }
        }

        public async Task UpdatePlayerFetchDate(Player player)
        {
            var entity = await GetPlayer(player);

            if (entity is null)
                return;

            entity.LastFetchDate = player.LastFetchDate;
            await _margoDbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerLevel(Player player)
        {
            var entity = await GetPlayer(player);

            if (entity is null)
                return;

            if (player.Level == entity.Level)
                return;

            entity.Level = player.Level;

            if (player.Level > 300)
            {
                var playerLevel = new PlayerLevel()
                {
                    PlayerId = entity.Id,
                    Level = player.Level,
                    FetchDate = player.FirstFetchDate
                };

                await _margoDbContext.AddAsync(playerLevel);
            }

            await _margoDbContext.SaveChangesAsync();
        }

        public async Task UpdatePlayerName(Player player)
        {
            var entity = await GetPlayer(player);

            if (entity is null)
                return;

            if (player.Nick == entity.Nick)
                return;

            entity.Nick = player.Nick;

            var playerName = new PlayerNick()
            {
                PlayerId = entity.Id,
                Nick = player.Nick,
                FetchDate = player.FirstFetchDate
            };

            await _margoDbContext.AddAsync(playerName);
            await _margoDbContext.SaveChangesAsync();
        }

        private async Task<Player?> GetPlayer(Player player)
        {
            return await _margoDbContext.Players.FirstOrDefaultAsync(x =>
                x.UserId == player.UserId &&
                x.CharId == player.CharId &&
                x.Server == player.Server);
        }
    }
}
