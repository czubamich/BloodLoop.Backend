using BloodCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public class Account : AggregateRoot<AccountId>
    {
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public IEnumerable<RoleId> RoleIds { get; private set; }
        public IEnumerable<Role> Roles { get; private set; }

        public Account(AccountId id, string userName, string email, string passwordHash, DateTime createdAt)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            createdAt = DateTime.Now;
        }

        public Account(AccountId id, string userName, string email, string passwordHash)
            => new Account(id, userName, email, passwordHash, DateTime.Now);

        public Account SetFirstName(string firstName)
        {
            FirstName = firstName;

            return this;
        }

        public Account SetLastName(string lastName)
        {
            LastName = lastName;

            return this;
        }

        public Account SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;

            return this;
        }
    }
}
