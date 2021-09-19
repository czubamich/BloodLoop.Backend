using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Common
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
        bool TryDequeue(out IDomainEvent domainEvent);
    }

    public interface IAggregateRoot<TIdentity> : IAggregateRoot, IEntity<TIdentity> where TIdentity : Identity
    {
    }
}
