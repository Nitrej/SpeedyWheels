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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel;

namespace SpeedyWheels.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        [DisplayName("Imie")]
        public string Name { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [NotNull]
        [MaxLength(100)]
        [DisplayName("Adres")]
        public string Address { get; set; }

        [Required]
        [NotNull]
        [MaxLength(11)]
        [DisplayName("Numer Telefonu")]
        public string PhoneNumber { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Data Urodzenia")]
        public DateTime BirthDate { get; set; }

        [Required]
        [NotNull]
        [MaxLength(20)]
        [DisplayName("Numer Prawa Jazdy")]
        public string DriverLicenseNr { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Czy Aktywny")]
        public bool IsActive { get; set; }

        [Required]
        [NotNull]
        public virtual string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
