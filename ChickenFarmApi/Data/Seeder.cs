using ChickenFarmApi.DataAccess.Entities;
using ChickenFarmApi.DataAccess;

namespace ChickenFarmApi.Data
{
    public static class Seeder
    {
        public static void SeedData(ChickenFarmContext context)
        {
            // Check if the database has been seeded
            if (context.Chickens.Any())
            {
                return; // Database has been seeded
            }

            // Seed chickens
            var chickens = new[]
            {
                new Chicken { Name = "Tanis" },
                new Chicken { Name = "Tina" },
                new Chicken { Name = "Maya" },
                new Chicken { Name = "Lilith" },
                new Chicken { Name = "Big Red" },
                // Add more chickens as needed
            };

            context.Chickens.AddRange(chickens);
            context.SaveChanges();

            // Seed egg laying records
            var eggLayingRecords = new[]
            {
                new EggLayingRecord { Year = 2023, Month = 10, EggCount = 27, ChickenId = 1 },
                new EggLayingRecord { Year = 2023, Month = 10, EggCount = 22, ChickenId = 2 },
                new EggLayingRecord { Year = 2023, Month = 10, EggCount = 19, ChickenId = 3 },
                new EggLayingRecord { Year = 2023, Month = 10, EggCount = 15, ChickenId = 4 },
                new EggLayingRecord { Year = 2023, Month = 10, EggCount = 23, ChickenId = 5 },
                new EggLayingRecord { Year = 2023, Month = 11, EggCount = 20, ChickenId = 1 },
                new EggLayingRecord { Year = 2023, Month = 11, EggCount = 25, ChickenId = 2 },
                new EggLayingRecord { Year = 2023, Month = 11, EggCount = 23, ChickenId = 3 },
                new EggLayingRecord { Year = 2023, Month = 11, EggCount = 28, ChickenId = 4 },
                new EggLayingRecord { Year = 2023, Month = 11, EggCount = 24, ChickenId = 5 },
                // Add more egg laying records as needed
            };

            context.EggLayingRecords.AddRange(eggLayingRecords);
            context.SaveChanges();
        }
    }
}
