using System.ComponentModel.DataAnnotations;

namespace ChickenFarmApi.Models
{
    public class Chicken
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
