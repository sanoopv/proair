using System.Linq;

namespace ProAir
{
    public class SeatBank : ISeatBank
    {
        public bool[] Seats { get; set; }

        public SeatBank(int seatsInBank)
        {
            Seats = new bool[seatsInBank];
        }

        public int Vacancy()
        {
            return Seats.Count(seat => !seat);
        }
    }
}
