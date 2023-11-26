﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenFarmApi.DataAccess.Entities
{
    public class EggLayingRecord 
    {
        [Key]
        public Guid RecordId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int EggCount { get; set; }


        // Foreign key to represent the associated chicken
        public int ChickenId { get; set; }
        public string? Name { get; set; }

        // Navigation property to represent the associated chicken
        [ForeignKey("ChickenId")]
        public Chicken Chicken { get; set; }

        
                       


    }
}
