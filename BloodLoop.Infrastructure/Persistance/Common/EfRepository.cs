using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Domain;
using BloodCore.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Infrastructure.Persistance
{
    public class EfRepository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>
        where TAggregate : AggregateRoot<TIdentity> 
        where TIdentity : Identity
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly DbSet<TAggregate> Aggregates;

        public IUnitOfWork UnitOfWork => DbContext as IUnitOfWork;

        public EfRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Aggregates = dbContext.Set<TAggregate>();
        }

        public async Task<TAggregate> GetByIdAsync(TIdentity id, CancellationToken cancellationToken = default)
        {
            return await Aggregates.FindAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Aggregates.ToListAsync(cancellationToken);
        }

        public async Task<TAggregate> AddAsync(TAggregate entity, CancellationToken cancellationToken = default)
        {
            await Aggregates.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default)
        {
            await Aggregates.AddRangeAsync(entities, cancellationToken);
        }

        public void Delete(TAggregate entity)
        {
            Aggregates.Remove(entity);
        }

        public async Task<bool> ExistsAsync(TIdentity id, CancellationToken cancellationToken = default)
        {
            return await Aggregates.AnyAsync(x => x.Id == id);
        }
    }
}
