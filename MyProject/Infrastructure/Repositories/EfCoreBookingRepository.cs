using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Booking;
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
            _context.Add(booking);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(Booking booking)
        {
            var result = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == booking.Id);
            if (result != null)
            {
                result.Model = airport.Model;
                result.TotalSeats = airport.TotalSeats;
                _context.SaveChanges();
            }
        }
        public async Task<Booking> GetByIdAsync(int id)
        {
            var result = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No booking was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<Booking>> GetAllAsync(DateOnly? date)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Bookings.ToList();
            }
            return _context.Bookings.Where(x => x.BookingDate == date ).ToList;
        }
    }
}
