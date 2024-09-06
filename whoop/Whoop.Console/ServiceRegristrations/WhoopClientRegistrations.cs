using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;

namespace Whoop.Console.ServiceRegristrations;

public static class WhoopClientRegistrations
{
    public static void RegisterCycleApiClient(this IServiceCollection services)
    {
        services.AddSingleton<ICycleApi>(implementationFactory: static sp => new CycleApi(sp.GetClientConfiguration()));
        services.AddSingleton<IUserApi>(implementationFactory: static sp => new UserApi(sp.GetClientConfiguration()));
    }

    private static Configuration GetClientConfiguration(this IServiceProvider sp)
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var clientId = configuration.GetValue<string>("WhoopApp:ClientId"); 
        var clientSecret = configuration.GetValue<string>("WhoopApp:ClientSecret");
        ArgumentNullException.ThrowIfNull(clientId);
        ArgumentNullException.ThrowIfNull(clientSecret);

        return new Configuration
        {
            OAuthClientId = clientId,
            OAuthClientSecret = clientSecret,
            AccessToken = "mpNQ8CNirARM_hhORXK-D47EWBW2FNt-Piq93hO_xCA.MopCQ8nT1FEHrcMLfirsyart3LYK01MHtW2jg7jTq9k"
        };
    }
}