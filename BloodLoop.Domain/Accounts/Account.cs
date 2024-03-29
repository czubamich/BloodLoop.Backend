﻿using BloodCore.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public class Account : IdentityUser<AccountId>, IEntity<AccountId>
    {
        AccountId IEntity<AccountId>.Id => AccountId.Of(Id);

        [PersonalData]
        public string FirstName { get; private set; }
        [PersonalData]
        public string LastName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        #region Constructors

        private Account() 
        { 
        }

        public Account(AccountId id, string userName, string email, DateTime createdAt)
        {
            Id = id;
            UserName = userName;
            Email = email;
            CreatedDate = createdAt;
        }

        #endregion

        #region Creations

        public static Account Create(AccountId id, string userName, string email)
            => new Account(id, userName, email, DateTime.Now);

        public static Account Create(string userName, string email)
            => new Account(AccountId.New, userName, email, DateTime.Now);

        #endregion

        #region Behaviours

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

        #endregion
    }
}
