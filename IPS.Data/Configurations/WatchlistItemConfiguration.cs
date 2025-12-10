// =============================================
// IPS.Data/Configurations/WatchlistItemConfiguration.cs
// Description: Entity configuration for WatchlistItem
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for WatchlistItem entity
	/// </summary>
	public class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
	{
		public void Configure(EntityTypeBuilder<WatchlistItem> builder)
		{
			builder.ToTable("WatchlistItems");

			builder.HasKey(wi => wi.WatchlistItemId);

			builder.Property(wi => wi.Notes)
				.HasMaxLength(500);

			builder.Property(wi => wi.AddedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(wi => wi.WatchlistId);
			builder.HasIndex(wi => wi.AssetId);
			builder.HasIndex(wi => new { wi.WatchlistId, wi.AssetId }).IsUnique();

			// Relationships configured in Watchlist and Asset
		}
	}
}