using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Airports;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreAirportRepository : IAirportRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCoreAirportRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Airport airport)
        {
            _context.Add(airport);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Airports.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Airport airport)
        {
            var result = await _context.Airports.FirstOrDefaultAsync(x => x.Id == airport.Id);
            if (result != null)
            {
                result.Name = airport.Name;
                result.City = airport.City;
                result.Country = airport.Country;
                _context.SaveChanges();
            }
        }
        public async Task<Airport> GetByIdAsync(int id)
        {
            var result = await _context.Airports.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No airport was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Airport>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Airports.ToList();
            }
            return _context.Airports.Where(x => x.Name.Contains(filter)).ToList();
        }

    }
}
