using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

namespace Whoop.Console;

public class ConsoleApp(IConfiguration configuration, ILogger<ConsoleApp> logger, ICycleApi cycleApi)
{

    public async Task Run(string[] args)
    {
        System.Console.WriteLine("Hello World!");
        System.Console.WriteLine(configuration.GetValue<string>("WhoopApp:ClientSecret"));
        
        var res = await cycleApi.GetCycleCollectionAsync(
            limit: 10);
        
        foreach (var resRecord in res.Records)
        {
            System.Console.WriteLine(resRecord);
        }
    }
}