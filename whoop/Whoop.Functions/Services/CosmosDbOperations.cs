using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Whoop.Commons;
using Whoop.Sdk.Api;
using Whoop.Sdk.Client.Auth;
using Type = Whoop.Commons.Type;

namespace Whoop.Functions.Services;

public class CosmosDbOperations(
    IConfiguration configuration, 
    ILogger<CosmosDbOperations> logger, 
    ICycleApi cycleApi,
    IUserApi userApi,
    ITokenRefresher tokenRefresher,
    Container container)
{
    public async Task<CycleDto> GetLastInsertedCycleAsync()
    {
        var linqFeed  = container.GetItemLinqQueryable<CycleDto>()
            .Where(c => c.type == Type.Cycle)
            .OrderByDescending(c => c.Start)
            .ToFeedIterator();

        return !linqFeed.HasMoreResults 
            ? null 
            : (await linqFeed.ReadNextAsync()).FirstOrDefault();
    }
}