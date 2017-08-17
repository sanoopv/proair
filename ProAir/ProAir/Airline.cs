using System.Collections.Generic;

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
