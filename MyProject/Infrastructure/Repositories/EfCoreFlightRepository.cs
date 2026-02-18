using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Flights;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreFlightRepository : IFlightRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCoreFlightRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Flight flight)
        {
            _context.Add(flight);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Flight flight)
        {
            var result = await _context.Flights.FirstOrDefaultAsync(x => x.Id == flight.Id);
            if (result != null)
            {
                result.FlightNumber = flight.FlightNumber;
                result.DepartureAirportId = flight.DepartureAirportId;
                result.ArrivalAirportId = flight.ArrivalAirportId;
                result.AircraftId = flight.AircraftId;
                result.DepartureTime = flight.DepartureTime;
                result.ArrivalTime = flight.ArrivalTime;
                result.Price = flight.Price;
                _context.SaveChanges();
            }
        }
        public async Task<Flight> GetByIdAsync(int id)
        {
            var result = await _context.Flights.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No Flight was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Flight>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Flights.ToList();
            }
            return _context.Flights.Where(x => x.FlightNumber.Contains(filter)).ToList();
        }

    }
}
