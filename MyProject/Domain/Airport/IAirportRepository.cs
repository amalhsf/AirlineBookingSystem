namespace MyProject.Domain.Airport
{
    public interface IAirportRepository
    {
        Task AddAsync(Airport airport);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Airport airport);
        Task<Airport> GetById(int Id);
        Task<List<Airport>> GetAllAsync(string? filter);
    }
}
