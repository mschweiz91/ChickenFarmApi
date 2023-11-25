using ChickenFarmApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenFarmApi.DataAccess
{
    public class EggLayingContext : DbContext
    {
        private string DBPath {  get; set; }

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
