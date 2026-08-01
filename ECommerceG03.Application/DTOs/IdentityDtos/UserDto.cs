using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.DTOs.IdentityDtos
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
