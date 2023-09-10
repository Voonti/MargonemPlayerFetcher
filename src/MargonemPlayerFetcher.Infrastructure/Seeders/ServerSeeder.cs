using MargoFetcher.Domain.Entities;
using MargoFetcher.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Infrastructure.Seeders
{
    public class ServerSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            string[] serverNames = new string[]
            {
                "classic", "aether", "aldous", "berufs", "brutal",
                "fobos", "gefion", "hutena", "jaruna", "katahha",
                "lelwani", "majuna", "nomada", "perkun", "tarhuna",
                "telawel", "tempest", "zemyna", "zorza"
            };

            int id = 1;

            foreach (var serverName in serverNames)
            {
                modelBuilder.Entity<Server>().HasData(new Server { Id = id, ServerName = serverName });
                id++;
            }

        }
    }
}