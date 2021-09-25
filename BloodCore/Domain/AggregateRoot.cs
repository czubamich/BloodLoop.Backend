using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BloodCore.Domain
{
    public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>, IAggregateRoot<TIdentity> where TIdentity : Identity
    {
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void Publish(IDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);

        public bool TryDequeue(out IDomainEvent domainEvent) => _domainEvents.TryDequeue(out domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected AggregateRoot(TIdentity id) : base(id)
        {
        }

        protected AggregateRoot() { }
    }
}
