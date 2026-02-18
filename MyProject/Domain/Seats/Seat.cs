using MyProject.Domain.Flights;
using MyProject.Domain.Passengers;

namespace MyProject.Domain.Seats
{
    public enum Class
    {
        Economy = 0,
        Business = 1
    }

    public class Seat : EntityBase<int>
    {
        public string SeatNumber { get; set; }
        public Class SeatClass { get; set; }

        // Relationship with Flight
        public int FlightId { get; set; }
        public Flight Flight { get; set; } = null!;

        // Optional relationship with Passenger
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }

    }
}
