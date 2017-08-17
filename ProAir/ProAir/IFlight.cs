using System.Collections.Generic;

namespace ProAir
{
    public interface IFlight
    {
        List<IRow> Rows { get; }
    }
}