using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Persistance
{
    public interface IUnitOfWork
    {
        IReadOnlyCollection<IAggregateRoot> Aggregates { get; }

        void AddAggregate(IAggregateRoot aggregateRoot, Func<IAggregateRoot, Task> updateFunc);

        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
