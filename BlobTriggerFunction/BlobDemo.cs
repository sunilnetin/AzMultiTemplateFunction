using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobTriggerFunction;

public class BlobDemo
{
    private readonly ILogger<BlobDemo> _logger;

    public BlobDemo(ILogger<BlobDemo> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobDemo))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}