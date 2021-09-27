using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BloodLoop.Domain.Accounts
{
    public class Role : IdentityRole<AccountId>
    {
        private Role() {}
        private Role(string roleName, AccountId id) : base(roleName)
        {
            Id = id;
        }

        public static Role Admin = new(nameof(Admin), AccountId.Of("fe967f00-e983-47af-9e36-13b388fc7911"));
        public static Role Staff = new(nameof(Staff), AccountId.Of("e9302cd4-0f08-4b9d-9677-932804efd1fa"));
        public static Role Donor = new(nameof(Donor), AccountId.Of("4870eb76-d925-4591-bf9e-57f9c1b8a902"));

        public static IEnumerable<Role> GetRoles()
        {
            return new[]
            {
                Admin,
                Staff,
                Donor,
            };
        }
    }
}