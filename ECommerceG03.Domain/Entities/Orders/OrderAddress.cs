using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Entities.Orders
{
    // Owned Entity for Order
    public class OrderAddress
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
