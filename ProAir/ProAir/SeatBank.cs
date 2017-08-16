using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProAir
{
    public class SeatBank
    {
        public bool[] Seats;

        public SeatBank(int seats)
        {
            Seats = new bool[seats];
        }

        public int Vacancy()
        {
            return Seats.Count(seat => !seat);
        }
    }
}
