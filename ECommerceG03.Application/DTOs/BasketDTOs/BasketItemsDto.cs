using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceG03.Application.DTOs.BasketDTOs
{
    public class BasketItemsDto
    {
        [Required(ErrorMessage = "Product Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; } = default!;
        [Required(ErrorMessage = "Picture URL is required.")]
        public string ProductUrl { get; set; } = default!;

        [Range(1,double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(1,50, ErrorMessage = "Quantity must be between 1 and 50.")]
        public int Quantity { get; set; }
    }
}
