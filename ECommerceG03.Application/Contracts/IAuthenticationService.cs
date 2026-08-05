using ECommerceG03.Application.Common;
using ECommerceG03.Application.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDtos registerDtos, CancellationToken ct = default);
        Task<Result<bool>> DeleteUserByUsernameAsync(string userName, CancellationToken ct);
    }
}
