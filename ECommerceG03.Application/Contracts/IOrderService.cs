using ECommerceG03.Application.Common;
using ECommerceG03.Application.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface IOrderService
    {
        // Create Order
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default);

        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetOrdersForSpecificUserAsync(string email, CancellationToken ct = default);
        Task<Result<OrderToReturnDto>> GetOrdersByIdAndUserEmailAsync(string email,Guid orderId, CancellationToken ct = default);
        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default);
    }
}
