using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProAir
{
    public class SeatAllocator
    {
        private Flight flight;
        public SeatAllocator(Flight flight)
        {
            this.flight = flight;
        }

        public int VacantSeats()
        {
            return flight.Rows.Sum(row => row.SeatBanks.Sum(bank => bank.Seats.Count(seat => !seat)));
        }

        public SeatDetail AllocateSingleSeat()
        {
            SeatDetail seatBookedDetail = new SeatDetail();
            bool seatAllocated = false;
            for (int rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                for (int bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    for (int seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
                    {
                        if (!flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex])
                        {
                            seatAllocated = true;
                            flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex] = true;
                            seatBookedDetail.Row = rowIndex + 1;
                            seatBookedDetail.Bank = bankIndex + 1;
                            seatBookedDetail.Seat = seatIndex + 1;
                            break;
                        }
                    }
                    if (!seatAllocated) continue;
                    break;
                }
                if (!seatAllocated) continue;
                break;
            }
            return seatBookedDetail;
        }

        public SeatDetail[] AllocateDoubleSeat()
        {
            var seatsBooked =  AllocateDoubleSeatFirstFullFit();
            if (seatsBooked.Length == 0)
            {
                seatsBooked = AllocateDoubleSeatNearbyFit();
            }
            if (seatsBooked.Length == 0)
            {
                seatsBooked = AllocateDoubleSeatAnyFit();
            }
            return seatsBooked;
        }

        private SeatDetail[] AllocateDoubleSeatAnyFit()
        {
            int seatsToBeBooked = 2;
            var seatBookedDetailList = new List<SeatDetail>();
            for (int i = 0; i < seatsToBeBooked; i++)
            {
                seatBookedDetailList.Add(AllocateSingleSeat());
            }
            return seatBookedDetailList.ToArray();
        }

        private SeatDetail[] AllocateDoubleSeatNearbyFit()
        {
            int seatsToBeBooked = 2;
            int seatsBooked = 0;
            bool seatsAllocated = false;
            var seatBookedDetailList = new List<SeatDetail>();
            for (int rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                if (flight.Rows[rowIndex].VacantSeats() < seatsToBeBooked) continue;
                for (int bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    for (int seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
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
                            if (seatsBooked == seatsToBeBooked)
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

        private SeatDetail[] AllocateDoubleSeatFirstFullFit()
        {
            int seatsToBeBooked = 2;
            bool seatAllocated = false;
            var seatBookedDetailList = new List<SeatDetail>();
            for (int rowIndex = 0; rowIndex < flight.Rows.Count; rowIndex++)
            {
                if (flight.Rows[rowIndex].VacantSeats() < seatsToBeBooked) continue;
                for (int bankIndex = 0; bankIndex < flight.Rows[rowIndex].SeatBanks.Count; bankIndex++)
                {
                    if (flight.Rows[rowIndex].SeatBanks[bankIndex].Vacancy() < seatsToBeBooked) continue;
                    int continuous = 0;
                    for (int seatIndex = 0; seatIndex < flight.Rows[rowIndex].SeatBanks[bankIndex].Seats.Length; seatIndex++)
                    {
                        if (!flight.Rows[rowIndex].SeatBanks[bankIndex].Seats[seatIndex])
                        {
                            continuous++;
                            if (continuous == seatsToBeBooked)
                            {
                                seatAllocated = true;
                                for (int i = 0; i < continuous; i++)
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
