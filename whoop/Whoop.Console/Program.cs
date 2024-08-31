using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Whoop.Console;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");

appBuilder.Services.AddSingleton<ConsoleApp>();
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
            AccessToken = "bzaPNH2WvjygCaouKyzBeC1-KNlil8WTQ3PnC3hZ5P0.ci0ipl6weeBRhZP0KQDIQrns9yuMWsK_xm3cSBZO3iI"
        });
    }); 

using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

await consoleApp.Run(args);
