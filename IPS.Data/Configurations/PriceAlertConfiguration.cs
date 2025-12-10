// =============================================
// IPS.Data/Configurations/PriceAlertConfiguration.cs
// Description: Entity configuration for PriceAlert
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for PriceAlert entity
	/// </summary>
	public class PriceAlertConfiguration : IEntityTypeConfiguration<PriceAlert>
	{
		public void Configure(EntityTypeBuilder<PriceAlert> builder)
		{
			builder.ToTable("PriceAlerts");

			builder.HasKey(pa => pa.AlertId);

			builder.Property(pa => pa.TargetPrice)
				.IsRequired()
				.HasColumnType("decimal(18, 4)");

			builder.Property(pa => pa.AlertType)
				.IsRequired()
				.HasMaxLength(10);

			builder.Property(pa => pa.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(pa => pa.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(pa => pa.UserId);
			builder.HasIndex(pa => pa.AssetId);
			builder.HasIndex(pa => pa.IsActive)
				.HasFilter("[IsActive] = 1");

			// Relationships configured in User and Asset
		}
	}
}