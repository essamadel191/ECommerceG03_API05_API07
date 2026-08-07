using AutoMapper;
using ECommerceG03.Application.Common;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.OrderDtos;
using ECommerceG03.Application.Specification;
using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities.Orders;
using ECommerceG03.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct)
        {
            // Items [Order Items] => Basket
            #region Items[Order ITems]&Basket
            var basket = await _basketRepository.GetBasketByIdAsync(orderDto.BasketId, ct);
            if (basket == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found", "Basket with this Id Not Found"));

            if (basket.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Validation", "Basket is empty!"));

            var orderItems = new List<OrderItem>(basket.Items.Count);

            // From Database
            var productIds = basket.Items.Select(x => x.Id).ToHashSet();
            var products = (await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithIdSpecification(productIds), ct)).ToDictionary(x => x.Id); // I need Id only

            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found", "Product Not Found"));

                // Add Product to orderItems List
                orderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    // From Database
                    Product = new ProductItemOrder
                    {
                        PictureUrl = product.PictureUrl,
                        ProductId = product.Id,
                        ProductName = product.Name
                    },
                });
            } 
            #endregion

            // Ship to Address
            var orderAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);

            // Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found","Delivery Method not Found"));

            // Sub Total
            // Product * Quantity
            var subTotal = orderItems.Sum(x => x.Quantity * x.Price);

            // Create Order
            var order = new Order(email, orderAddress, orderItems, deliveryMethod,subTotal);
            _unitOfWork.GetRepository<Order,Guid>().Add(order);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Validation", "Cannot Add Order"));
            else
            {
                await _basketRepository.DeleteBasketAsync(orderDto.BasketId, ct);
                return Result<OrderToReturnDto>.Ok(_mapper.Map<OrderToReturnDto>(order));
            }

        }
        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetOrdersForSpecificUserAsync(string email, CancellationToken ct = default)
        {
            var orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(new OrderSpecification(email) ,ct);
            if (orders.Any())
                return Result<IReadOnlyList<OrderToReturnDto>>.Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            
            else
                return Result<IReadOnlyList<OrderToReturnDto>>.Fail(Error.NotFound());
        }

        public async Task<Result<OrderToReturnDto>> GetOrdersByIdAndUserEmailAsync(string email, Guid orderId, CancellationToken ct = default)
        {
            var order = await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(orderId,new OrderSpecification(email,orderId),ct );
            if (order == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound());
            else
                return Result<OrderToReturnDto>.Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            if (deliveryMethods.Any())
                return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(_mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
            else
                return Result<IReadOnlyList<DeliveryMethodDto>>.Fail(Error.NotFound("Not Found","Deliver Methods Not Found"));
        }
    }
}
