using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Seats;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreSeatRepository : ISeatRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCoreSeatRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Seat seat)
        {
            _context.Add(seat);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Seats.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Seat seat)
        {
            var result = await _context.Seats.FirstOrDefaultAsync(x => x.Id == seat.Id);
            if (result != null)
            {
                result.SeatNumber = seat.SeatNumber;
                _context.SaveChanges();
            }
        }
        public async Task<Seat> GetByIdAsync(int id)
        {
            var result = await _context.Seats.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No seat was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Seat>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Seats.ToList();
            }
            return _context.Seats.Where(x => x.SeatNumber.Contains(filter)).ToList();
        }

    }
}
