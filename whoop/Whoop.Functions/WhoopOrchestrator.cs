using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Whoop.Core;
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
        var input = context.GetInput<OrchestratorInput>();
        await context.CallActivityAsync(nameof(RefreshTokenActivity), input.UserId);

        var tasks = new List<Task>
        {
            // **********************************
            // Step a: Upsert denormalized cycles
            // **********************************
            UpsertDenormalizedCyclesActivity(context, input.UserId),
            
            // **********************************
            // Step b: Upsert workouts
            // **********************************
            context.CallActivityAsync(nameof(UpdateWorkoutActivity), input)
        };
        
        await Task.WhenAll(tasks);
        
        log.LogInformation("inside Orchestrator completed for userId '{userId}'", input.UserId);
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
    public async Task UpdateWorkoutActivity([ActivityTrigger] OrchestratorInput input, ILogger log)
    {
        await workoutService.UpdateWorkoutsAsync(input);
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
        var input = new OrchestratorInput { UserId = UserId };
        var start = req.Query["Start"];
        var end = req.Query["End"];

        if (!string.IsNullOrEmpty(start))
        {
            input.Start = DateTime.Parse(start);
        }

        if (!string.IsNullOrEmpty(end))
        {
            input.End = DateTime.Parse(end);
        }
        
        var instanceId = await starter.StartNewAsync(nameof(WhoopOrchestratorFunction), input);
        log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);
        return starter.CreateCheckStatusResponse(req, instanceId);
    }
}

public class RecoveryActivityInput
{
    public string UserId { get; init; }
    public IList<string> CycleIds { get; init; }
}
