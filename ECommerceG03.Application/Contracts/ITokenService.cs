using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface ITokenService
    {
        string CreateToken(string userId,string email,string userName,IReadOnlyList<string> roles);
    }
}
