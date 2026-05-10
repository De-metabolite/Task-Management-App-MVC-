using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Entities;

namespace TaskManagementApp_MVC_.Repositories
{
    public interface ITaskRepository
    {

        public Task<IEnumerable<TaskItem>> GetAllAsync(int UserId);
        public Task<TaskItem> GetByIdAsync(int id, int UserId);
        public Task AddAsync(TaskItem task);
        public Task UpdateAsync(TaskItem task);
        public Task DeleteAsync(int id, int UserId);
        public Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status, int UserId);
        public Task<IEnumerable<TaskItem>> GetByOverdueAsync(int UserId);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetAllAsync(int UserId)
        {
            return await _context.TaskItems.Where(u=> u.UserId == UserId ).ToListAsync();

        }
        public async Task<TaskItem> GetByIdAsync(int id, int UserId)
        {
            var result = await _context.TaskItems.FirstOrDefaultAsync(u=> u.Id == id && u.UserId == UserId);
            return result;
        }
        public async Task AddAsync(TaskItem task) 
        {  
             await _context.TaskItems.AddAsync(task);
            return;
        }
        public async Task UpdateAsync(TaskItem task) 
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id, int UserId)
        {
            var delete = await _context.TaskItems.FirstOrDefaultAsync(u=> u.Id == id && u.UserId== UserId);
            if (delete != null)
            {
                _context.TaskItems.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<TaskItem>> GetByStatusAsync(Status status, int UserId) 
        { 
            var result = await _context.TaskItems.Where(u=> u.Status == status && u.UserId== UserId).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<TaskItem>> GetByOverdueAsync(int UserId) 
        {
            var tasks = await _context.TaskItems.ToListAsync();
            return tasks.Where(t=> t.IsOverdue && t.UserId== UserId).ToList();
        }



    }


}
