using ECommerceG03.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketByIdAsync(string basketId, CancellationToken ct = default);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null, CancellationToken ct = default);
        Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
