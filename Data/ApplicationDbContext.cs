using Microsoft.EntityFrameworkCore;
using TaskManagementApp_MVC_.Models
namespace TaskManagementApp_MVC_.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){ }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems {  get; set; }
    }
}
