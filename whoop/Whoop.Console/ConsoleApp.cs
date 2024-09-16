using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Commons;
using Whoop.Commons.Services;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Client.Auth;
using Type = Whoop.Commons.Type;

namespace Whoop.Console;

public class ConsoleApp(
    IConfiguration configuration, 
    ILogger<ConsoleApp> logger, 
    Container container,
    CosmosDbOperations cosmosDbOperations,
    ProfileService profileService)
{
    private const string UserId = "18435265";

    public async Task Run(string[] args)
    {
        var fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");

        var totalCount = 0;
        string? nextToken = null;

        var lastInsertedCycleStartTimeMinus1 = (await GetLastInsertedCycleAsync())?.Start.Subtract(TimeSpan.FromDays(1));
        
        if (lastInsertedCycleStartTimeMinus1 != null)
            logger.LogInformation("Last inserted cycle was at {start}", lastInsertedCycleStartTimeMinus1);
        else
            logger.LogInformation("No items found");

        var profile = await cosmosDbOperations.GetProfileAsync(UserId);
        ArgumentNullException.ThrowIfNull(profile);
        
        // var userApi = new UserApi(new Configuration { AccessToken = profile.AccessToken });
        // var userProfile = await userApi.GetProfileBasicAsync();
        var cycleApi = new CycleApi(new Configuration { AccessToken = profile.AccessToken });
        
        do
        {
            var res = await cycleApi.GetCycleCollectionAsync(
                limit: fetchLimit,
                start: lastInsertedCycleStartTimeMinus1,
                nextToken: nextToken);
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);

            await BulkUpsertAsync(res.Records.Select(r => r.ToCycleDto(profile)));
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }

    public async Task RefreshToken(string[] args)
    {
        await profileService.UpdateTokenAsync(UserId);
    }

    private async Task BulkUpsertAsync(IEnumerable<CycleDto> items)
    {
        var tasks = items
            .Select(cycleDto => container.UpsertItemAsync(cycleDto, new PartitionKey(cycleDto.Id)))
            .Cast<Task>()
            .ToList();

        await Task.WhenAll(tasks);
    }

    private async Task<CycleDto?> GetLastInsertedCycleAsync()
    {
        var linqFeed  = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.Type == Type.Cycle)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults 
            ? null 
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }
}
