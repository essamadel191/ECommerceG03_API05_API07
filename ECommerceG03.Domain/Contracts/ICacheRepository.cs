using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Contracts
{
    public interface ICacheRepository
    {
        // Get Cache
        // Key => Endpoint
        Task<string?> GetAsync(string cacheKey);

        // Set Cache
        Task SetAsync(string cacheKey, string cacheValue,TimeSpan? timeToLive = default);

    }
}
