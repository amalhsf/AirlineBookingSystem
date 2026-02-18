using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Passengers;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCorePassengerRepository : IPassengerRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCorePassengerRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Passenger passenger)
        {
            _context.Add(passenger);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Passengers.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Passenger passenger)
        {
            var result = await _context.Passengers.FirstOrDefaultAsync(x => x.Id == passenger.Id);
            if (result != null)
            {
                result.FirstName = passenger.FirstName;
                result.LastName = passenger.LastName;
                result.DateOfBirth = passenger.DateOfBirth;
                result.PassportNumber = passenger.PassportNumber;
                result.BookingId = passenger.BookingId;
                _context.SaveChanges();
            }
        }
        public async Task<Passenger> GetByIdAsync(int id)
        {
            var result = await _context.Passengers.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No passenger was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Passenger>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Passengers.ToList();
            }
            return _context.Passengers.Where(x => x.FirstName.Contains(filter) || x.LastName.Contains(filter)).ToList();
        }

    }
}
