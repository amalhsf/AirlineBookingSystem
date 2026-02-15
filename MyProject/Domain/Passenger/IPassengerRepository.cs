namespace MyProject.Domain.Passenger
{
    public interface IPassengerRepository
    {
        Task AddAsync(Passenger passenger);
        Task DeleteAsync(int Id);
        Task UpdateAsync(Flight passenger);
        Task<Passenger> GetById(int Id);
        Task<List<Passenger>> GetAllAsync(string? filter);
    }
}
