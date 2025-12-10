// =============================================
// IPS.Data/Configurations/UserConfiguration.cs
// Description: Entity configuration for User
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for User entity
	/// </summary>
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasKey(u => u.UserId);

			builder.Property(u => u.Username)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(u => u.Email)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(u => u.PasswordHash)
				.IsRequired()
				.HasMaxLength(256);

			builder.Property(u => u.FirstName)
				.HasMaxLength(50);

			builder.Property(u => u.LastName)
				.HasMaxLength(50);

			builder.Property(u => u.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(u => u.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(u => u.Username).IsUnique();
			builder.HasIndex(u => u.Email).IsUnique();

			// Relationships
			builder.HasMany(u => u.Portfolios)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(u => u.Watchlists)
				.WithOne(w => w.User)
				.HasForeignKey(w => w.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(u => u.PriceAlerts)
				.WithOne(pa => pa.User)
				.HasForeignKey(pa => pa.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}