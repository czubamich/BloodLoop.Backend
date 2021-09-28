using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using BloodCore.Domain;
using BloodCore.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BloodLoop.Infrastructure.Persistance
{
    public class EfRepository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>
        where TAggregate : AggregateRoot<TIdentity> 
        where TIdentity : Identity
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TAggregate> Aggregates;
        protected readonly ISpecificationEvaluator SpecificationEvaluator;
        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork => DbContext as IUnitOfWork;

        public EfRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IMapper mapper)
        {
            DbContext = dbContext;
            Aggregates = dbContext.Set<TAggregate>();
            SpecificationEvaluator = specificationEvaluator;
            _mapper = mapper;
        }

        public async Task<TAggregate> GetById(TIdentity id, CancellationToken cancellationToken = default)
            => await Aggregates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<TAggregate> Get(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
            => await Aggregates.WithSpecification(specification).SingleOrDefaultAsync(cancellationToken);

        public async Task<TResult> Get<TResult>(ISpecification<TAggregate, TResult> specification, CancellationToken cancellationToken = default)
            => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

        public async Task<TResult> GetAs<TResult>(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
            => await Aggregates.AsNoTracking().WithSpecification(specification).ProjectTo<TResult>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<TAggregate>> Find(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
            => (await Aggregates.ToListAsync(specification, cancellationToken)).ToList();

        public async Task<IReadOnlyList<TResult>> Find<TResult>(ISpecification<TAggregate, TResult> specification, CancellationToken cancellationToken = default)
            => await ApplySpecification(specification).ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<TResult>> FindAs<TResult>(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
            => await Aggregates.AsNoTracking().WithSpecification(specification).ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        public async Task AddRangeAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default)
            => await Aggregates.AddRangeAsync(entities, cancellationToken);

        public async Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default)
            => (await Aggregates.AddAsync(entity, cancellationToken)).Entity;

        public async Task Delete(TAggregate entity)
            => await Task.FromResult(Aggregates.Remove(entity));

        protected virtual IQueryable<TAggregate> ApplySpecification(ISpecification<TAggregate> specification, bool evaluateCriteriaOnly = false)
        {
            if (specification is null) throw new ArgumentNullException(nameof(specification));

            return SpecificationEvaluator.GetQuery(Aggregates.AsQueryable(), specification, evaluateCriteriaOnly);
        }

        protected IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TAggregate, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException(nameof(specification));
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return SpecificationEvaluator.GetQuery(Aggregates.AsQueryable(), specification);
        }
    }
}
