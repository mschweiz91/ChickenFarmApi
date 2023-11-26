﻿using System.ComponentModel.DataAnnotations;

namespace ChickenFarmApi.DataAccess.Entities
{
    public class Chicken
    {
        [Key]
        public int ChickenId { get; set; }

        public string? Name { get; set; }

        // Navigation property to represent egg laying records associated with the chicken
        public ICollection<EggLayingRecord> EggLayingRecords { get; set; }
    }
}
