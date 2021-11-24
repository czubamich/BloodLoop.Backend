using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.DonationHelpers;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class DonationConverterConfiguration : IEntityTypeConfiguration<DonationConverter>
    {
        public void Configure(EntityTypeBuilder<DonationConverter> builder)
        {
            builder
               .ToTable($"{nameof(DonationConverter)}s");

            builder
                .HasKey(x => new { x.DonationFromLabel, x.DonationToLabel });

            builder
                .HasOne(x => x.DonationFrom)
                .WithMany()
                .HasForeignKey(x => x.DonationFromLabel)
                .IsRequired();

            builder
                .HasOne(x => x.DonationTo)
                .WithMany()
                .HasForeignKey(x => x.DonationToLabel)
                .IsRequired();

            builder
                .HasData(SeedData());
        }

        public DonationConverter[] SeedData()
        {
            return new DonationConverter[] {
                new DonationConverter(DonationType.Whole.Label, DonationType.Whole.Label, 1),
                new DonationConverter(DonationType.Plasma.Label, DonationType.Whole.Label, 200.0/600.0),
                new DonationConverter(DonationType.Platelets.Label, DonationType.Whole.Label, 500.0/500.0),
                new DonationConverter(DonationType.RedCells.Label, DonationType.Whole.Label, 500.0/300.0),
                new DonationConverter(DonationType.Disqualified.Label, DonationType.Whole.Label, 0),
            };
        }
    }
}