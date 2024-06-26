﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyWheels.Models
{
    public class RentalDataContext : DbContext
    {
        public RentalDataContext(DbContextOptions<RentalDataContext> options): base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<Client>(e => { e.HasIndex(e => e.UserId).IsUnique(); });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.Id });
        }

        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientOpinion> ClientOpinions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Services> Services { get; set; }

    }

}
