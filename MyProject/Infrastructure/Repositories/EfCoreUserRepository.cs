using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Users;
using MyProject.Infrastructure.Database;
using MyProject.Exceptions;

namespace MyProject.Infrastructure.Repositories
{
    public class EfCoreUserRepository : IUserRepository
    {
        private readonly BookingSystemDbContext _context;
        public EfCoreUserRepository(BookingSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
        public async Task UpdateAsync(User user)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (result != null)
            {
                result.Name = user.Name;
                result.Email = user.Email;
                result.PasswordHash = user.PasswordHash;
                _context.SaveChanges();
            }
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException($"No user was founf with this id: {id}");
            }
            return result;
        }
        public async Task<List<User>> GetAllAsync(string? filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _context.Users.ToList();
            }
            return _context.Users.Where(x => x.Name.Contains(filter)).ToList();
        }

    }
}
