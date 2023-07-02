using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyWheels.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [NotNull]
        public int CostPerHour { get; set; }

        [Required]
        [NotNull]
        public DateTime ProductionDay { get; set; }

        [Required]
        [NotNull]
        public double Mileage { get; set; }

        [Required]
        [NotNull]
        public int DoorCount { get; set; }

        [Required]
        [NotNull]
        public char GearBox { get; set; }

        [Required]
        [NotNull]
        public int SeatsCount { get; set; }

        [Required]
        [NotNull]
        public bool IsRented { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [NotNull]
        [MaxLength(20)]
        public string RegistrationNumber { get; set; }

        [Required]
        [NotNull]
        public string ImageAddress { get; set; }

    }
}
