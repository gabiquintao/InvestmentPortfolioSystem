// =============================================
// IPS.Data/Configurations/PortfolioPositionConfiguration.cs
// Description: Entity configuration for PortfolioPosition
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for PortfolioPosition entity
	/// </summary>
	public class PortfolioPositionConfiguration : IEntityTypeConfiguration<PortfolioPosition>
	{
		public void Configure(EntityTypeBuilder<PortfolioPosition> builder)
		{
			builder.ToTable("PortfolioPositions");

			builder.HasKey(pp => pp.PositionId);

			builder.Property(pp => pp.Quantity)
				.IsRequired()
				.HasColumnType("decimal(18, 8)");

			builder.Property(pp => pp.AveragePurchasePrice)
				.IsRequired()
				.HasColumnType("decimal(18, 4)");

			builder.Property(pp => pp.CurrentPrice)
				.HasColumnType("decimal(18, 4)");

			builder.Property(pp => pp.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(pp => pp.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(pp => pp.PortfolioId);
			builder.HasIndex(pp => pp.AssetId);
			builder.HasIndex(pp => new { pp.PortfolioId, pp.AssetId }).IsUnique();

			// Relationships configured in Portfolio and Asset
		}
	}
}