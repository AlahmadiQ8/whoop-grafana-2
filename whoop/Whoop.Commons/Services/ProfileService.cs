namespace Whoop.Commons.Services;

public class ProfileService(
    CosmosDbOperations cosmosDbOperations,
    WhoopServices whoopServices)
{
    public async Task UpdateTokenAsync(string userId)
    {
        var profile = await cosmosDbOperations.GetProfileAsync(userId);
        ArgumentNullException.ThrowIfNull(profile);
        var tokenResponse = await whoopServices.RefreshTokenAsync(profile);
        var newProfile = profile with { AccessToken = tokenResponse.AccessToken, RefreshToken = tokenResponse.RefreshToken };
        await cosmosDbOperations.UpsertProfileAsync(newProfile);
    }
}