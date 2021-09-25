using BloodCore.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public class Account : IdentityUser<AccountId>, IAggregateRoot<AccountId>
    {
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

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

        public Account(AccountId id, string userName, string email, string passwordHash, DateTime createdAt)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            createdAt = DateTime.Now;
        }

        #endregion

        #region Creations

        public Account(AccountId id, string userName, string email, string passwordHash)
            => new Account(id, userName, email, passwordHash, DateTime.Now);

        #endregion

        #region Behaviours

        protected void Publish(IDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);

        public bool TryDequeue(out IDomainEvent domainEvent) => _domainEvents.TryDequeue(out domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();

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
