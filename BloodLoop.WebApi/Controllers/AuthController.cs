using System;
using System.Threading;
using System.Threading.Tasks;
using BloodLoop.Api.Requests;
using BloodLoop.Api.Responses;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string REFRESH_TOKEN_COOKIE_KEY = "X-Refresh-Token";
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResult>> SignIn([FromBody] AuthenticateRequest request, CancellationToken cancellationToken)
        {
            AuthenticationResult result =
                await _identityService.SignInAsync(request.UsernameOrEmail, request.Password, cancellationToken);

            if (!result.Success)
                return result;

            Response.Cookies.Append(
                REFRESH_TOKEN_COOKIE_KEY, 
                result.RefreshToken.Token, 
                new CookieOptions() 
                    {
                        HttpOnly = true, 
                        Expires = result.RefreshToken.ExpireAt, 
                        SameSite = SameSiteMode.Strict
                    });

            return result;
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<AuthenticationResult>> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            string refreshToken = request.RefreshToken ?? Request.Cookies[REFRESH_TOKEN_COOKIE_KEY];

            AuthenticationResult result =
                await _identityService.RefreshToken(request.RefreshToken, cancellationToken);

            if (!result.Success)
                return result;

            Response.Cookies.Delete(REFRESH_TOKEN_COOKIE_KEY);
            Response.Cookies.Append(
                REFRESH_TOKEN_COOKIE_KEY,
                result.RefreshToken.Token,
                new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = result.RefreshToken.ExpireAt,
                    SameSite = SameSiteMode.Strict
                });

            return result;
        }

        [Authorize]
        [HttpPost("Revoke")]
        public async Task<ActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            string refreshToken = request.RefreshToken ?? Request.Cookies[REFRESH_TOKEN_COOKIE_KEY];

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest();

            await _identityService.RevokeRefreshToken(request.RefreshToken, cancellationToken);

            return Ok();
        }
    }
}