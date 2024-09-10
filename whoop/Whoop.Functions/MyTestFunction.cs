using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Whoop.Functions.Services;

namespace Whoop.Functions;

public class MyTestFunction(
    IOptions<WhoopSettings> options,
    CosmosDbOperations cosmosDbOperations
    )
{
    private readonly WhoopSettings _settings = options.Value;

    [FunctionName("MyTestFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
    {
        log.LogInformation("This is a log");
        log.LogInformation($"This is client id: {_settings.ClientId}");
        log.LogInformation($"This is client Secret: {_settings.ClientSecret}");

        var result = await cosmosDbOperations.GetLastInsertedCycleAsync();
        return new OkObjectResult(result);
    }
}