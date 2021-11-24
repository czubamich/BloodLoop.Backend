using BloodCore;
using BloodCore.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BloodLoop.Infrastructure.Persistance.Common
{
    [Injectable]
    internal class ReadOnlyContextAdapter : IReadOnlyContext
    {
        private readonly ApplicationDbContext _dbContext;

        public ReadOnlyContextAdapter(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(params Expression<Func<TEntity, string>>[] includables) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            foreach(var include in includables)
                query.Include(include);

            return query.AsQueryable();
        }
    }
}
