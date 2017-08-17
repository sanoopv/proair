using System.Collections.Generic;

namespace ProAir
{
    public class Flight
    {
        private int maxRows;
        public List<Row> Rows;

        public Flight(int maxRows = 10, params int[] seatBanks)
        {
            Rows = new List<Row>();
            this.maxRows = maxRows;
            for (int i = 0; i < maxRows; i++)
            {
                Rows.Add(new Row(seatBanks));
            }
        }

    }
}
