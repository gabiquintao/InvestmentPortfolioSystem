// =============================================
// IPS.Data/Configurations/WatchlistConfiguration.cs
// Description: Entity configuration for Watchlist
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for Watchlist entity
	/// </summary>
	public class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
	{
		public void Configure(EntityTypeBuilder<Watchlist> builder)
		{
			builder.ToTable("Watchlists");

			builder.HasKey(w => w.WatchlistId);

			builder.Property(w => w.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(w => w.Description)
				.HasMaxLength(500);

			builder.Property(w => w.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(w => w.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(w => w.UserId);

			// Relationships
			builder.HasOne(w => w.User)
				.WithMany(u => u.Watchlists)
				.HasForeignKey(w => w.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(w => w.Items)
				.WithOne(wi => wi.Watchlist)
				.HasForeignKey(wi => wi.WatchlistId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
