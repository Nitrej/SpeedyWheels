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
        [DisplayName("Data Rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Data Zakończenia")]
        public DateTime EndDate { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Koszt")]
        public double Cost { get; set; }

        [Required]
        [NotNull]
        [DisplayName("Opis")]
        public string Description { get; set; }
    }
}
