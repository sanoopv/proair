using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;

namespace ProAir
{
    public class Row
    {
        public List<SeatBank> SeatBanks;
        public Row(int[] seatGroups)
        {
            SeatBanks = new List<SeatBank>();
            foreach (var seats in seatGroups)
            {
                SeatBanks.Add(new SeatBank(seats));
            }

        }

        public int VacantSeats()
        {
           return SeatBanks.Sum(item => item.Seats.Count(seat => !seat));
        }
    }

}
