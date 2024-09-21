using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Model;

namespace Whoop.Core.Services;

public class CyclesService(
    ILogger<CyclesService> logger,
    CosmosDbOperations cosmosDbOperations,
    IConfiguration configuration)
{
    private readonly int _fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");
    private readonly int _daysFromLastInserted = configuration.GetValue<int>("DaysFromLastInserted");

    public async Task UpdateCyclesAsync(string userId)
    {
        var totalCount = 0;
        string? nextToken = null;

        var lastInsertedCycleStartTimeMinus1 =
            (await cosmosDbOperations.GetLastInsertedCycleAsync())?.Start.Subtract(
                TimeSpan.FromDays(_daysFromLastInserted));

        if (lastInsertedCycleStartTimeMinus1 != null)
            logger.LogInformation("Last inserted cycle was at {start}", lastInsertedCycleStartTimeMinus1);
        else
            logger.LogInformation("No items found");

        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);

        var cycleApi = new CycleApi(new Configuration { AccessToken = profile.AccessToken });

        do
        {
            var res = await cycleApi.GetCycleCollectionAsync(
                limit: _fetchLimit,
                start: lastInsertedCycleStartTimeMinus1,
                nextToken: nextToken);
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);

            await cosmosDbOperations.BulkUpsertItemsAsync(
                res.Records
                    .Where(r => r.ScoreState == Cycle.ScoreStateEnum.SCORED)
                    .Select(r => r.ToCycleDto())
                );
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while (nextToken != null);
    }
}