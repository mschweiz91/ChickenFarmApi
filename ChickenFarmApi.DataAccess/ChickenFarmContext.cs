using ChickenFarmApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;

namespace ChickenFarmApi.DataAccess
{
    public class ChickenFarmContext : DbContext
    {
        private string DBPath { get; set; }

        public DbSet<Chicken> Chickens { get; set; }

        public DbSet<EggLayingRecord> EggLayingRecords { get; set; }
       
        public ChickenFarmContext()
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EggLayingRecord>()
                .HasOne(e => e.Chicken)
                .WithMany(c => c.EggLayingRecords)
                .HasForeignKey(e => e.ChickenId)
                .OnDelete(DeleteBehavior.Cascade);
               

            base.OnModelCreating(modelBuilder);
        }        
    }
}
