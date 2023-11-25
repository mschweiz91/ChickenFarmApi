using ChickenFarmApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChickenFarmApi.DataAccess
{
    public class EggLayingContext : DbContext
    {
        private string DBPath {  get; set; }

        public DbSet <Chicken> Chickens { get; set; }

        public DbSet<EggLayingRecord> EggLayingRecords { get; set; }

        public EggLayingContext() 
        {
            DBPath = Path
               .Join(Environment
               .GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ChickenFarmApi.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={DBPath}");
        }

    }
}
