﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;
using System.Security.Claims;

namespace SpeedyWheels.Controllers
{
    public class RentsHistoryController : Controller
    {
        private readonly RentalDataContext context;
        public RentsHistoryController(RentalDataContext context)
        {
            this.context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var id = this.context.Clients.FirstOrDefault(i => i.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
            var rent = this.context.Rentals.Where(o => o.ClientId == id).Include("Car").Include("Client").OrderBy(o => o.Id).Select(m => new HistoryViewModel
            {
                Id = m.Id,
                Brand = m.Car.Brand,
                Name = m.Car.Name,
                CarId = m.CarId,
                ClientId = m.ClientId,
                Cost = m.Cost,
                CostPerHour = m.Car.CostPerHour,
                HourCount = m.HourCount,
                ImgUrl = m.Car.ImageAddress,
                ProductionYear = m.Car.ProductionYear,
                RentDate = m.RentDate,
                IsRated = m.IsRated
            });
            return View(rent);
        }
        public IActionResult Details(int id) 
        {
            return RedirectToAction("Details","CarDetails", new { id = id });
        }

        [HttpPost]
        public IActionResult Rating(int rentId, int rate, string comment, int clientId)
        {

            if(rate<1) rate = 1;
            if(rate>5) rate = 5;

            this.context.Rentals.FirstOrDefault(i => i.Id == rentId).IsRated = true;
            this.context.SaveChanges();

            var ClientOpinion = new ClientOpinion();

            ClientOpinion.Date = DateTime.Now;
            ClientOpinion.Rating = rate;
            ClientOpinion.ClientId = clientId;
            ClientOpinion.RentalId = rentId;
            ClientOpinion.Content = comment;

            this.context.ClientOpinions.Add(ClientOpinion);

            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Policy = "moderatorsOnly")]
        public IActionResult AllHistory()
        {
            var rent = this.context.Rentals.Include("Car").Include("Client").OrderBy(o => o.Id).Select(m => new HistoryViewModel
            {
                Id = m.Id,
                Brand = m.Car.Brand,
                Name = m.Car.Name,
                CarId = m.CarId,
                ClientId = m.ClientId,
                Cost = m.Cost,
                CostPerHour = m.Car.CostPerHour,
                HourCount = m.HourCount,
                ImgUrl = m.Car.ImageAddress,
                ProductionYear = m.Car.ProductionYear,
                RentDate = m.RentDate,
                IsRated = m.IsRated
            });
            return View(rent);
        }
        [Authorize(Policy = "moderatorsOnly")]
        public IActionResult GoToRate(int id)
        {
            var op = this.context.ClientOpinions.FirstOrDefault(i => i.RentalId == id);
            return RedirectToAction("Details","ClientOpinions", new {id = op.Id});
        }
        [Authorize(Policy = "moderatorsOnly")]
        public IActionResult GoToInvoice(int id)
        {
            var inv = this.context.Invoices.FirstOrDefault(i => i.RentalId == id);
            return RedirectToAction("Details", "Invoices", new { id = inv.Id });
        }
    }
}
