namespace ProAir
{
    public class SeatAllocatorFactory
    {
        public static ISeatAllocator GetAllocator()
        {
           ISeatAllocator seatAllocator = new SeatAllocator();
            return seatAllocator;
        }
    }
}
