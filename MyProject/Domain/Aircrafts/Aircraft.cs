using MyProject.Domain.Flights;

namespace MyProject.Domain.Aircrafts
{
    public class Aircraft : EntityBase<int>
    {
        public string Model { get; set; }
        public int TotalSeats { get; set; }

        // Navigation Properties
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
