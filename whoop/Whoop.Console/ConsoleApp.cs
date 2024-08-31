using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

namespace Whoop.Console;

public class ConsoleApp(IConfiguration configuration, ILogger<ConsoleApp> logger, ICycleApi cycleApi)
{

    public async Task Run(string[] args)
    {
        var fetchLimit = configuration.GetValue<int>("RecordsFetchLimit");

        var totalCount = 0;
        string? nextToken = null;
        do
        {
            
            var res = await cycleApi.GetCycleCollectionAsync(
                limit: fetchLimit,
                nextToken: nextToken);
            nextToken = res.NextToken;
            totalCount += res.Records.Count;

            System.Console.WriteLine($"Total fetched so far: {totalCount} records");
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        } while(nextToken != null);
    }
}
