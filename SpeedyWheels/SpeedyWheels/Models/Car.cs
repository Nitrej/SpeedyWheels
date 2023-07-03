using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Koszt na godzine")]
        public int CostPerHour { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Rok produkcji")]
        public string ProductionYear { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Przebieg")]
        public double Mileage { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Ilość drzwi")]
        public int DoorCount { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Skrzynia biegów")]
        public char GearBox { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Ilość siedzeń")]
        public int SeatsCount { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Czy wypożyczony")]
        public bool IsRented { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        [DisplayName("Marka")]
        public string Brand { get; set; }

        [Required]
        [NotNull]
        [MaxLength(20)]
        [DisplayName("Numer Rejestracyjny")]
        public string RegistrationNumber { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Url obrazka")]
        public string ImageAddress { get; set; }

        [DisplayName("Czy aktywny")]
        public bool IsActive { get; set; }

    }
}
