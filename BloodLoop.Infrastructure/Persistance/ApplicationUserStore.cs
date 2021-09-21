using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Persistance
{
    class ApplicationUserStore : UserStore<Account, IdentityRole<AccountId>, ApplicationDbContext, AccountId>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
            AutoSaveChanges = false;
        }
    }
}
