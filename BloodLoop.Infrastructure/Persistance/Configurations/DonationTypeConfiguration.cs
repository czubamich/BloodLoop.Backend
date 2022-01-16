using BloodLoop.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class DonationTypeConfiguration : IEntityTypeConfiguration<DonationType>
    {
        public void Configure(EntityTypeBuilder<DonationType> builder)
        {
            builder
               .ToTable($"{nameof(DonationType)}s");

            builder
                .Property(x => x.Label)
                .HasMaxLength(20);

            builder
                .HasKey(x => x.Label);

            builder
                .HasData(SeedData());
        }

        public DonationType[] SeedData()
        {
            return DonationType.GetDonationTypes();
        }
    }
}