using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CosmosDBTriggerFunction;

public class CosmosDBDemo
{
    private readonly ILogger<CosmosDBDemo> _logger;

    public CosmosDBDemo(ILogger<CosmosDBDemo> logger)
    {
        _logger = logger;
    }

    [Function("CosmosDBDemo")]
    public void Run([CosmosDBTrigger(
        databaseName: "databaseName",
        containerName: "containerName",
        Connection = "",
        LeaseContainerName = "leases",
        CreateLeaseContainerIfNotExists = true)] IReadOnlyList<MyDocument> input)
    {
        if (input != null && input.Count > 0)
        {
            _logger.LogInformation("Documents modified: " + input.Count);
            _logger.LogInformation("First document Id: " + input[0].id);
        }
    }
}

public class MyDocument
{
    public string id { get; set; }

    public string Text { get; set; }

    public int Number { get; set; }

    public bool Boolean { get; set; }
}