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

namespace RentalApp.Data
{
    public class Services
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        public virtual int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        [Required]
        [NotNull]
        public DateTime StartDate { get; set; }

        [Required]
        [NotNull]
        public DateTime EndDate { get; set; }

        [Required]
        [NotNull]
        public double Cost { get; set; }

        [Required]
        [NotNull]
        public string Description { get; set; }
    }
}
