using System.Collections.Generic;

namespace ProAir
{
    public interface IRow
    {
        List<SeatBank> SeatBanks { get; set; }
        int VacantSeats();
    }
}