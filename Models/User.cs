namespace TaskManagementApp_MVC_.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string ?Name { get; set; }
        public string? Email { get; set; }
        public List<TaskItem> taskItems { get; set; }

    }
}
