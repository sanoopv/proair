using System.Collections.Generic;

namespace ProAir
{
    public interface IRow
    {
        List<ISeatBank> SeatBanks { get; set; }
        int VacantSeats();
    }
}