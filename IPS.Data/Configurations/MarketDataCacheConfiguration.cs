// =============================================
// IPS.Data/Configurations/MarketDataCacheConfiguration.cs
// Description: Entity configuration for MarketDataCache
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for MarketDataCache entity
	/// </summary>
	public class MarketDataCacheConfiguration : IEntityTypeConfiguration<MarketDataCache>
	{
		public void Configure(EntityTypeBuilder<MarketDataCache> builder)
		{
			builder.ToTable("MarketDataCache");

			builder.HasKey(mdc => mdc.CacheId);

			builder.Property(mdc => mdc.Price)
				.IsRequired()
				.HasColumnType("decimal(18, 4)");

			builder.Property(mdc => mdc.OpenPrice)
				.HasColumnType("decimal(18, 4)");

			builder.Property(mdc => mdc.HighPrice)
				.HasColumnType("decimal(18, 4)");

			builder.Property(mdc => mdc.LowPrice)
				.HasColumnType("decimal(18, 4)");

			builder.Property(mdc => mdc.Change)
				.HasColumnType("decimal(18, 4)");

			builder.Property(mdc => mdc.ChangePercent)
				.HasColumnType("decimal(10, 4)");

			builder.Property(mdc => mdc.DataSource)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(mdc => mdc.LastUpdated)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(mdc => mdc.AssetId).IsUnique();
			builder.HasIndex(mdc => mdc.LastUpdated).IsDescending();

			// Relationship configured in Asset
		}
	}
}