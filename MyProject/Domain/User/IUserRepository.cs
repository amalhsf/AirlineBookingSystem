namespace MyProject.Domain.User
{
    public interface IUserRepository
    {
        Task AddAsync(Seat seat);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Seat seat);
        Task<Seat> GetById(int Id);
        Task<List<Seat>> GetAllAsync(string? filter);
    }
}
