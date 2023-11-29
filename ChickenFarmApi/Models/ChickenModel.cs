using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChickenFarmApi.Models
{
    public class ChickenModel
    {
        [Key]
        public int ChickenId { get; set; }

        public string? Name { get; set; }

        // Navigation property to represent egg laying records associated with the chicken
        [JsonIgnore]
        public ICollection<EggLayingRecordModel> EggLayingRecords { get; set; }
    }
}
