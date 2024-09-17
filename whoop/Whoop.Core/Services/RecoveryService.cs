using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Model;

namespace Whoop.Core.Services;

public class RecoveryService(
    CosmosDbOperations cosmosDbOperations,
    ILogger<RecoveryService> logger
)
{
    public async Task<IList<string>> UpdateRecoveriesAsync(string userId, IList<string> cycleIds)
    {
        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);

        var recoveryApi = new RecoveryApi(new Configuration
        {
            AccessToken = profile.AccessToken,
        });

        var results = await FetchRecoveries(recoveryApi, cycleIds);
        logger.LogInformation("fetched recoveries: {recCount}", results.Count);

        await cosmosDbOperations.BulkUpdateCyclesWithRecoveryDataAsync(results);

        return results.Select(r => r.SleepId.ToString()).ToList();
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