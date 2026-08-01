using ECommerceG03.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> GetUserByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email,string password, CancellationToken ct = default);
    }
}
