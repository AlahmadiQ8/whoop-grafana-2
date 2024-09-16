using System.Threading.Tasks;

namespace Whoop.Sdk.Client.Auth;

public interface ITokenRefresher
{
    Task<TokenResponse> GetTokenAsync();
}