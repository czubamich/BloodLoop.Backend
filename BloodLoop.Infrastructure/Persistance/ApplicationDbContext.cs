using BloodCore.Common;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<Account, IdentityRole<AccountId>, AccountId>, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;
        private IList<IAggregateRoot> _aggregates;

        sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                var connectionString = configuration.GetConnectionString("BloodLoop");

                DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer(connectionString, opts =>
                {
                    opts.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name);
                });

                return new ApplicationDbContext(builder.Options);
            }
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _aggregates = new List<IAggregateRoot>();
        }

        public void AddAggregate(IAggregateRoot aggregateRoot, Func<IAggregateRoot, Task> updateFunc)
        {
            if (!Aggregates.Contains(aggregateRoot))
                _aggregates.Add(aggregateRoot);
        }

        public IReadOnlyCollection<IAggregateRoot> Aggregates => ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
            .Select(po => po.Entity)
            .Concat(_aggregates)
            .ToList();

        public async Task BeginTransactionAsync()
        {
            _dbContextTransaction = Database.CurrentTransaction ?? await Database.BeginTransactionAsync();

            await Task.CompletedTask;
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContextTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
