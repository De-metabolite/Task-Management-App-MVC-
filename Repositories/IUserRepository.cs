using Microsoft.EntityFrameworkCore;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Entities;

namespace TaskManagementApp_MVC_.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistingAccountAsync(string email, string password);
        Task CreateUserAsync(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistingAccountAsync(string email, string password)
        {
            var exist = await _context.Users.AnyAsync(u => u.Email == email && u.Password== password);
            return exist;
        }
        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
