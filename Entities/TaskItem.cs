using TaskManagementApp_MVC_.Models;

namespace TaskManagementApp_MVC_.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsOverdue => Status != Status.Completed && DueDate < DateTime.Now;


}
public enum Priority
{
    Low,
    Medium,
    High,
}
public enum Status
{
    Pending,
    InProgress,
    Completed,

}
