using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetDataAsync(string cacheKey);
        Task SetDataAsync (string cacheKey, object cacheValue,TimeSpan? timeToLike = default);
    }
}
