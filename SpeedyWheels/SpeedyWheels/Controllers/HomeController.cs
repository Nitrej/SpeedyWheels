using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;
using System.Diagnostics;

namespace SpeedyWheels.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentalDataContext context;
        public HomeController(RentalDataContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var cars = this.context.Cars.Select(m => new CarSearchViewModel
            {
                Brand = m.Brand,
                Name = m.Name,
                ProductionYear = m.ProductionDay.Year.ToString(),
                CostPerHour = m.CostPerHour,
                ImgUrl = m.ImageAddress
            });
            return View(cars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}