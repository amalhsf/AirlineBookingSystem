namespace MyProject.Domain.Aircrafts
{
    public interface IAircraftRepository
    {
        Task AddAsync(Aircraft aircraft);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Aircraft aircraft);
        Task<Aircraft> GetByIdAsync(int Id);
        Task<List<Aircraft>> GetAllAsync(string? filter);
    }
}
