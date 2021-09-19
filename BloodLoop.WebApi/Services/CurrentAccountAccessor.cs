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
    public class CurrentAccountAccessor : ICurrentAccountAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAccountAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AccountId AccountId => _httpContextAccessor.HttpContext.User.AccountId();

        public bool IsInRole(RoleId role) => _httpContextAccessor.HttpContext.User.IsInRole(role.Id.ToString());
    }
}
