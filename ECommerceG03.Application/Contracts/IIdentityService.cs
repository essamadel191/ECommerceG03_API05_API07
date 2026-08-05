using ECommerceG03.Application.Common;
using ECommerceG03.Application.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> GetUserByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email,string password, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDtos registerDtos, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> GetUserByUsernameAsync(string username, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default);
        Task<Result<bool>> DeleteUserAsync(string Id, CancellationToken ct);
    }
}
