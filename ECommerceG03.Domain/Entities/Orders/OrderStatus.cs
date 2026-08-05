using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Entities.Orders
{
    public enum OrderStatus
    {
        Pending = 0,
        PaymentReceived = 1,
        PaymentFailed = 2,
    }
}
