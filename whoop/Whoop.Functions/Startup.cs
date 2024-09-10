using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whoop.Commons.ServiceRegistrations;
using Whoop.Functions.Services;

[assembly: FunctionsStartup(typeof(Whoop.Functions.Startup))]

namespace Whoop.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .RegisterCosmosDb()
            .AddSingleton<CosmosDbOperations>()
            .AddOptions<WhoopSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("WhoopSettings").Bind(settings);
            });
    }
}