using Microsoft.AspNetCore.Mvc;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;

namespace SpeedyWheels.Controllers
{
    public class CarDetailsController : Controller
    {
        private readonly RentalDataContext context;
        public CarDetailsController(RentalDataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var car = this.context.Cars.Where(o => o.Id == id);
            return View(car);
        }
    }
}
