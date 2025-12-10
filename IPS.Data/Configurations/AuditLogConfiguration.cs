// =============================================
// IPS.Data/Configurations/AuditLogConfiguration.cs
// Description: Entity configuration for AuditLog
// =============================================

using IPS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IPS.Data.Configurations
{
	/// <summary>
	/// Fluent API configuration for AuditLog entity
	/// </summary>
	public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
	{
		public void Configure(EntityTypeBuilder<AuditLog> builder)
		{
			builder.ToTable("AuditLog");

			builder.HasKey(al => al.AuditId);

			builder.Property(al => al.ActionType)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(al => al.EntityType)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(al => al.IpAddress)
				.HasMaxLength(50);

			builder.Property(al => al.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(al => al.UserId);
			builder.HasIndex(al => al.CreatedAt).IsDescending();
			builder.HasIndex(al => al.EntityType);

			// Relationships
			builder.HasOne(al => al.User)
				.WithMany()
				.HasForeignKey(al => al.UserId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}