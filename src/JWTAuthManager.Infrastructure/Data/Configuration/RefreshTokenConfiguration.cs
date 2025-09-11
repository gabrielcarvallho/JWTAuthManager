using JWTAuthManager.Domain.Entities.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTAuthManager.Infrastructure.Data.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Token).IsRequired().HasMaxLength(500);
        builder.Property(e => e.ExpiresAt).HasColumnType("timestamp with time zone");

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.Token).IsUnique();
        builder.HasIndex(e => e.ExpiresAt);
        builder.HasIndex(e => e.IsRevoked);
    }
}