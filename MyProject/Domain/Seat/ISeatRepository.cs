namespace MyProject.Domain.Seat
{
    public interface ISeatRepository
    {
        Task AddAsync(Seat seat);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Seat seat);
        Task<Seat> GetById(int Id);
        Task<List<Seat>> GetAllAsync(string? filter);
    }
}
