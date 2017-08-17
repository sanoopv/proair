using System.Collections.Generic;

namespace ProAir
{
    public class Flight : IFlight
    {
        public List<IRow> Rows { get; set; }

        public Flight(int maxRows = 10, params int[] seatBanks)
        {
            Rows = new List<IRow>();
            for (var i = 0; i < maxRows; i++)
            {
                Rows.Add(new Row(seatBanks));
            }
        }

    }
}
