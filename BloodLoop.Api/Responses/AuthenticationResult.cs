using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodCore.AspNet;

namespace BloodLoop.Api.Responses
{
    public class AuthenticationResult
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public JwtToken AccessToken { get; init; }
        public JwtToken RefreshToken { get; init; }

        public AuthenticationResult(bool success, string message, JwtToken accessToken, JwtToken refreshToken)
        {
            Success = success;
            Message = message;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public static AuthenticationResult Failed(string message) => new(false, message, null, null);
        public static AuthenticationResult Succeed(string message, JwtToken accessToken, JwtToken refreshToken) => new(true, message, accessToken, refreshToken);
    }
}
