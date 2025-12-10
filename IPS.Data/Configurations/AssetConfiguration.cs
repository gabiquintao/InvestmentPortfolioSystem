using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// =============================================
// IPS.Data/Configurations/AssetConfiguration.cs
// Description: Entity configuration for Asset
// =============================================

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for Asset entity
	/// </summary>
	public class AssetConfiguration : IEntityTypeConfiguration<Asset>
	{
		public void Configure(EntityTypeBuilder<Asset> builder)
		{
			builder.ToTable("Assets");

			builder.HasKey(a => a.AssetId);

			builder.Property(a => a.Symbol)
				.IsRequired()
				.HasMaxLength(20);

			builder.Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(a => a.AssetType)
				.IsRequired()
				.HasMaxLength(20);

			builder.Property(a => a.Exchange)
				.HasMaxLength(50);

			builder.Property(a => a.Currency)
				.IsRequired()
				.HasMaxLength(3)
				.HasDefaultValue("USD");

			builder.Property(a => a.Description)
				.HasMaxLength(1000);

			builder.Property(a => a.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(a => a.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(a => a.Symbol).IsUnique();
			builder.HasIndex(a => a.AssetType);
			builder.HasIndex(a => a.Exchange);

			// Relationships
			builder.HasMany(a => a.Positions)
				.WithOne(pp => pp.Asset)
				.HasForeignKey(pp => pp.AssetId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(a => a.Transactions)
				.WithOne(t => t.Asset)
				.HasForeignKey(t => t.AssetId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(a => a.WatchlistItems)
				.WithOne(wi => wi.Asset)
				.HasForeignKey(wi => wi.AssetId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(a => a.PriceAlerts)
				.WithOne(pa => pa.Asset)
				.HasForeignKey(pa => pa.AssetId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(a => a.MarketDataCache)
				.WithOne(mdc => mdc.Asset)
				.HasForeignKey<MarketDataCache>(mdc => mdc.AssetId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
