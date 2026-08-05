using ECommerceG03.Application.Common;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<Result<bool>> DeleteUserByUsernameAsync(string userName, CancellationToken ct)
        {
            var userResult = await _identityService.GetUserByUsernameAsync(userName, ct);
            var deleteResult = await _identityService.DeleteUserAsync(userResult.data.Id, ct);
            if (!deleteResult.IsSuccess)
                return Result<bool>.Fail(deleteResult.Errors);

            return Result<bool>.Ok(true);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            // Get User By Email
            var user = await _identityService.GetUserByEmailAsync(loginDto.Email, ct);
            if(!user.IsSuccess)
            {
                return Result<UserDto>.Fail(user.Errors);
            }

            var password = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!password.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);
  
            if(!password.data)
                return Result<UserDto>.Fail(Error.Unauthorized("Invalid email or password."));

            var rolesResult = await _identityService.GetUserRoles(user.data.Email, ct);

            var userDto = new UserDto
            {
                Email = user.data.Email,
                DisplayName = user.data.DisplayName,
                Token = _tokenService.CreateToken(user.data.Id,user.data.Email,user.data.DisplayName,rolesResult.data)
            };
            return Result<UserDto>.Ok(userDto);
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDtos registerDtos, CancellationToken ct = default)
        {
            var user = await _identityService.CreateUserAsync(registerDtos, ct);
            if (!user.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);


            var rolesResult = await _identityService.GetUserRoles(user.data.Email, ct);
            if (!rolesResult.IsSuccess)
            {
                // RoleBack
                var deleteResult = await _identityService.DeleteUserAsync(user.data.Id, ct);
                return Result<UserDto>.Fail(rolesResult.Errors);

            }

            var userDto = new UserDto
            {
                Email = user.data.Email,
                DisplayName = user.data.DisplayName,
                Token = _tokenService.CreateToken(user.data.Id, user.data.Email, user.data.DisplayName, rolesResult.data)
            };
            return Result<UserDto>.Ok(userDto);
        }


    }
}
