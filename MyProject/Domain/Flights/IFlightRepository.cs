namespace MyProject.Domain.Flights
{
    public interface IFlightRepository
    {
        Task AddAsync(Flight flight);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Flight flight);
        Task<Flight> GetByIdAsync(int Id);
        Task<List<Flight>> GetAllAsync(string? filter = null);
    }
}
