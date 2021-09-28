using System;
using BloodCore.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

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
        Task<TAggregate> GetById(TIdentity id, CancellationToken cancellationToken = default);

        Task<TAggregate> Get(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);
        [Obsolete("Use FindAs instead for automapper")]
        Task<TResult> Get<TResult>(ISpecification<TAggregate, TResult> specification, CancellationToken cancellationToken = default);
        Task<TResult> GetAs<TResult>(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TAggregate>> Find(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);
        [Obsolete("Use FindAs instead for automapper")]
        Task<IReadOnlyList<TResult>> Find<TResult>(ISpecification<TAggregate, TResult> specification, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TResult>> FindAs<TResult>(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default);
        Task Delete(TAggregate entity);
    }
}
