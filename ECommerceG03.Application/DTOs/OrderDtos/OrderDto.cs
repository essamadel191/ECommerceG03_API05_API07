using ECommerceG03.Application.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.DTOs.OrderDtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; } = default!;
        public AddressDto ShipToAddress { get; set; } = default!;
    }
}
