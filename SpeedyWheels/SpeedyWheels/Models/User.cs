using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyWheels.Models
{
    public class User : IdentityUser
    {
        
        [Required]
        [NotNull]
        public bool IsAdmin { get; set; }

        [Required]
        [NotNull]
        public bool IsActive { get; set; }

    }
}
