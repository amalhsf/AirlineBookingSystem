namespace MyProject.Domain.Seats
{
    public interface ISeatRepository
    {
        Task AddAsync(Seat seat);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Seat seat);
        Task<Seat> GetByIdAsync(int Id);
        Task<List<Seat>> GetAllAsync(string? filter);
    }
}
