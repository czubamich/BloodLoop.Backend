using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class DonorConfiguration : IEntityTypeConfiguration<Donor>
    {
        public void Configure(EntityTypeBuilder<Donor> builder)
        {
            builder
               .ToTable($"{nameof(Donor)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => DonorId.Of(id));

            builder
                .HasOne(x => x.Account)
                .WithOne()
                .HasForeignKey<Donor>(x => x.AccountId);

            builder
                .Property(x => x.Pesel)
                .HasConversion(x => x.Value, value => new Pesel(value))
                .HasMaxLength(11);

            builder
                .Property(x => x.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder
                .Property(x => x.BloodType)
                .HasConversion(bt => bt.Label, str => BloodType.GetBloodTypes().FirstOrDefault(bt => bt.Label == str));

            builder
                .Property(x => x.BirthDay)
                .IsRequired();
        }
    }
}