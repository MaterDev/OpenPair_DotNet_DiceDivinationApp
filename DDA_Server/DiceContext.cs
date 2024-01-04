using Microsoft.EntityFrameworkCore;
using Dice.Entities;

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
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }

}
