namespace MyProject.Domain.Airports
{
    public interface IAirportRepository
    {
        Task AddAsync(Airport airport);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Airport airport);
        Task<Airport> GetByIdAsync(int Id);
        Task<List<Airport>> GetAllAsync(string? filter = null);
    }
}
