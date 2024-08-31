using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Whoop.Console;

public class ConsoleApp(IConfiguration configuration, ILogger<ConsoleApp> logger)
{
    public void Run(string[] args)
    {
        System.Console.WriteLine("Hello World!");
        System.Console.WriteLine(configuration.GetValue<string>("WhoopApp:ClientSecret"));
    }
}