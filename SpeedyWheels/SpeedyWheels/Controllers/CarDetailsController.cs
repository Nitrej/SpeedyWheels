﻿using Microsoft.AspNetCore.Mvc;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Data;
using Microsoft.Build.Framework;
using NuGet.Protocol.Plugins;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace SpeedyWheels.Controllers
{
    public class CarDetailsController : Controller
    {
        private readonly RentalDataContext context;
        private readonly UserManager<User> userManager;
        public CarDetailsController(RentalDataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var car = this.context.Cars.Where(o => o.Id == id).Select(m => new CarDetailsViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Brand = m.Brand,
                CostPerHour = m.CostPerHour,
                DoorCount = m.DoorCount,
                GearBox = m.GearBox,
                ImageAddress = m.ImageAddress,
                IsRented = m.IsRented,
                Mileage = m.Mileage,
                ProductionYear = m.ProductionDay.Year.ToString(),
                RegistrationNumber = m.RegistrationNumber,
                SeatsCount = m.SeatsCount,
            });
            return View(car);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Rent(int id, string st, string ed)
        {
            DateTime start = DateTime.Parse(st);
            DateTime end = DateTime.Parse(ed);
            var rent = new Rental();

            rent.RentDate = start.ToUniversalTime();
            rent.CarId = id;

            rent.ClientId = this.context.Clients.FirstOrDefault(i => i.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
            TimeSpan duration = end.Subtract(start);

            if(duration.Minutes>0) rent.HourCount = duration.Hours + 1 + duration.Days * 24;
            else rent.HourCount = duration.Hours + duration.Days * 24;

            rent.Cost = rent.HourCount * this.context.Cars.FirstOrDefault(i => i.Id == id).CostPerHour;

            this.context.Rentals.Add(rent);

            this.context.Cars.FirstOrDefault(i => i.Id == id).IsRented = true;

            this.context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
