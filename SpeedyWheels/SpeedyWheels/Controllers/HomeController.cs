using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace SpeedyWheels.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentalDataContext context;
        public HomeController(RentalDataContext context)
        {
            this.context = context;
        }

        public IActionResult Index(string pattern)
        {
            
            var cars = this.context.Cars.Where(o => o.IsRented==false && o.IsActive == true).Select(m => new CarSearchViewModel
            {
                Brand = m.Brand,
                Name = m.Name,
                ProductionYear = m.ProductionDay.Year.ToString(),
                CostPerHour = m.CostPerHour,
                ImgUrl = m.ImageAddress,
                Id = m.Id
            });

            if (!String.IsNullOrEmpty(pattern))
            {
                pattern = pattern.ToLower();
                cars = cars.Where(s => s.Name.ToLower().Contains(pattern) || s.Brand.ToLower().Contains(pattern));
            }
            return View(cars);
        }


        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Email()
        {
            //brak implementacji obsługi wysłania email
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}