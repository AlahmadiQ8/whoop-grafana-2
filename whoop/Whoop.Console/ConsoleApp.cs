using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Commons;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client.Auth;
using Type = Whoop.Commons.Type;

namespace Whoop.Console;

public class ConsoleApp(
    IConfiguration configuration, 
    ILogger<ConsoleApp> logger, 
    ICycleApi cycleApi,
    IUserApi userApi,
    ITokenRefresher tokenRefresher,
    Container container)
{

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

        var userProfile = await userApi.GetProfileBasicAsync();
        
        do
        {
            var res = await cycleApi.GetCycleCollectionAsync(
                limit: fetchLimit,
                start: lastInsertedCycleStartTimeMinus1,
                nextToken: nextToken);
            nextToken = res.NextToken;
            totalCount += res.Records.Count;
            logger.LogInformation("Total fetched so far: {totalCount} records", totalCount);

            await BulkUpsertAsync(res.Records.Select(r => r.ToCycleDto(userProfile)));
            logger.LogInformation("Upserted so far: {totalCount} records", totalCount);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }

    public async Task Authenticate(string[] args)
    {
        var token = await tokenRefresher.GetToken();
        System.Console.WriteLine($"Token: {token}");
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
            .Where(c => c.type == Type.Cycle)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults 
            ? null 
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }
}
