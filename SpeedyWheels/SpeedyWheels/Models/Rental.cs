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
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        public virtual int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [Required]
        [NotNull]
        public virtual int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [Required]
        [NotNull]
        public int HourCount { get; set; }

        [Required]
        [NotNull]
        public DateTime RentDate { get; set; }

        [Required]
        [NotNull]
        public double Cost { get; set; }

        [Required]
        [NotNull]
        public bool IsRated { get; set; }
    }
}
