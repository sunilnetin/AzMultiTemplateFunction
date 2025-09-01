using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerTriggerFunction;

public class TimerDemo
{
    private readonly ILogger _logger;
    private readonly ITaskRepository _taskRepository;
    public TimerDemo(ILoggerFactory loggerFactory, ITaskRepository taskRepository)
    {
        _logger = loggerFactory.CreateLogger<TimerDemo>();
        _taskRepository = taskRepository;
    }
 [Function("ProcessPendingTasks")]
    // The cron expression is retrieved from an application setting for flexibility.
    //[TimerTrigger("%PendingTasksSchedule%")] 
    public async Task RunAsync([TimerTrigger("%PendingTasksSchedule%")] TimerInfo timerInfo)
    {
        // Log information about the timer trigger and its schedule.
        _logger.LogInformation($"Pending Task Processor triggered at: {DateTime.UtcNow}");
        _logger.LogInformation($"Next timer schedule: {timerInfo.ScheduleStatus?.Next}");

        // Handle cases where an execution was missed.
        if (timerInfo.IsPastDue)
        {
            _logger.LogWarning("Timer trigger is running late! read about cold start here.");
        }

        try
        {
            // --- Real-world application logic begins here ---

            // 1. Retrieve the list of pending tasks from the database.
            var pendingTasks = await _taskRepository.GetPendingTasksAsync();

            if (pendingTasks.Count == 0)
            {
                _logger.LogInformation("No pending tasks found. Exiting.");
                return;
            }
            
            _logger.LogInformation($"Found {pendingTasks.Count} pending tasks to process.");

            // 2. Process each pending task asynchronously.
            var tasks = pendingTasks.Select(task => ProcessSingleTaskAsync(task)).ToList();
            await Task.WhenAll(tasks);
            
            _logger.LogInformation("All pending tasks processed successfully.");
        }
        catch (Exception ex)
        {
            // 3. Log any exceptions for debugging.
            _logger.LogError(ex, "An error occurred while processing pending tasks.");
            // You could also send an alert or notification here.
        }
    }

    private async Task ProcessSingleTaskAsync(TaskItem task)
    {
        // Simulate processing the task (e.g., calling an API, sending an email).
        _logger.LogInformation($"Processing task ID: {task.Id}");
        await Task.Delay(100); // Simulate async work
        _logger.LogInformation($"Finished processing task ID: {task.Id}");
        
        // 4. Update the task status in the database.
        task.Status = "Completed";
        await _taskRepository.UpdateTaskAsync(task);
    }
}

    /*
    [Function("TimerDemo")]    
    public void Run([TimerTrigger("*0 *'/5 * * * *")] TimerInfo myTimer)
    {/*
    --The cron expression [TimerTrigger("0 *'/5 * * * *")] in Azure Functions schedules a job to run every five minutes, at the top of the minute. 
    --This is a powerful feature for automating tasks like data cleanup, reporting, or health checks. 
    --A timer trigger cron expression has six parts, representing the following units of time:
{second} {minute} {hour} {day} {month} {day-of-week}
        *
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }*/
