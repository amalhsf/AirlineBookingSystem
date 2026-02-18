using MyProject.Domain.Aircrafts;
using MyProject.Domain.Airports;
using MyProject.Domain.Bookings;
using MyProject.Domain.Seats;

namespace MyProject.Domain.Flights
{
    public class Flight : EntityBase<int>
    {
        public string FlightNumber { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int AircraftId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public Aircraft Aircraft { get; set; } = null!;

        public Airport DepartureAirport { get; set; } = null!;
        public Airport ArrivalAirport { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
