using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder
               .ToTable($"{nameof(Staff)}s");

            builder
                .Property(x => x.Id)
                .HasConversion(x => x.Id, id => StaffId.Of(id));

            builder
                .HasOne(x => x.Account)
                .WithOne()
                .HasForeignKey<Staff>(x => x.AccountId);

            builder
                .HasOne(x => x.BloodBank)
                .WithMany()
                .HasForeignKey(x => x.BloodBankId);
        }
    }
}