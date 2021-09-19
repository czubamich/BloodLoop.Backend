using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Common
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
