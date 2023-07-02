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
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotNull]
        public virtual int RentalId { get; set; }

        [ForeignKey("RentalId")]
        public virtual Rental Rental { get; set; }

        [Required]
        [NotNull]
        public virtual int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [Required]
        [NotNull]
        public DateTime IssueDate { get; set; }

        [Required]
        [NotNull]
        public double amount { get; set; }

        [Required]
        [NotNull]
        public bool PaymentStatus { get; set; }
    }
}
