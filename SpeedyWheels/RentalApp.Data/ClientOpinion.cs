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
    public class ClientOpinion
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
        public int Rating { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        [Required]
        [NotNull]
        public DateTime Date { get; set; }

    }
}
