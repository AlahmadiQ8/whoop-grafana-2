using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Model;

namespace Whoop.Core.Services;

public class WorkoutService(    
    CosmosDbOperations cosmosDbOperations,
    ILogger<RecoveryService> logger,
    IConfiguration configuration)
{
    private readonly int _fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");
    private readonly int _daysFromLastInserted = configuration.GetValue<int>("DaysFromLastInserted");

    public async Task UpdateWorkoutsAsync(string userId)
    {
        var totalCount = 0;
        string? nextToken = null;
        
        var lastInsertedWorkoutStartTime = (await cosmosDbOperations.GetLastInsertedWorkoutAsync())?.Start.Subtract(TimeSpan.FromDays(_daysFromLastInserted));
        
        if (lastInsertedWorkoutStartTime != null)
            logger.LogInformation("Last inserted workout was at {start}", lastInsertedWorkoutStartTime);
        else
            logger.LogInformation("No items found");
        
        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);
        
        var workoutApi = new WorkoutApi(new Configuration { AccessToken = profile.AccessToken });
        
        do
        {
            var res = await workoutApi.GetWorkoutCollectionAsync(
                limit: _fetchLimit,
                start: lastInsertedWorkoutStartTime,
                nextToken: nextToken);
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);
            
            await cosmosDbOperations.BulkUpsertItemsAsync(res.Records
                .Where(w => w.ScoreState == Workout.ScoreStateEnum.SCORED)
                .Select(r => r.ToWorkoutDto()));
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }
}