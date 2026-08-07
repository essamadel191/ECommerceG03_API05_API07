using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.DTOs.OrderDtos
{
    public class DeliveryMethodDto
    {
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
