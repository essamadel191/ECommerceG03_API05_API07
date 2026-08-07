using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.DTOs.BasketDTOs
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemsDto> Items { get; set; } = default!;
    }
}
