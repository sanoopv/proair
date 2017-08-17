using System.Collections.Generic;

namespace ProAir
{
    public class Flight : IFlight
    {
        private int maxRows;
        public List<IRow> Rows { get; set; }

        public Flight(int maxRows = 10, params int[] seatBanks)
        {
            Rows = new List<IRow>();
            this.maxRows = maxRows;
            for (int i = 0; i < maxRows; i++)
            {
                Rows.Add(new Row(seatBanks));
            }
        }

    }
}
