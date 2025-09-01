public class InMemoryTaskRepository : ITaskRepository
{
    private static List<TaskItem> _tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Description = "Review new PRs", Status = "Pending" },
        new TaskItem { Id = 2, Description = "Run data cleanup job", Status = "Completed" },
        new TaskItem { Id = 3, Description = "Archive old logs", Status = "Pending" }
    };

    public Task<List<TaskItem>> GetPendingTasksAsync()
    {
        return Task.FromResult(_tasks.Where(t => t.Status == "Pending").ToList());
    }

    public Task UpdateTaskAsync(TaskItem task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            existingTask.Status = task.Status;
        }
        return Task.CompletedTask;
    }
}