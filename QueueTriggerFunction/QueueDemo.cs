using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueTriggerFunction;

public class QueueDemo
{
    private readonly ILogger<QueueDemo> _logger;

    public QueueDemo(ILogger<QueueDemo> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueDemo))]
    public void Run([QueueTrigger("myqueue-items", Connection = "")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}