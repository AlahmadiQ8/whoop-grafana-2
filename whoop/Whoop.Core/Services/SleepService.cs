using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

namespace Whoop.Core.Services;

public class SleepService(
    CosmosDbOperations cosmosDbOperations,
    ILogger<SleepService> logger,
    IConfiguration configuration)
{
    private readonly int _fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");
    private readonly int _daysFromLastInserted = configuration.GetValue<int>("DaysFromLastInserted");

    public async Task UpdateSleepAsync(string userId)
    {
        var totalCount = 0;
        string? nextToken = null;
        
        var lastInsertedCycleSleepDataStartTimeMinusXDays = (await cosmosDbOperations.GetLastInsertedCycleWithSleepDataAsync())?.Start.Subtract(TimeSpan.FromDays(_daysFromLastInserted));
        
        if (lastInsertedCycleSleepDataStartTimeMinusXDays != null)
            logger.LogInformation("Last inserted cycle with sleep data was at {start}", lastInsertedCycleSleepDataStartTimeMinusXDays);
        else
            logger.LogInformation("No items with sleep data found");
        
        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);
        
        var sleepApi = new SleepApi(new Configuration { AccessToken = profile.AccessToken });

        do
        {
            var res = await sleepApi.GetSleepCollectionAsync(
                limit: _fetchLimit,
                start: lastInsertedCycleSleepDataStartTimeMinusXDays,
                nextToken: nextToken);
            
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);
            
            await cosmosDbOperations.BulkUpdateCyclesWithSleepDataAsync(res.Records);
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }
}