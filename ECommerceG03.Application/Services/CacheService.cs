using ECommerceG03.Application.Contracts;
using ECommerceG03.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerceG03.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetDataAsync(string cacheKey)
        {
            return await _cacheRepository.GetAsync(cacheKey);
        }

        public Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? timeToLike = default)
        {
            var jsonValue = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase   
            });

            return _cacheRepository.SetAsync(cacheKey, jsonValue, timeToLike ?? TimeSpan.FromDays(2) );
        }
    }
}
