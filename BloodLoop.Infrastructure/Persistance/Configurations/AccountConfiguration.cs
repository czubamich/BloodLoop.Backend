﻿using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
               .ToTable($"{nameof(Account)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => AccountId.Of(id))
                .IsRequired();
        }
    }
}