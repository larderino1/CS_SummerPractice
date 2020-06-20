using System;
using System.Collections.Generic;
using System.Text;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbManager
{
    public class AzureSqlDbContext : DbContext
    {
        private readonly string ConnectionString;

        public DbSet<Category> Categories { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AzureSqlDbContext(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("SqlDbConnectionString");
        }

        public AzureSqlDbContext()
        {
            ConnectionString = Environment.GetEnvironmentVariable("SqlDbConnectionString");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
