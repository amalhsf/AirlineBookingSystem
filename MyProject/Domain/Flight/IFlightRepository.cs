namespace MyProject.Domain.Flight
{
    public interface IFlightRepository
    {
        Task AddAsync(Flight flight);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Flight flight);
        Task<Flight> GetById(int Id);
        Task<List<Flight>> GetAllAsync(string? filter);
    }
}
