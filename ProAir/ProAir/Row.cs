using System.Collections.Generic;
using System.Linq;

namespace ProAir
{
    public class Row : IRow
    {
        public List<SeatBank> SeatBanks { get; set; }

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
