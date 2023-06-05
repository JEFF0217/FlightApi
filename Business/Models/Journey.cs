using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Journey
    {
        public List<Flight>? Flights { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double Price { get; set; }
        public Journey(List<Flight> flights, string origin, string destination)
        {
            this.Flights = flights;
            this.Origin = origin;
            this.Destination = destination;
            this.Price = GetPrice();
        }
        private double GetPrice()
        {
            double price = 0;
            this.Flights?.ForEach(flight => { price+= flight.Price; });
            return price;
        }
    }
}
