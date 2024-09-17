using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

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
        
        var tasks = cycleIds.Select(cycleId => recoveryApi.GetRecoveryForCycleAsync(long.Parse(cycleId))).ToList();

        try
        {
            var results = await Task.WhenAll(tasks);
            await cosmosDbOperations.BulkUpdateCyclesWithRecoveryDataAsync(results);
        
            return results.Select(r => r.SleepId.ToString()).ToList();
        }
        catch (AggregateException aggEx)
        {
            foreach (var ex in aggEx.InnerExceptions)
            {
                if (ex is ApiException)
                {
                    logger.LogInformation("Caught ApiException: {errorMessage}", new { errorMessage = ex.Message});
                    // Handle InvalidOperationException
                }
                else
                {
                    // Rethrow other exceptions
                    throw;
                }
            }
            throw;
        }
    }
}