/*
 * WHOOP API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Whoop.Sdk.Client.Auth
{
    /// <summary>
    /// An authenticator for OAuth2 authentication flows
    /// </summary>
    public class OAuthAuthenticator : AuthenticatorBase, ITokenRefresher
    {
        readonly string _tokenUrl;
        readonly string? _refreshToken;
        readonly string _clientId;
        readonly string _clientSecret;
        readonly string? _scope;
        readonly string _grantType;
        readonly JsonSerializerSettings _serializerSettings;
        readonly IReadableConfiguration _configuration;

        /// <summary>
        /// Initialize the OAuth2 Authenticator
        /// </summary>
        public OAuthAuthenticator(
            string tokenUrl,
            string clientId,
            string clientSecret,
            string? scope,
            OAuthFlow? flow,
            JsonSerializerSettings serializerSettings,
            IReadableConfiguration configuration,
            string? refreshToken = null) : base("")
        {
            _refreshToken = refreshToken;
            _tokenUrl = tokenUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scope = scope;

            switch (flow)
            {
                /*case OAuthFlow.ACCESS_CODE:
                    _grantType = "authorization_code";
                    break;
                case OAuthFlow.IMPLICIT:
                    _grantType = "implicit";
                    break;
                case OAuthFlow.PASSWORD:
                    _grantType = "password";
                    break;*/
                case OAuthFlow.APPLICATION:
                    _grantType = "client_credentials";
                    break;
                case OAuthFlow.REFRESH_TOKEN:
                    _grantType = "refresh_token";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates an authentication parameter from an access token.
        /// </summary>
        /// <param name="accessToken">Access token to create a parameter from.</param>
        /// <returns>An authentication parameter.</returns>
        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the token from the OAuth2 server.
        /// </summary>
        /// <returns>An authentication token.</returns>
        public async Task<TokenResponse> GetTokenAsync()
        {
            var client = new RestClient(_tokenUrl,
                configureSerialization: serializerConfig => serializerConfig.UseSerializer(() => new CustomJsonCodec(_serializerSettings, _configuration)));

            var request = new RestRequest()
                .AddParameter("grant_type", _grantType)
                .AddParameter("client_id", _clientId)
                .AddParameter("client_secret", _clientSecret)
                .AddParameter("refresh_token", _refreshToken);

            if (!string.IsNullOrEmpty(_scope))
            {
                request.AddParameter("scope", _scope);
            }

            var response = await client.PostAsync<TokenResponse>(request).ConfigureAwait(false);

            return response;
        }
    }
}
