using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerceG03.Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _conn;

        // DbConnect
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _conn = connectionMultiplexer.GetDatabase();
        }
        public async Task<CustomerBasket?> GetBasketByIdAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _conn.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket.ToString());
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var value = JsonSerializer.Serialize(basket);
            var result = await _conn.StringSetAsync(basket.Id, value, TimeToLive ?? TimeSpan.FromDays(7));
            return result ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await _conn.KeyDeleteAsync(basketId);
            return result;
        }

    }
}
