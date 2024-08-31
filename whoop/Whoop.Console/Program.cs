using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Whoop.Console;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Configuration.AddJsonFile("appsettings.local.json");
appBuilder.Services.AddSingleton<ConsoleApp>();
using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

consoleApp.Run(args);
