using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whoop.Console;
using Whoop.Core;
using Whoop.Core.ServiceRegistrations;
using Whoop.Core.Services;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");
appBuilder.Services.Configure<WhoopSettings>(appBuilder.Configuration.GetSection("WhoopSettings"));
appBuilder.Services.AddSingleton<ConsoleApp>();
appBuilder.Services.AddSingleton<CosmosDbOperations>();
appBuilder.Services.AddSingleton<WhoopServices>();
appBuilder.Services.AddSingleton<ProfileService>();
appBuilder.Services.AddSingleton<CyclesService>();
appBuilder.Services.RegisterCosmosDb();

using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

// await consoleApp.Authenticate(args);
await consoleApp.Run(args);
