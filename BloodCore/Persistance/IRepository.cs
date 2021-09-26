using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodCore.Persistance
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IRepository<TAggregate, in TIdentity> : IRepository
         where TAggregate : IAggregateRoot<TIdentity>
         where TIdentity : Identity
    {
        Task<TAggregate> GetByIdAsync(TIdentity id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default);
        void Delete(TAggregate entity);
        Task<bool> ExistsAsync(TIdentity id, CancellationToken cancellationToken = default);
    }
}
