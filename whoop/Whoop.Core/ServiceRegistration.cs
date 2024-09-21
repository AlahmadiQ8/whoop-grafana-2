using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;
using Whoop.Core.Services;

namespace Whoop.Core;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterCosmosDb(this IServiceCollection services)
    {
        var client = new CosmosClientBuilder("https://whoop.documents.azure.com:443", new DefaultAzureCredential())
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .WithBulkExecution(true)
            .Build()
            .GetDatabase("whoop")
            .GetContainer("whoop-data");
        
        services.AddSingleton(client);

        return services;
    }

    public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<CosmosDbOperations>()
            .AddSingleton<ProfileService>()
            .AddSingleton<CyclesService>()
            .AddSingleton<WhoopServices>()
            .AddSingleton<RecoveryService>()
            .AddSingleton<SleepService>()
            .AddSingleton<WorkoutService>();
    }
}