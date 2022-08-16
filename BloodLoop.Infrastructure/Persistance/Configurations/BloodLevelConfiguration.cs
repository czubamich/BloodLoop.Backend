using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class BloodLevelConfiguration : IEntityTypeConfiguration<BloodLevel>
    {
        public void Configure(EntityTypeBuilder<BloodLevel> builder)
        {
            builder
               .ToTable($"{nameof(BloodLevel)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => BloodLevelId.Of(id))
                .IsRequired();

            builder
                .HasOne<BloodBank>()
                .WithMany(x => x.BloodLevels)
                .HasForeignKey(x => x.BloodBankId)
                .IsRequired();

            builder
                .Property(x => x.BloodType)
                .HasConversion(bt => bt.Label, str => BloodType.GetBloodTypes().FirstOrDefault(bt => bt.Label == str));

            builder
                .HasIndex(x => x.CreatedAt);
        }
    }
}