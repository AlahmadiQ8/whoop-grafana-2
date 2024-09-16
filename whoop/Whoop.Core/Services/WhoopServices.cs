using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Whoop.Sdk.Client;
using Whoop.Sdk.Client.Auth;

namespace Whoop.Core.Services;

public class WhoopServices(IOptions<WhoopSettings> options)
{
    private readonly WhoopSettings _settings = options.Value;
    
    public async Task<TokenResponse> RefreshTokenAsync(ProfileDto profile)
    {
        var clientId = _settings.ClientId;
        var clientSecret = _settings.ClientSecret;
        ArgumentNullException.ThrowIfNull(clientId);
        ArgumentNullException.ThrowIfNull(clientSecret);
        
        var client = new OAuthAuthenticator(
            tokenUrl: "https://api.prod.whoop.com/oauth/oauth2/token",
            clientId: clientId,
            clientSecret: clientSecret,
            scope: "offline",
            flow: OAuthFlow.REFRESH_TOKEN,
            serializerSettings: new JsonSerializerSettings(),
            configuration: new Configuration(),
            refreshToken: profile.RefreshToken
        );

        var tokenResponse = await client.GetTokenAsync();
        
        return tokenResponse;
    }
}