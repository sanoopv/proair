namespace ProAir
{
    public interface ISeatAllocator
    {
        int VacantSeats(IFlight flight);
        ISeatDetail AllocateSingleSeat(IFlight flight);
        ISeatDetail[] AllocateDoubleSeat(IFlight flight);
    }
}