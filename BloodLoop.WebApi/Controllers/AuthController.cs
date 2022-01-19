using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodLoop.Application.Auth.Requests;
using BloodLoop.Application.Auth.Responses;
using BloodLoop.Application.Services;
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

            if (string.IsNullOrWhiteSpace(refreshToken))
                return AuthenticationResult.Failed("Invalid refresh token");

            AuthenticationResult result =
                await _identityService.RefreshToken(refreshToken, cancellationToken);

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
        public async Task<ActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            string refreshToken = request.RefreshToken ?? Request.Cookies[REFRESH_TOKEN_COOKIE_KEY];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest(ErrorResponse.FromMessage(StatusCodes.Status400BadRequest, "Revoke Token has failed", $"Missing required field: {nameof(request.RefreshToken)}"));

            try
            {
                await _identityService.RevokeRefreshToken(refreshToken, cancellationToken);
            }
            catch (AuthenticationException e)
            {
                return BadRequest(ErrorResponse.FromMessage(StatusCodes.Status400BadRequest, "Revoke Token has failed", e.Message));
            }

            return Ok();
        }
    }
}