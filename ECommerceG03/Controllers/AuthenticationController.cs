using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceG03.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginDto loginDto)
            => ToActionResult(await _authenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterAsync([FromBody] RegisterDtos registerDto,CancellationToken ct)
            => ToActionResult(await _authenticationService.RegisterAsync(registerDto, ct));

        [HttpDelete("DeleteUser/{username}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(string username, CancellationToken ct)
            => ToActionResult(await _authenticationService.DeleteUserByUsernameAsync(username, ct));
    }
}
