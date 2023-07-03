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
    }
}
