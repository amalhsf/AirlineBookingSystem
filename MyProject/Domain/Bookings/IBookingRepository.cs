namespace MyProject.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Booking booking);
        Task<Booking> GetByIdAsync(int Id);
        Task<List<Booking>> GetAllAsync(DateTime? date);
    }
}
