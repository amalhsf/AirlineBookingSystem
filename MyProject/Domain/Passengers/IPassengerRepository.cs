namespace MyProject.Domain.Passengers
{
    public interface IPassengerRepository
    {
        Task AddAsync(Passenger passenger);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Passenger passenger);
        Task<Passenger> GetByIdAsync(int Id);
        Task<List<Passenger>> GetAllAsync(string? filter = null);
    }
}
