using AutoMapper;
using ECommerceG03.Application.DTOs.IdentityDtos;
using ECommerceG03.Application.DTOs.OrderDtos;
using ECommerceG03.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //CreateMap<OrderToReturnDto, Order>()
            //    .ForMember(dest => dest.Status, 
            //    opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)));

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ProductUrl, opt => opt.MapFrom<OrderPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

        }
    }
}
