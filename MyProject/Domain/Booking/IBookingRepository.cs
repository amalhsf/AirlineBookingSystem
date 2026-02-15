namespace MyProject.Domain.Booking
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Booking booking);
        Task<Booking> GetById(int Id);
        Task<List<Booking>> GetAllAsync(string? filter);
    }
}
