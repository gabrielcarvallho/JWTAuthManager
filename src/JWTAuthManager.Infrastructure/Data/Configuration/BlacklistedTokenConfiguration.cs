using JWTAuthManager.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTAuthManager.Infrastructure.Data.Configuration;

public class BlacklistedTokenConfiguration : IEntityTypeConfiguration<BlacklistedToken>
{
    public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(e => e.ExpiresAt).HasColumnType("timestamp with time zone");

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.Token).IsUnique();
        builder.HasIndex(e => e.ExpiresAt);
    }
}