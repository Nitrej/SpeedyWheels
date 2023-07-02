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
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [NotNull]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [NotNull]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [NotNull]
        public DateTime BirthDate { get; set; }

        [Required]
        [NotNull]
        [MaxLength(20)]
        public string DriverLicenseNr { get; set; }

        [Required]
        [NotNull]
        public bool IsActive { get; set; }

        [Required]
        [NotNull]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
