using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using BloodLoop.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using BloodLoop.Domain.BloodBanks;

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
        public BloodBankId BloodBank => _httpContextAccessor.HttpContext.User.BloodBank();
        public IEnumerable<Role> Roles => _httpContextAccessor.HttpContext.User.Roles();
    }
}
