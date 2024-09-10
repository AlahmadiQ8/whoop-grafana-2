using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client;
using Whoop.Sdk.Client.Auth;

namespace Whoop.Console.ServiceRegristrations;

public static class WhoopClientRegistrations
{
    public static void RegisterCycleApiClient(this IServiceCollection services)
    {
        services.AddSingleton<ICycleApi>(implementationFactory: static sp => new CycleApi(sp.GetClientConfiguration()));
        services.AddSingleton<IUserApi>(implementationFactory: static sp => new UserApi(sp.GetClientConfiguration()));
        
        services.AddSingleton<ITokenRefresher>(
            implementationFactory: static sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var client = new OAuthAuthenticator(
                    refreshToken: "LusXbcWWBiCvBe4rdKsJ58n0Zg7T7K2rPU8h_2u2c_Q.unrASLmsL2GzLkFl4KExdHgD1rA1EwFxnxgV6XYKwqg",
                    tokenUrl: "https://api.prod.whoop.com/oauth/oauth2/token",
                    clientId: configuration.GetValue<string>("WhoopSettings:ClientId")!,
                    clientSecret: configuration.GetValue<string>("WhoopSettings:ClientSecret")!,
                    scope: "offline",
                    flow: OAuthFlow.REFRESH_TOKEN,
                    serializerSettings: new JsonSerializerSettings(),
                    configuration: new Configuration()
                    );
                

                return client;
            });
    }

    private static Configuration GetClientConfiguration(this IServiceProvider sp)
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var clientId = configuration.GetValue<string>("WhoopSettings:ClientId"); 
        var clientSecret = configuration.GetValue<string>("WhoopSettings:ClientSecret");
        ArgumentNullException.ThrowIfNull(clientId);
        ArgumentNullException.ThrowIfNull(clientSecret);

        return new Configuration
        {
            AccessToken = "PSq0gagKJnp_ngutsGlvlgLI2yKIJ6pdvs_RMcQIamc.QDJja7G2mhgm9DMvBcb3AlBUTXafUFEnJq1LdRegOtk"
        };
    }
}