/*
 * WHOOP API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Whoop.Sdk.Client;
using Whoop.Sdk.Client.Auth;
using Whoop.Sdk.Model;

namespace Whoop.Sdk.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRecoveryApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PaginatedRecoveryResponse</returns>
        PaginatedRecoveryResponse GetRecoveryCollection(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PaginatedRecoveryResponse</returns>
        ApiResponse<PaginatedRecoveryResponse> GetRecoveryCollectionWithHttpInfo(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get the recovery for a cycle
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Recovery</returns>
        Recovery GetRecoveryForCycle(long cycleId, int operationIndex = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get the recovery for a cycle
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Recovery</returns>
        ApiResponse<Recovery> GetRecoveryForCycleWithHttpInfo(long cycleId, int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRecoveryApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PaginatedRecoveryResponse</returns>
        System.Threading.Tasks.Task<PaginatedRecoveryResponse> GetRecoveryCollectionAsync(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PaginatedRecoveryResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<PaginatedRecoveryResponse>> GetRecoveryCollectionWithHttpInfoAsync(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get the recovery for a cycle
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Recovery</returns>
        System.Threading.Tasks.Task<Recovery> GetRecoveryForCycleAsync(long cycleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Get the recovery for a cycle
        /// </remarks>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Recovery)</returns>
        System.Threading.Tasks.Task<ApiResponse<Recovery>> GetRecoveryForCycleWithHttpInfoAsync(long cycleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRecoveryApi : IRecoveryApiSync, IRecoveryApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class RecoveryApi : IRecoveryApi
    {
        private Whoop.Sdk.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RecoveryApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RecoveryApi(string basePath)
        {
            this.Configuration = Whoop.Sdk.Client.Configuration.MergeConfigurations(
                Whoop.Sdk.Client.GlobalConfiguration.Instance,
                new Whoop.Sdk.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Whoop.Sdk.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Whoop.Sdk.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Whoop.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public RecoveryApi(Whoop.Sdk.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Whoop.Sdk.Client.Configuration.MergeConfigurations(
                Whoop.Sdk.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Whoop.Sdk.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Whoop.Sdk.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Whoop.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoveryApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public RecoveryApi(Whoop.Sdk.Client.ISynchronousClient client, Whoop.Sdk.Client.IAsynchronousClient asyncClient, Whoop.Sdk.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Whoop.Sdk.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Whoop.Sdk.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Whoop.Sdk.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Whoop.Sdk.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Whoop.Sdk.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        ///  Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PaginatedRecoveryResponse</returns>
        public PaginatedRecoveryResponse GetRecoveryCollection(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0)
        {
            Whoop.Sdk.Client.ApiResponse<PaginatedRecoveryResponse> localVarResponse = GetRecoveryCollectionWithHttpInfo(limit, start, end, nextToken);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PaginatedRecoveryResponse</returns>
        public Whoop.Sdk.Client.ApiResponse<PaginatedRecoveryResponse> GetRecoveryCollectionWithHttpInfo(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0)
        {
            Whoop.Sdk.Client.RequestOptions localVarRequestOptions = new Whoop.Sdk.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Whoop.Sdk.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Whoop.Sdk.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (limit != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if (start != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "start", start));
            }
            if (end != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "end", end));
            }
            if (nextToken != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "nextToken", nextToken));
            }

            localVarRequestOptions.Operation = "RecoveryApi.GetRecoveryCollection";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (OAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PaginatedRecoveryResponse>("/v1/recovery", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetRecoveryCollection", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PaginatedRecoveryResponse</returns>
        public async System.Threading.Tasks.Task<PaginatedRecoveryResponse> GetRecoveryCollectionAsync(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            Whoop.Sdk.Client.ApiResponse<PaginatedRecoveryResponse> localVarResponse = await GetRecoveryCollectionWithHttpInfoAsync(limit, start, end, nextToken, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get all recoveries for a user, paginated. Results are sorted by start time of the related sleep in descending order.
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit">Limit on the number of recoveries returned (optional, default to 10)</param>
        /// <param name="start">Return recoveries that occurred after or during (inclusive) this time. If not specified, the response will not filter recoveries by a minimum time. (optional)</param>
        /// <param name="end">Return recoveries that intersect this time or ended before (exclusive) this time. If not specified, &#x60;end&#x60; will be set to &#x60;now&#x60;. (optional)</param>
        /// <param name="nextToken">Optional next token from the previous response to get the next page. If not provided, the first page in the collection is returned (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PaginatedRecoveryResponse)</returns>
        public async System.Threading.Tasks.Task<Whoop.Sdk.Client.ApiResponse<PaginatedRecoveryResponse>> GetRecoveryCollectionWithHttpInfoAsync(int? limit = default(int?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), string? nextToken = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            Whoop.Sdk.Client.RequestOptions localVarRequestOptions = new Whoop.Sdk.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Whoop.Sdk.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Whoop.Sdk.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (limit != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if (start != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "start", start));
            }
            if (end != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "end", end));
            }
            if (nextToken != null)
            {
                localVarRequestOptions.QueryParameters.Add(Whoop.Sdk.Client.ClientUtils.ParameterToMultiMap("", "nextToken", nextToken));
            }

            localVarRequestOptions.Operation = "RecoveryApi.GetRecoveryCollection";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (OAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PaginatedRecoveryResponse>("/v1/recovery", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetRecoveryCollection", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Get the recovery for a cycle
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Recovery</returns>
        public Recovery GetRecoveryForCycle(long cycleId, int operationIndex = 0)
        {
            Whoop.Sdk.Client.ApiResponse<Recovery> localVarResponse = GetRecoveryForCycleWithHttpInfo(cycleId);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get the recovery for a cycle
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Recovery</returns>
        public Whoop.Sdk.Client.ApiResponse<Recovery> GetRecoveryForCycleWithHttpInfo(long cycleId, int operationIndex = 0)
        {
            Whoop.Sdk.Client.RequestOptions localVarRequestOptions = new Whoop.Sdk.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Whoop.Sdk.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Whoop.Sdk.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("cycleId", Whoop.Sdk.Client.ClientUtils.ParameterToString(cycleId)); // path parameter

            localVarRequestOptions.Operation = "RecoveryApi.GetRecoveryForCycle";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (OAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Recovery>("/v1/cycle/{cycleId}/recovery", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetRecoveryForCycle", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        ///  Get the recovery for a cycle
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Recovery</returns>
        public async System.Threading.Tasks.Task<Recovery> GetRecoveryForCycleAsync(long cycleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {
            Whoop.Sdk.Client.ApiResponse<Recovery> localVarResponse = await GetRecoveryForCycleWithHttpInfoAsync(cycleId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  Get the recovery for a cycle
        /// </summary>
        /// <exception cref="Whoop.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="cycleId">ID of the cycle to retrieve</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Recovery)</returns>
        public async System.Threading.Tasks.Task<Whoop.Sdk.Client.ApiResponse<Recovery>> GetRecoveryForCycleWithHttpInfoAsync(long cycleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
        {

            Whoop.Sdk.Client.RequestOptions localVarRequestOptions = new Whoop.Sdk.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Whoop.Sdk.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Whoop.Sdk.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("cycleId", Whoop.Sdk.Client.ClientUtils.ParameterToString(cycleId)); // path parameter

            localVarRequestOptions.Operation = "RecoveryApi.GetRecoveryForCycle";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (OAuth) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Recovery>("/v1/cycle/{cycleId}/recovery", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetRecoveryForCycle", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
