using ECommerceG03.Application.Common;
using ECommerceG03.Application.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketByIdAsync(string basketId, CancellationToken ct = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? TimeToLive = default, CancellationToken ct = default);
        Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
