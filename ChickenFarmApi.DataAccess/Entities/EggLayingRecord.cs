using System.ComponentModel.DataAnnotations;

namespace ChickenFarmApi.DataAccess.Entities
{
    public class EggLayingRecord : Chicken
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int EggCount { get; set; }

        public int ChickenId { get; set; }

        // Navigation property to represent the associated chicken
        public required Chicken Chicken { get; set; }


    }
}
