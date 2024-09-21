using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whoop.Console;
using Whoop.Core;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");
appBuilder.Services.Configure<WhoopSettings>(appBuilder.Configuration.GetSection("WhoopSettings"));
appBuilder.Services.AddSingleton<ConsoleApp>();
appBuilder.Services.RegisterCoreServices();
appBuilder.Services.RegisterCosmosDb();

using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

// await consoleApp.Authenticate(args);
await consoleApp.Run(args);
