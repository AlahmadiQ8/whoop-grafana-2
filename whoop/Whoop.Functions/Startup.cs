using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whoop.Commons;
using Whoop.Commons.ServiceRegistrations;
using Whoop.Commons.Services;

[assembly: FunctionsStartup(typeof(Whoop.Functions.Startup))]

namespace Whoop.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .RegisterCosmosDb()
            .AddSingleton<CosmosDbOperations>()
            .AddSingleton<ProfileService>()
            .AddSingleton<WhoopServices>()
            .AddOptions<WhoopSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("WhoopSettings").Bind(settings);
            });
    }
}