using System.Collections.Generic;
using System.Linq;

namespace ProAir
{
    public class SeatAllocator : ISeatAllocator
    {
        public int VacantSeats(IFlight flight)
        {
            return flight.Rows.Sum(row => row.SeatBanks.Sum(bank => bank.Seats.Count(seat => !seat)));
        }

        public ISeatDetail AllocateSingleSeat(IFlight flight)
        {
            var seatBookedDetail = new SeatDetail();
            var seatAllocated = false;
            for (var rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                for (var bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    for (var seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
                    {
                        if (flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex]) continue;
                        seatAllocated = true;
                        flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex] = true;
                        seatBookedDetail.Row = rowIndex + 1;
                        seatBookedDetail.Bank = bankIndex + 1;
                        seatBookedDetail.Seat = seatIndex + 1;
                        break;
                    }
                    if (!seatAllocated) continue;
                    break;
                }
                if (!seatAllocated) continue;
                break;
            }
            return seatBookedDetail;
        }

        public ISeatDetail[] AllocateDoubleSeat(IFlight flight)
        {
            var seatsBooked =  AllocateDoubleSeatFirstFullFit(flight);
            if (seatsBooked.Length == 0)
            {
                seatsBooked = AllocateDoubleSeatNearbyFit(flight);
            }
            if (seatsBooked.Length == 0)
            {
                seatsBooked = AllocateDoubleSeatAnyFit(flight);
            }
            return seatsBooked;
        }

        private ISeatDetail[] AllocateDoubleSeatAnyFit(IFlight flight)
        {
            const int SEATS_TO_BE_BOOKED = 2;
            var seatBookedDetailList = new List<ISeatDetail>();
            for (var i = 0; i < SEATS_TO_BE_BOOKED; i++)
            {
                seatBookedDetailList.Add(AllocateSingleSeat(flight));
            }
            return seatBookedDetailList.ToArray();
        }

        private ISeatDetail[] AllocateDoubleSeatNearbyFit(IFlight flight)
        {
            const int SEATS_TO_BE_BOOKED = 2;
            var seatsBooked = 0;
            var seatsAllocated = false;
            var seatBookedDetailList = new List<ISeatDetail>();
            for (var rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                if (flight.Rows[rowIndex].VacantSeats() < SEATS_TO_BE_BOOKED) continue;
                for (var bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    for (var seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
                    {
                        if (!flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex])
                        {
                            flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex] = true;
                            seatBookedDetailList.Add(new SeatDetail
                            {
                                Row = rowIndex + 1,
                                Bank = bankIndex + 1,
                                Seat = seatIndex + 1
                            });
                            seatsBooked++;
                            if (seatsBooked == SEATS_TO_BE_BOOKED)
                            {
                                seatsAllocated = true;
                                break;
                            }
                        }
                    }
                    if (!seatsAllocated) continue;
                    break;
                }
                if (!seatsAllocated) continue;
                break;
            }
            return seatBookedDetailList.ToArray();
        }

        private ISeatDetail[] AllocateDoubleSeatFirstFullFit(IFlight flight)
        {
            const int SEATS_TO_BE_BOOKED = 2;
            var seatAllocated = false;
            var seatBookedDetailList = new List<ISeatDetail>();
            for (var rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                if (flight.Rows[rowIndex].VacantSeats() < SEATS_TO_BE_BOOKED) continue;
                for (var bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    if (flight.Rows[rowIndex].SeatBanks[bankIndex].Vacancy() < SEATS_TO_BE_BOOKED) continue;
                    var continuous = 0;
                    for (var seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
                    {
                        if (!flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex])
                        {
                            continuous++;
                            if (continuous == SEATS_TO_BE_BOOKED)
                            {
                                seatAllocated = true;
                                for (var i = 0; i < continuous; i++)
                                {
                                    flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex - i] = true;
                                    seatBookedDetailList.Add(new SeatDetail
                                    {
                                        Row = rowIndex + 1,
                                        Bank = bankIndex + 1,
                                        Seat = (seatIndex - i) + 1
                                    });
                                }
                                break;
                            }
                        }
                        else
                        {
                            continuous = 0;
                        }
                    }
                    if (!seatAllocated) continue;
                    break;
                }
                if (!seatAllocated) continue;
                break;
            }
            return seatBookedDetailList.ToArray();
        }
    }
}
