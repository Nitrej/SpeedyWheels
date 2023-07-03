using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SpeedyWheels.ViewModels
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CostPerHour { get; set; }

        public string ProductionYear { get; set; }

        public double Mileage { get; set; }

        public int DoorCount { get; set; }

        public char GearBox { get; set; }

        public int SeatsCount { get; set; }

        public bool IsRented { get; set; }

        public string Brand { get; set; }

        public string RegistrationNumber { get; set; }

        public string ImageAddress { get; set; }
    }
}
