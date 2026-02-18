using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Aircrafts;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreAircraftRepository : IAircraftRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCoreAircraftRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Aircraft aircraft)
        {
            _context.Add(aircraft);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Aircraft aircraft)
        {
            var result = await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == aircraft.Id);
            if (result != null)
            {
                result.Model = aircraft.Model;
                result.TotalSeats = aircraft.TotalSeats;
                _context.SaveChanges();
            }
        }
        public async Task<Aircraft> GetByIdAsync(int id)
        {
            var result = await _context.Aircrafts.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No aircraft was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Aircraft>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Aircrafts.ToList();
            }
            return _context.Aircrafts.Where(x => x.Model.Contains(filter)).ToList();
        }
    }
}
