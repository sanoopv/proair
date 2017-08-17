using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProAir;

namespace ProAirTests
{
    /// <summary>
    /// Summary description for ProAirTest
    /// </summary>
    [TestClass]
    public class ProAirTest
    {
        public ProAirTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFlightCanBeAdded()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            List<IFlight> flights = airline.Flights;
            Assert.AreEqual(1, flights.Count);
        }


        [TestMethod]
        public void TestFlightSeatingNeedsToBeSpecified()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            List<IFlight> flights = airline.Flights;
            Assert.AreEqual(1, flights.Count);
        }
        [TestMethod]
        public void TestFlightRowsCanToBeSpecified()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            IFlight flight = airline.Flights[0];
            List<IRow> rows = flight.Rows;
            Assert.AreEqual(10, rows.Count);
        }



        [TestMethod]
        public void TestVacancyReporting()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();

            int expected = 70;
            int actual = seatAllocator.VacantSeats(airline.Flights[0]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestVacancyReportingAgain()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(1, 1, 1, 1));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            int expected = 3;
            int actual = seatAllocator.VacantSeats(airline.Flights[0]);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSingleSeatAllocation()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            ISeatDetail expected = new SeatDetail { Row = 1, Bank = 1, Seat = 1 };
            ISeatDetail seatDetails = seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            Assert.AreEqual(expected.Bank, seatDetails.Bank);
            Assert.AreEqual(expected.Row, seatDetails.Row);
            Assert.AreEqual(expected.Seat, seatDetails.Seat);

        }
        [TestMethod]
        public void TestSingleSeatAllocation3Times()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            ISeatDetail expected = new SeatDetail { Row = 1, Bank = 2, Seat = 1 };
            ISeatDetail seatDetails = seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            Assert.AreEqual(expected.Bank, seatDetails.Bank);
            Assert.AreEqual(expected.Row, seatDetails.Row);
            Assert.AreEqual(expected.Seat, seatDetails.Seat);

        }
        [TestMethod]
        public void TestSingleSeatAllocationForSecondRow()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            ISeatDetail expected = new SeatDetail { Row = 2, Bank = 1, Seat = 2 };
            ISeatDetail seatDetails = seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            Assert.AreEqual(expected.Bank, seatDetails.Bank);
            Assert.AreEqual(expected.Row, seatDetails.Row);
            Assert.AreEqual(expected.Seat, seatDetails.Seat);

        }
        [TestMethod]
        public void TestDoubleSeatAllocation()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(10, 2, 3, 2));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            ISeatDetail[] allocatedSeats = seatAllocator.AllocateDoubleSeat(airline.Flights[0]);
            ISeatDetail expected1 = new SeatDetail { Row = 1, Bank = 1, Seat = 1 };
            ISeatDetail expected2 = new SeatDetail { Row = 1, Bank = 1, Seat = 2 };

            Assert.AreEqual(expected2.Bank, allocatedSeats[0].Bank);
            Assert.AreEqual(expected2.Row, allocatedSeats[0].Row);
            Assert.AreEqual(expected2.Seat, allocatedSeats[0].Seat);

            Assert.AreEqual(expected1.Bank, allocatedSeats[1].Bank);
            Assert.AreEqual(expected1.Row, allocatedSeats[1].Row);
            Assert.AreEqual(expected1.Seat, allocatedSeats[1].Seat);

        }

        [TestMethod]
        public void TestDoubleSeatAllocationNoContinousFit()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(2, 1, 1, 1));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            ISeatDetail[] allocatedSeats = seatAllocator.AllocateDoubleSeat(airline.Flights[0]);
            ISeatDetail expected1 = new SeatDetail { Row = 1, Bank = 1, Seat = 1 };
            ISeatDetail expected2 = new SeatDetail { Row = 1, Bank = 2, Seat = 1 };


            Assert.AreEqual(expected1.Bank, allocatedSeats[0].Bank);
            Assert.AreEqual(expected1.Row, allocatedSeats[0].Row);
            Assert.AreEqual(expected1.Seat, allocatedSeats[0].Seat);

            Assert.AreEqual(expected2.Bank, allocatedSeats[1].Bank);
            Assert.AreEqual(expected2.Row, allocatedSeats[1].Row);
            Assert.AreEqual(expected2.Seat, allocatedSeats[1].Seat);

        }
        [TestMethod]
        public void TestDoubleSeatAllocationGetRandomFit()
        {
            Airline airline = new Airline();
            airline.Add(new Flight(2, 1, 1));
            ISeatAllocator seatAllocator = SeatAllocatorFactory.GetAllocator();
            seatAllocator.AllocateSingleSeat(airline.Flights[0]);
            airline.Flights[0].Rows[1].SeatBanks[1].Seats[0] = true;
            ISeatDetail[] allocatedSeats = seatAllocator.AllocateDoubleSeat(airline.Flights[0]);
            ISeatDetail expected1 = new SeatDetail { Row = 1, Bank = 2, Seat = 1 };
            ISeatDetail expected2 = new SeatDetail { Row = 2, Bank = 1, Seat = 1 };

            Assert.AreEqual(expected1.Bank, allocatedSeats[0].Bank);
            Assert.AreEqual(expected1.Row, allocatedSeats[0].Row);
            Assert.AreEqual(expected1.Seat, allocatedSeats[0].Seat);

            Assert.AreEqual(expected2.Bank, allocatedSeats[1].Bank);
            Assert.AreEqual(expected2.Row, allocatedSeats[1].Row);
            Assert.AreEqual(expected2.Seat, allocatedSeats[1].Seat);

        }
    }
}
