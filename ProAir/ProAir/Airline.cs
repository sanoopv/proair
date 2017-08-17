using System.Collections.Generic;
namespace ProAir
{
    public class Airline
    {
        public Airline()
        {
            Flights = new List<IFlight>();
        }

        public List<IFlight> Flights;
        public void Add(IFlight flight)
        {
            Flights.Add(flight);
        }
    }
}
