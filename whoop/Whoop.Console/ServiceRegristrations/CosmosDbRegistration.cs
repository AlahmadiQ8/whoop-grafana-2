using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;

namespace Whoop.Console.ServiceRegristrations;

public static class CosmosDbRegistration
{
    public static void RegisterCosmosDb(this IServiceCollection services)
    {
        var client = new CosmosClientBuilder("https://whoop.documents.azure.com:443", new DefaultAzureCredential())
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .WithBulkExecution(true)
            .Build()
            .GetDatabase("whoop")
            .GetContainer("whoop-data");
        
        services.AddSingleton(client);
    }
}