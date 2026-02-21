using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Bookings;
using MyProject.Exceptions;
using MyProject.Infrastructure.Database;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreBookingRepository : IBookingRepository
    {
        private readonly BookingSystemDbContext _context;

        public EfCoreBookingRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Bookings
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                _context.Bookings.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Booking booking)
        {
            var result = await _context.Bookings
                .FirstOrDefaultAsync(x => x.Id == booking.Id);

            if (result == null)
                throw new EntityNotFoundException($"No booking found with id: {booking.Id}");

            result.Status = booking.Status;
            result.TotalPrice = booking.TotalPrice;
            result.FlightId = booking.FlightId;

            await _context.SaveChangesAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            var result = await _context.Bookings
                .Include(b => b.Flight)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                throw new EntityNotFoundException($"No booking found with id: {id}");

            return result;
        }

        public async Task<List<Booking>> GetAllAsync(DateTime? date = null)
        {
            var query = _context.Bookings
                .Include(b => b.Flight)
                .AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(x => x.BookingDate.Date == date.Value.Date);
            }

            return await query.ToListAsync();
        }
    }
}
