using ECommerceG03.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Specification
{
    public class OrderSpecification : BaseSpecification<Order,Guid>
    {
        public OrderSpecification(string email):base(x => x.BuyerEmail == email)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderSpecification(string email,Guid orderId):base(x => x.BuyerEmail == email && x.Id == orderId)
        {
            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDescending(x => x.OrderDate);
        }
    }
}
