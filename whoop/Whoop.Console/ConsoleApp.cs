using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Whoop.Console;

public class ConsoleApp(IConfiguration configuration, ILogger<ConsoleApp> logger)
{
    public void Run(string[] args)
    {
        logger.LogInformation("Starting application");
        logger.LogDebug("Debugging application");
        System.Console.WriteLine("Hello World!");
        System.Console.WriteLine(configuration.GetValue<string>("MyConfig"));
    }
}