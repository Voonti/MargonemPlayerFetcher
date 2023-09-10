using MargoFetcher.Domain.Entities;
using MargoFetcher.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace MargoFetcher.Infrastructure.DbContexts
{
    public class MargoDbContext : DbContext
    {
        //public MargoDbContext()
        //{

        //}
        public MargoDbContext(DbContextOptions<MargoDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<PlayerLevel> PlayersLevels { get; set; }
        public virtual DbSet<PlayerNick> PlayersNick { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerLevelConfiguration());
            modelBuilder.ApplyConfiguration(new ServerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerNameConfiguration());

            ServerSeeder.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MargoData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    }
        //}
    }

}
