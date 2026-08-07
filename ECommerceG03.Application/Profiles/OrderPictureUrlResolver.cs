using AutoMapper;
using ECommerceG03.Application.DTOs.OrderDtos;
using ECommerceG03.Domain.Entities.Orders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Profiles
{
    public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly UrlSettings _urlSettings;
        public OrderPictureUrlResolver(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            var path = source.Product.PictureUrl.TrimStart('/');

            return $"{baseUrl}/Files/{path}";
        }
    }
}
