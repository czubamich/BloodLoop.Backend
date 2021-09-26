using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BloodLoop.Infrastructure.Identities;

namespace BloodLoop.Infrastructure.Persistance.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
               .ToTable($"{nameof(RefreshToken)}s");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne<Account>()
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(x => x.ExpireAt)
                .IsRequired();

            builder
                .Property(x => x.Created)
                .IsRequired();
        }
    }
}