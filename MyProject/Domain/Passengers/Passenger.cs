using MyProject.Domain.Bookings;
using MyProject.Domain.Seats;

namespace MyProject.Domain.Passengers
{
    public class Passenger : EntityBase<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BookingId { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SeatId { get; set; }


        // Navigation Property
        public Booking Booking { get; set; } = null!;
        public Seat Seat { get; set; } = null!;

    }
}
