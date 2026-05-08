using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Models;

namespace TaskManagementApp_MVC_.Repositories
{
    public interface ITaskRepository
    {
       
        public Task<IEnumerable<TaskItem>> GetAllAsync();
        public Task<TaskItem> GetByIdAsync(int id);
        public Task AddAsync(TaskItem task);
        public Task UpdateAsync(TaskItem task);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status);
        public Task<IEnumerable<TaskItem>> GetByOverdueAsync();
    }

    public class TaskRepository: ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetAllAsync() 
        {
             return await _context.TaskItems.ToListAsync();
            
        }
        public async Task<TaskItem> GetByIdAsync(int id)
        {
            var result = await _context.TaskItems.FindAsync(id);
            return result;
        }

    }


}
