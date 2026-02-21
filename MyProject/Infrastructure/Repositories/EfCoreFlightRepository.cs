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
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Flights
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                _context.Flights.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Flight flight)
        {
            var result = await _context.Flights
                .FirstOrDefaultAsync(x => x.Id == flight.Id);

            if (result == null)
                throw new EntityNotFoundException($"No Flight found with id: {flight.Id}");

            result.FlightNumber = flight.FlightNumber;
            result.DepartureAirportId = flight.DepartureAirportId;
            result.ArrivalAirportId = flight.ArrivalAirportId;
            result.AircraftId = flight.AircraftId;
            result.DepartureTime = flight.DepartureTime;
            result.ArrivalTime = flight.ArrivalTime;
            result.Price = flight.Price;

            await _context.SaveChangesAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            var result = await _context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                throw new EntityNotFoundException($"No Flight found with id: {id}");

            return result;
        }

        public async Task<List<Flight>> GetAllAsync(string? filter = null)
        {
            var query = _context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.FlightNumber.Contains(filter));
            }

            return await query.ToListAsync();
        }
    }
}
