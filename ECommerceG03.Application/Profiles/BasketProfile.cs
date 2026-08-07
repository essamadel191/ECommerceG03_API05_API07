using AutoMapper;
using ECommerceG03.Application.DTOs.BasketDTOs;
using ECommerceG03.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Profiles
{
    public class BasketProfile : Profile    
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDto>().ReverseMap();
        }
    }
}
