using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whoop.Console;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");

appBuilder.Services.AddSingleton<ConsoleApp>();

Container container = new CosmosClientBuilder("https://whoop.documents.azure.com:443", new DefaultAzureCredential())
    .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
    .WithBulkExecution(true)
    .Build()
    .GetDatabase("whoop")
    .GetContainer("whoop-data");

appBuilder.Services.AddSingleton(container);

appBuilder.Services.AddSingleton<ICycleApi>(
    implementationFactory: static sp =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var clientId = configuration.GetValue<string>("WhoopApp:ClientId"); 
        var clientSecret = configuration.GetValue<string>("WhoopApp:ClientSecret");
        ArgumentNullException.ThrowIfNull(clientId);
        ArgumentNullException.ThrowIfNull(clientSecret);
        
        return new CycleApi(new Configuration
        {
            OAuthClientId = clientId,
            OAuthClientSecret = clientSecret,
            AccessToken = "dPRkzEfUwZG6BnwyZAK4VZHTtuwc-r1aUaSWtpO660U.sFSeyJCOujHPba_kUHwDTRC-fCFshDIkl0dtDs2IVJI"
        });
    }); 

using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

await consoleApp.Run(args);
