﻿using BloodCore.Domain;
using BloodCore.Persistance;
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
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using BloodLoop.Infrastructure.Identities;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<Account, Role, AccountId>, IUnitOfWork
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<DonationType> DonationTypes { get; set; }
        public DbSet<BloodBank> BloodBanks { get; set; }
        public DbSet<BloodLevel> BloodLevels { get; set; }

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
