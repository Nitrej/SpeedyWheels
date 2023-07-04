using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SpeedyWheels.Controllers;
using SpeedyWheels.Models;
using SpeedyWheels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfCars()
        {
            // Arrange
            var pattern = "####";
            var expectedCars = new List<Car>
            {
                new Car { Brand = "TestBrand", Name = "TestCar####", IsRented = false, IsActive = true, CostPerHour = 0, DoorCount = 0, GearBox = 'M', ImageAddress="0", Mileage = 1, ProductionYear= "1", RegistrationNumber = "1", SeatsCount = 1},
                new Car { Brand = "AnotherBrand####", Name = "AnotherCar", IsRented = false, IsActive = true, CostPerHour = 0, DoorCount = 0, GearBox = 'M' , ImageAddress="0", Mileage = 1, ProductionYear= "1", RegistrationNumber = "1", SeatsCount = 1 }
            };
            
            var options = new DbContextOptionsBuilder<RentalDataContext>().UseNpgsql( "Server=158.75.112.40;Database=jasno;Port=5432;User Id=jasno;Password=TreTronik229!").Options;

            using var context = new RentalDataContext(options);
            context.Cars.AddRange(expectedCars);
            context.SaveChanges();

            var controller = new HomeController(context);

            // Act
            var result = controller.Index(pattern) as ViewResult;
            var model = result?.Model as IEnumerable<CarSearchViewModel>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(expectedCars.Count, model.Count());
            Assert.All(model, car =>
            {
                Assert.True(
                    car.Brand.ToLower().Contains(pattern.ToLower()) ||
                    car.Name.ToLower().Contains(pattern.ToLower())
                );
            });

            context.Cars.RemoveRange(expectedCars);
            context.SaveChanges();
        }
    }
}