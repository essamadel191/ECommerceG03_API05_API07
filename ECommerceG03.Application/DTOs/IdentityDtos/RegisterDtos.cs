using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceG03.Application.DTOs.IdentityDtos
{
    public class RegisterDtos
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; } = default!;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage = "DisplayName is required")]
        public string DisplayName { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
    }
}
