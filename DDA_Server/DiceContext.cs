using Microsoft.EntityFrameworkCore;
using Dice.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Dice.Context
{
    public class DiceContext : DbContext
    {
        public DbSet<Dice.Entities.DiceSpread> DiceSpread { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection") + ";Username=" + Environment.GetEnvironmentVariable("DB_USERNAME");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}