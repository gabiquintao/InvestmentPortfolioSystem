// =============================================
// IPS.Data/Configurations/TransactionConfiguration.cs
// Description: Entity configuration for Transaction
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for Transaction entity
	/// </summary>
	public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
	{
		public void Configure(EntityTypeBuilder<Transaction> builder)
		{
			builder.ToTable("Transactions");

			builder.HasKey(t => t.TransactionId);

			builder.Property(t => t.TransactionType)
				.IsRequired()
				.HasMaxLength(10);

			builder.Property(t => t.Quantity)
				.IsRequired()
				.HasColumnType("decimal(18, 8)");

			builder.Property(t => t.PricePerUnit)
				.IsRequired()
				.HasColumnType("decimal(18, 4)");

			builder.Property(t => t.Commission)
				.HasColumnType("decimal(18, 4)")
				.HasDefaultValue(0);

			builder.Property(t => t.Notes)
				.HasMaxLength(500);

			builder.Property(t => t.TransactionDate)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(t => t.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(t => t.PortfolioId);
			builder.HasIndex(t => t.AssetId);
			builder.HasIndex(t => t.TransactionDate).IsDescending();
			builder.HasIndex(t => t.TransactionType);

			// Relationships configured in Portfolio and Asset
		}
	}
}