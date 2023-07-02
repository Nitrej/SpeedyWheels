using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalApp.Data;
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
            var clients = this.context.Clients.Include(m => m.User).Select(m => new ClientViewModel { 
                Name = m.Name,
                Surname = m.Surname,
                PhoneNumber = m.PhoneNumber,
                Address = m.Address,
                Email = m.User.Email
            
            });
            return View(clients);
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