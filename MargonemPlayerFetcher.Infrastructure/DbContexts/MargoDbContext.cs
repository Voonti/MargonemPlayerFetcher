﻿using MargonemPlayerFetcher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Infrastructure.DbContexts
{
    public class MargoDbContext : DbContext
    {
        public MargoDbContext(DbContextOptions<MargoDbContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
