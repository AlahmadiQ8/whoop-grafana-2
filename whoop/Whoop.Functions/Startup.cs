using System;
using System.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Whoop.Core;

[assembly: FunctionsStartup(typeof(Whoop.Functions.Startup))]

namespace Whoop.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .RegisterCosmosDb()
            .RegisterCoreServices()
            .AddOptions<WhoopSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("WhoopSettings").Bind(settings);
            });
        
        // See https://github.com/Azure/azure-functions-host/issues/4689#issuecomment-1064472477
        // Replace ILogger<T> with the one that works fine in all scenarios 
        var logger = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(ILogger<>));
        if (logger != null)
            builder.Services.Remove(logger);

        builder.Services.Add(new ServiceDescriptor(typeof(ILogger<>), typeof(FunctionsLogger<>), ServiceLifetime.Transient));
    }
    
    /// <summary>
    /// See https://github.com/Azure/azure-functions-host/issues/4689#issuecomment-1064472477
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class FunctionsLogger<T> : ILogger<T>
    {
        private readonly ILogger _logger;
        public FunctionsLogger(ILoggerFactory factory)
            // See https://github.com/Azure/azure-functions-host/issues/4689#issuecomment-533195224
            => _logger = factory.CreateLogger(LogCategories.CreateFunctionUserCategory(typeof(T).FullName));
        public IDisposable BeginScope<TState>(TState state) => _logger.BeginScope(state);
        public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => _logger.Log(logLevel, eventId, state, exception, formatter);
    }
}