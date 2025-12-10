// =============================================
// IPS.Data/Configurations/PortfolioConfiguration.cs
// Description: Entity configuration for Portfolio
// =============================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for Portfolio entity
	/// </summary>
	public class PortfolioConfiguration : IEntityTypeConfiguration<Core.Entities.Portfolio>
	{
		public void Configure(EntityTypeBuilder<Core.Entities.Portfolio> builder)
		{
			builder.ToTable("Portfolios");

			builder.HasKey(p => p.PortfolioId);

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Description)
				.HasMaxLength(500);

			builder.Property(p => p.BaseCurrency)
				.IsRequired()
				.HasMaxLength(3)
				.HasDefaultValue("USD");

			builder.Property(p => p.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(p => p.UserId);
			builder.HasIndex(p => p.Name);

			// Relationships
			builder.HasOne(p => p.User)
				.WithMany(u => u.Portfolios)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Positions)
				.WithOne(pp => pp.Portfolio)
				.HasForeignKey(pp => pp.PortfolioId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Transactions)
				.WithOne(t => t.Portfolio)
				.HasForeignKey(t => t.PortfolioId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

