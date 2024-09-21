using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Whoop.Core.Services;

namespace Whoop.Functions;

public class WhoopOrchestrator(ProfileService profileService, CyclesService cyclesService, RecoveryService recoveryService, SleepService sleepService, WorkoutService workoutService, ILogger<WhoopOrchestrator> logger)
{
    private const string UserId = "18435265";

    [FunctionName(nameof(WhoopOrchestratorFunction))]
    public async Task WhoopOrchestratorFunction(
        [OrchestrationTrigger] IDurableOrchestrationContext context,
        ILogger log)
    {
        // **********************************
        // Step 1: Refresh OAuth Token
        // **********************************
        var userId = context.GetInput<OrchestratorInput>().UserId;
        await context.CallActivityAsync(nameof(RefreshTokenActivity), userId);

        var tasks = new List<Task>
        {
            // **********************************
            // Step a: Upsert denormalized cycles
            // **********************************
            UpsertDenormalizedCyclesActivity(context, userId),
            
            // **********************************
            // Step b: Upsert workouts
            // **********************************
            context.CallActivityAsync(nameof(UpdateWorkoutActivity), UserId)
        };
        
        await Task.WhenAll(tasks);
        
        log.LogInformation("inside Orchestrator completed for userId '{userId}'", userId);
    }
    
    private async Task UpsertDenormalizedCyclesActivity(IDurableOrchestrationContext context, string userId)
    {
        // **********************************
        // Step a.1: Upsert Cycles
        // **********************************
        await context.CallActivityAsync(nameof(UpdateCyclesActivity), userId);
        
        // **********************************
        // Step a.2: Upsert Recoveries
        // **********************************
        await context.CallActivityAsync(nameof(UpdateRecoveryActivity), userId);
        
        // **********************************
        // Step a.3: Upsert Sleeps
        // **********************************
        await context.CallActivityAsync(nameof(UpdateSleepActivity), userId);
    }
    
    [FunctionName(nameof(UpdateWorkoutActivity))]
    public async Task UpdateWorkoutActivity([ActivityTrigger] string userId, ILogger log)
    {
        await workoutService.UpdateWorkoutsAsync(userId);
    }

    [FunctionName(nameof(RefreshTokenActivity))]
    public async Task RefreshTokenActivity([ActivityTrigger] string userId, ILogger log)
    {
        await profileService.UpdateTokenAsync(userId);
    }
    
    [FunctionName(nameof(UpdateCyclesActivity))]
    public async Task UpdateCyclesActivity([ActivityTrigger] string userId, ILogger log)
    {
        await cyclesService.UpdateCyclesAsync(userId);
    }
    
    [FunctionName(nameof(UpdateRecoveryActivity))]
    public async Task UpdateRecoveryActivity([ActivityTrigger] string userId, ILogger log)
    {
        await recoveryService.UpdateRecoveriesAsync(userId);
    }
    
    [FunctionName(nameof(UpdateSleepActivity))]
    public async Task UpdateSleepActivity([ActivityTrigger] string userId, ILogger log)
    {
        await sleepService.UpdateSleepAsync(userId);
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

public class RecoveryActivityInput
{
    public string UserId { get; init; }
    public IList<string> CycleIds { get; init; }
}
