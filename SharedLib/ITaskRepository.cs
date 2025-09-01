public interface ITaskRepository
{
    Task<List<TaskItem>> GetPendingTasksAsync();
    Task UpdateTaskAsync(TaskItem task);
}