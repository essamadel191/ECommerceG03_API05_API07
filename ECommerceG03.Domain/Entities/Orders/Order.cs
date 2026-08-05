using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        private Order() // EF Core
        {
    
        }

        public Order(string buyerEmail, OrderAddress shipToAddress, ICollection<OrderItem> items
            , DeliveryMethod deliveryMethod, int deliveryMethodId, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethodId;
            Subtotal = subtotal;
        }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string BuyerEmail { get; set; } = default!;

        // Own Entity and Need to be configured in fluent validations
        public OrderAddress ShipToAddress { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        
        // Relationship
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }

        public decimal Subtotal { get; set; }
        public decimal GetTotal() => Subtotal + (DeliveryMethod?.Price ?? 0);

    }
}
