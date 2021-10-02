using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using BloodLoop.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodLoop.WebApi.Services
{
    public class ApplicationContext : IApplicationContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AccountId AccountId => _httpContextAccessor.HttpContext.User.AccountId();
        public IEnumerable<Role> Roles => _httpContextAccessor.HttpContext.User.Roles().Select(x => new Role(x));
    }
}
