using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItems> Items { get; set; } = default!;
    }
}
