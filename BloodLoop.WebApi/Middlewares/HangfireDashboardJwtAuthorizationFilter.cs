using BloodLoop.Application.Services;
using Serilog;
using System;
using System.Linq;
using Hangfire.Dashboard;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Http;

namespace BloodLoop.WebApi.Middlewares
{
    public class HangfireDashboardJwtAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static ILogger logger = Log.Logger;
        private static readonly string HangFireCookieName = "HangFireCookie";
        private static readonly int CookieExpirationMinutes = 30;
        private IIdentityService identityService;

        public HangfireDashboardJwtAuthorizationFilter(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public bool Authorize(DashboardContext context)
        {
            return true;

            var httpContext = context.GetHttpContext();

            var access_token = string.Empty;
            var setCookie = false;

            // try to get token from query string
            if (httpContext.Request.Query.ContainsKey("access_token"))
            {
                access_token = httpContext.Request.Query["access_token"].FirstOrDefault();
                setCookie = true;
            }
            else
            {
                access_token = httpContext.Request.Cookies[HangFireCookieName];
            }

            if (string.IsNullOrEmpty(access_token))
            {
                return false;
            }

            try
            {
                if (!identityService.ValidateTokenForRole(access_token, Role.Admin))
                    return false;
            }
            catch (Exception e)
            {
                logger.Error(e, "Error during dashboard hangfire jwt validation process");
                throw;
            }

            if (setCookie)
            {
                httpContext.Response.Cookies.Append(HangFireCookieName,
                access_token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(CookieExpirationMinutes)
                });
            }


            return true;
        }
    }
}
