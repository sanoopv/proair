using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProAir
{
    public class Airline
    {
        public Airline()
        {
            Flights = new List<Flight>();
        }

        public List<Flight> Flights;
        public void Add(Flight flight)
        {
            this.Flights.Add(flight);
        }
    }
}
