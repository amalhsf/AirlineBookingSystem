namespace MyProject.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(int Id);
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int Id);
        Task<List<User>> GetAllAsync(string? filter);
    }
}
