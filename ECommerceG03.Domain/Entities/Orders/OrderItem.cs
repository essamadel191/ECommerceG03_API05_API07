using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
