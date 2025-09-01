using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Extensions.Logging;
using System.Net;

namespace HttpTriggerFunction;

public class HttpDemo
{
    private readonly ILogger<HttpDemo> _logger;
    private readonly IGreetingService _greetingService;
    public HttpDemo(ILogger<HttpDemo> logger,IGreetingService greetingService)
    {
        _logger = logger;
        _greetingService = greetingService;
    }
[Function("HttpDemo")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        // Create the response object using HttpRequestData.
        var response = req.CreateResponse(HttpStatusCode.OK);
        
        // Write the body to the response using isolated worker methods.
        await response.WriteStringAsync(_greetingService.Greet("Developer"));
        
        return response;
    }

    /*[Function("HttpDemo")]--this is outo generated code
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        // _logger.LogInformation("C# HTTP trigger function processed a request.");
        // return new OkObjectResult("Welcome to Azure Functions!");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString(_greetingService.Greet("Developer"));
        return response;

    }*/
}
