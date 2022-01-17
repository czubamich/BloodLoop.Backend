using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder
               .ToTable($"{nameof(Donation)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => DonationId.Of(id))
                .IsRequired();

            builder
                .HasOne(x => x.DonationType)
                .WithMany()
                .HasForeignKey($"{nameof(DonationType)}Label")
                .IsRequired();

            builder
                .HasOne<Donor>()
                .WithMany(x => x.Donations)
                .HasForeignKey(x => x.DonorId)
                .IsRequired();

            builder
                .HasOne<BloodBank>()
                .WithMany()
                .HasForeignKey(x => x.SourceBankId);
        }
    }
}