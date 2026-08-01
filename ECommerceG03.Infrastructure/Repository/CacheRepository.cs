using ECommerceG03.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Repository
{
    public class CacheRepository : ICacheRepository
    {
        // Redis Database 
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheKey)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public Task SetAsync(string cacheKey, string cacheValue, TimeSpan? timeToLive = null)
        {
            return _database.StringSetAsync(cacheKey,cacheValue,timeToLive ?? TimeSpan.FromDays(1));
        }
    }
}
