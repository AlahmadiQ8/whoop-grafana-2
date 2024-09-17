using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Model;

namespace Whoop.Core.Services;

public class RecoveryService(
    CosmosDbOperations cosmosDbOperations,
    ILogger<RecoveryService> logger,
    IConfiguration configuration
)
{
    private readonly int _fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");
    private readonly int _daysFromLastInserted = configuration.GetValue<int>("DaysFromLastInserted");
    
    public async Task UpdateRecoveriesAsync(string userId)
    {
        var totalCount = 0;
        string? nextToken = null;
        
        var lastInsertedCycleNoRecoveryStartTimeMinus1 = (await cosmosDbOperations.GetLastInsertedCycleWithRecoveryDataAsync())?.Start.Subtract(TimeSpan.FromDays(_daysFromLastInserted));
        
        if (lastInsertedCycleNoRecoveryStartTimeMinus1 != null)
            logger.LogInformation("Last inserted cycle with recovery data was at {start}", lastInsertedCycleNoRecoveryStartTimeMinus1);
        else
            logger.LogInformation("No items found");
        
        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);
        
        var recoveryApi = new RecoveryApi(new Configuration { AccessToken = profile.AccessToken });

        do
        {
            var res = await recoveryApi.GetRecoveryCollectionAsync(
                limit: _fetchLimit,
                start: lastInsertedCycleNoRecoveryStartTimeMinus1,
                nextToken: nextToken);
            
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);
            
            await cosmosDbOperations.BulkUpdateCyclesWithRecoveryDataAsync(res.Records);
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }

    private async Task<List<Recovery>> FetchRecoveries(RecoveryApi recoveryApi, IList<string> cycleIds)
    {
        var tasks = cycleIds.Select(cycleId => recoveryApi.GetRecoveryForCycleWithHttpInfoAsync(long.Parse(cycleId)))
            .ToList();

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            logger.LogInformation("OMG Caught Exception: {ex} : {errorMessage}", ex, ex.Message);
        }

        return tasks.Where(r => r.Status == TaskStatus.RanToCompletion)
            .Select(r => r.Result.Data)
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            .Where(r => r != null)
            .ToList();
    }
}