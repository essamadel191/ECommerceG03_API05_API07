using ECommerceG03.Application.Common;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.IdentityDtos;
using ECommerceG03.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceG03.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;    

        public IdentityService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync(ct);
            if (users == null || users.Count == 0) return Result<bool>.Fail(Error.NotFound("NotFound","User not found"));
            if (users.Count > 1) return Result<bool>.Fail(Error.Conflict("MultipleUsers","Multiple users found with the same email."));
            var user = users[0];

            var isMatch = await _userManager.CheckPasswordAsync(user, password);
            return Result<bool>.Ok(isMatch);
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDtos registerDtos, CancellationToken ct = default)
        {
            var user = new ApplicationUser
            {
                Email = registerDtos.Email,
                PhoneNumber = registerDtos.PhoneNumber,
                DisplayName = registerDtos.DisplayName,
                UserName = registerDtos.UserName
            };

            var userResult = await _userManager.CreateAsync(user, registerDtos.Password);
            if(!userResult.Succeeded)
            {
                var errors = userResult.Errors.Select(e => new Error(e.Code,e.Description));
                return Result<IdentityUserResult>.Fail(errors.ToList());
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(
                id: user.Id,
                email: user.Email,
                userName: user.UserName,
                displayName: user.DisplayName
            ));
        }


        public async Task<Result<IdentityUserResult>> GetUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync(ct);
            if (users == null || users.Count == 0) return Result<IdentityUserResult>.Fail(Error.NotFound("NotFound","User not found"));
            if (users.Count > 1) return Result<IdentityUserResult>.Fail(Error.Conflict("MultipleUsers","Multiple users found with the same email."));
            var user = users[0];

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(
                id: user.Id,
                email: user.Email,
                userName: user.UserName,
                displayName: user.DisplayName
                ));
        }

        public async Task<Result<IdentityUserResult>> GetUserByUsernameAsync(string username, CancellationToken ct = default)
        {
            var user = await _userManager.Users.Where(u => u.UserName == username).ToListAsync();
            
            if (user == null) return Result<IdentityUserResult>.Fail(Error.NotFound("NotFound","User not found"));
            if(user.Count > 1) return Result<IdentityUserResult>.Fail(Error.Conflict("MultipleUsers","Multiple users found with the same username."));
            
            var userData = user[0];
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(
                id: userData.Id,
                email: userData.Email,
                userName: userData.UserName,
                displayName: userData.DisplayName
            ));
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync(ct);
            if (users == null || users.Count == 0) return Result<IReadOnlyList<string>>.Fail(Error.NotFound("NotFound","User not found"));
            if (users.Count > 1) return Result<IReadOnlyList<string>>.Fail(Error.Conflict("MultipleUsers","Multiple users found with the same email."));
            var user = users[0];

            var roles = await _userManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }

        public async Task<Result<bool>> DeleteUserAsync(string Id, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if(user == null)
                return Result<bool>.Fail(Error.NotFound("NotFound","User not found"));

            var deleteResult = await _userManager.DeleteAsync(user);
            return Result<bool>.Ok(deleteResult.Succeeded);
        }
    }
}


