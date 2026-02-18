using MyProject.Domain.Flights;
using MyProject.Domain.Passengers;
using MyProject.Domain.Users;

namespace MyProject.Domain.Bookings
{

    public enum BookingStatus
    {
        Pending = 0,
        Confirmed = 1,
        Cancelled = 2
    }

    public class Booking : EntityBase<int>
    {
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        // Navigation Properties

        public User User { get; set; } = null!;
        public Flight Flight { get; set; } = null!;
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
