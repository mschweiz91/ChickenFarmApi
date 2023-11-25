using System.ComponentModel.DataAnnotations;

namespace ChickenFarmApi.DataAccess.Entities
{
    public class EggLayingRecord
    {

        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int EggCount { get; set; }
    }
}
