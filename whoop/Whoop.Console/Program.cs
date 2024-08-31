using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Whoop.Console;

var appBuilder = Host.CreateApplicationBuilder(args);
appBuilder.Services.AddSingleton<ConsoleApp>();
using var app = appBuilder.Build();

var consoleApp = app.Services.GetRequiredService<ConsoleApp>();

consoleApp.Run(args);
