using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class BloodBankConfiguration : IEntityTypeConfiguration<BloodBank>
    {
        public void Configure(EntityTypeBuilder<BloodBank> builder)
        {
            builder
               .ToTable($"{nameof(BloodBank)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => BloodBankId.Of(id));

            builder
                .Property(x => x.Name)
                .HasMaxLength(11);
        }
    }
}