using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace Whoop.Core.Services;

public class CosmosDbOperations(
    ILogger<CosmosDbOperations> logger, 
    Container container)
{
    public async Task<CycleDto?> GetLastInsertedCycleAsync()
    {
        var linqFeed  = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.Type == Type.Cycle)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults 
            ? null 
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }

    public async Task<ProfileDto?> GetProfileAsync(string userId)
    {
        var linkFeed = container
            .GetItemLinqQueryable<ProfileDto>()
            .Where(c => c.Type == Type.Profile)
            .Where(c => c.Id == userId)
            .ToFeedIterator();

        var result = (await linkFeed.ReadNextAsync()).FirstOrDefault();

        return result;
    }

    public async Task UpsertProfileAsync(ProfileDto profile)
    {
        await container.UpsertItemAsync(profile, new PartitionKey(profile.Id));
    }
    
    public async Task BulkUpsertCyclesAsync(IEnumerable<CycleDto> items)
    {
        var tasks = items
            .Select(cycleDto => container.UpsertItemAsync(cycleDto, new PartitionKey(cycleDto.Id)))
            .Cast<Task>()
            .ToList();

        await Task.WhenAll(tasks);
    }
}