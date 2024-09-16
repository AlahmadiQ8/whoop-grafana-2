using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Whoop.Commons.Services;

namespace Whoop.Functions;

public class WhoopOrchestrator(ProfileService profileService)
{
    private const string UserId = "18435265";

    [FunctionName(nameof(WhoopOrchestratorFunction))]
    public async Task WhoopOrchestratorFunction(
        [OrchestrationTrigger] IDurableOrchestrationContext context, 
        ILogger log)
    {
        var userId = context.GetInput<OrchestratorInput>().UserId;
        await context.CallActivityAsync(nameof(RefreshTokenActivity), userId);
        log.LogInformation("inside Orchestrator completed for userId '{userId}'", userId);
    }

    [FunctionName(nameof(RefreshTokenActivity))]
    public async Task RefreshTokenActivity([ActivityTrigger] string userId, ILogger log)
    {
        await profileService.UpdateTokenAsync(userId);
    }
    
    [FunctionName(nameof(MyTestFunction))]
    public async Task<IActionResult> MyTestFunction(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        [DurableClient] IDurableOrchestrationClient starter,
        ILogger log)
    {
        var instanceId = await starter.StartNewAsync(nameof(WhoopOrchestratorFunction), new OrchestratorInput { UserId = UserId });
        log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);
        return starter.CreateCheckStatusResponse(req, instanceId);
    }
}

public class OrchestratorInput
{
    public string UserId { get; init; }
}