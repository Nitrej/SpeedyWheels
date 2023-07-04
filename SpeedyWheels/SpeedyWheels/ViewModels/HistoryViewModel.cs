using SpeedyWheels.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SpeedyWheels.ViewModels
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string ProductionYear { get; set; }

        public double CostPerHour { get; set; }

        public  int ClientId { get; set; }

        public  int CarId { get; set; }

        public int HourCount { get; set; }

        public DateTime RentDate { get; set; }

        public double Cost { get; set; }

        public bool IsRated { get; set; }

        public int InvoiceId{ get; set; }
    }
}
