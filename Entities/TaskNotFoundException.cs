namespace TaskManagementApp_MVC_.Entities
{
    public class TaskNotFoundException:Exception
    {
        public int TaskId { get; private set; }
        public TaskNotFoundException(int taskId): base($"The task with the taskId{taskId} was not found.")
        {
            TaskId = taskId;
        }
    }
}
