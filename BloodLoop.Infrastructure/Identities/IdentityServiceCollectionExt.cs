using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BloodLoop.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace BloodLoop.Infrastructure.Identities
{
    public static class IdentityServiceCollectionExt
    {
        public static void AddBloodLoopAuthentication(this IServiceCollection service, IConfiguration configuration)
        {
            var AuthSettings = configuration
                .GetSection(AuthenticationSettings.SECTION)
                .Get<AuthenticationSettings>();

            service.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = AuthSettings.Issuer,
                        ValidAudience = AuthSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.SecretKey))
                    };
                });
        }
    }
}
