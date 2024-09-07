using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whoop.Console;
using Whoop.Console.ServiceRegristrations;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");
appBuilder.Services.AddSingleton<ConsoleApp>();
appBuilder.Services.RegisterCosmosDb();
appBuilder.Services.RegisterCycleApiClient();

using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

// await consoleApp.Authenticate(args);
await consoleApp.Run(args);
