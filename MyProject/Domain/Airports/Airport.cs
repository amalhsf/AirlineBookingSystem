using MyProject.Domain.Flights;

namespace MyProject.Domain.Airports
{
    public class Airport : EntityBase<int>
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; } 

        // Navigation Properties

        // Flights departing from this airport
        public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();

        // Flights arriving to this airport
        public ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
    }
}
