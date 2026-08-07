using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.DTOs.OrderDtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
