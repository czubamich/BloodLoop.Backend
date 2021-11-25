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
    public class DonationIntervalConfiguration : IEntityTypeConfiguration<DonationInterval>
    {
        public void Configure(EntityTypeBuilder<DonationInterval> builder)
        {
            builder
               .ToTable($"{nameof(DonationInterval)}s");

            builder
                .HasOne(x => x.DonationFrom)
                .WithMany()
                .HasForeignKey(x => x.DonationFromLabel)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(x => x.DonationTo)
                .WithMany()
                .HasForeignKey(x => x.DonationToLabel)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder
                .HasKey(x => new { x.DonationFromLabel, x.DonationToLabel });

            builder
                .Property(x => x.Interval)
                .HasConversion(
                    x => x.TotalDays,
                    x => TimeSpan.FromDays(x)
                );

            builder
                .HasData(SeedData());
        }

        public DonationInterval[] SeedData()
        {
            return new DonationInterval[] {
                new DonationInterval(DonationType.Whole.Label, DonationType.Whole.Label, TimeSpan.FromDays(7*8)),
                new DonationInterval(DonationType.Whole.Label, DonationType.Plasma.Label, TimeSpan.FromDays(7*2)),
                new DonationInterval(DonationType.Whole.Label, DonationType.Platelets.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.Whole.Label, DonationType.RedCells.Label, TimeSpan.FromDays(7*8)),

                new DonationInterval(DonationType.Plasma.Label, DonationType.Whole.Label, TimeSpan.FromDays(7*2)),
                new DonationInterval(DonationType.Plasma.Label, DonationType.Plasma.Label, TimeSpan.FromDays(7*2)),
                new DonationInterval(DonationType.Plasma.Label, DonationType.Platelets.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.Plasma.Label, DonationType.RedCells.Label, TimeSpan.FromDays(7*4)),

                new DonationInterval(DonationType.Platelets.Label, DonationType.Whole.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.Platelets.Label, DonationType.Plasma.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.Platelets.Label, DonationType.Platelets.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.Platelets.Label, DonationType.RedCells.Label, TimeSpan.FromDays(7*4)),

                new DonationInterval(DonationType.RedCells.Label, DonationType.Whole.Label, TimeSpan.FromDays(7*8)),
                new DonationInterval(DonationType.RedCells.Label, DonationType.Plasma.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.RedCells.Label, DonationType.Platelets.Label, TimeSpan.FromDays(7*4)),
                new DonationInterval(DonationType.RedCells.Label, DonationType.RedCells.Label, TimeSpan.FromDays(7*8)),
            };
        }
    }
}