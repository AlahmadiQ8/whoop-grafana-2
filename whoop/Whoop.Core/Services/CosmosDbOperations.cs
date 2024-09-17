using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Model;

namespace Whoop.Core.Services;

public class CosmosDbOperations(
    ILogger<CosmosDbOperations> logger,
    Container container)
{
    public async Task<CycleDto?> GetLastInsertedCycleAsync()
    {
        var linqFeed = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.Type == Type.Cycle)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults
            ? null
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }

    public async Task<CycleDto?> GetLastInsertedCycleWithRecoveryDataAsync()
    {
        var linqFeed = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.Type == Type.Cycle)
            .Where(c => c.RecoveryScore != null)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults
            ? null
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }

    public async Task<CycleDto?> GetLastInsertedCycleWithSleepDataAsync()
    {
        var linqFeed = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.Type == Type.Cycle)
            .Where(c => c.SleepScore != null)
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

    public async Task BulkUpdateCyclesWithRecoveryDataAsync(IList<Recovery> recoveries)
    {
        var tasks = recoveries.Select(recovery =>
                container.PatchItemAsync<CycleDto>(
                    id: recovery.CycleId.ToString(),
                    partitionKey: new PartitionKey(recovery.CycleId.ToString()),
                    patchOperations:
                    [
                        PatchOperation.Set("/sleepId", recovery.SleepId.ToString()),
                        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                        PatchOperation.Set("/recoveryScore", recovery.Score?.ToRecoveryScoreDto())
                    ]
                )
            )
            .Cast<Task>()
            .ToList();

        await Task.WhenAll(tasks);
    }

    public async Task BulkUpdateCyclesWithSleepDataAsync(IList<Sleep> sleeps)
    {
        var cycles = await GetCyclesFromSleepIdsAsync(sleeps.Select(s => s.Id.ToString()).ToList());

        var tasks = cycles.Select(cycle =>
            container.PatchItemAsync<CycleDto>(
                id: cycle.Id,
                partitionKey: new PartitionKey(cycle.Id.ToString()),
                patchOperations: [
                    PatchOperation.Set("/sleepScore", sleeps.First(s => s.Id.ToString() == cycle.SleepId).ToSleepDto())
                ]
            )
        ).Cast<Task>()
        .ToList();
        
        await Task.WhenAll(tasks);
    }

    private async Task<IList<CycleDto>> GetCyclesFromSleepIdsAsync(IList<string> sleepIds)
    {
        var query = new QueryDefinition(query: "SELECT * FROM c where ARRAY_CONTAINS(@ids, c.sleepId)")
            .WithParameter("@ids", sleepIds);

        using FeedIterator<CycleDto> filteredFeed = container.GetItemQueryIterator<CycleDto>(
            queryDefinition: query
        );

        var cycles = new List<CycleDto>();

        while (filteredFeed.HasMoreResults)
        {
            var response = await filteredFeed.ReadNextAsync();
            cycles.AddRange(response);
        }

        return cycles;
    }
}