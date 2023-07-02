using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Data
{
    public class User
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
        public string Email { get; set; }

        [Required]
        [NotNull]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [NotNull]
        [MaxLength(50)]
        public string Salt { get; set; }

        [Required]
        [NotNull]
        public bool IsAdmin { get; set; }

        [Required]
        [NotNull]
        public bool IsActive { get; set; }

    }
}
