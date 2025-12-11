// =============================================
// IPS.Data/Context/IPSDbContext.cs
// Description: Entity Framework Core DbContext
// =============================================

using Microsoft.EntityFrameworkCore;
using IPS.Core.Entities;
using IPS.Data.Configurations;

namespace IPS.Data.Context
{
	/// <summary>
	/// Database context for the Investment Portfolio System
	/// Manages entity configurations and database connections
	/// </summary>
	public class IPSDbContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the PortfolioDbContext
		/// </summary>
		/// <param name="options">Database context options</param>
		public IPSDbContext(DbContextOptions<IPSDbContext> options)
			: base(options)
		{
		}

		// DbSet properties for each entity
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Portfolio> Portfolios { get; set; } = null!;
		public DbSet<Asset> Assets { get; set; } = null!;
		public DbSet<PortfolioPosition> PortfolioPositions { get; set; } = null!;
		public DbSet<Transaction> Transactions { get; set; } = null!;
		public DbSet<PriceAlert> PriceAlerts { get; set; } = null!;
		public DbSet<Watchlist> Watchlists { get; set; } = null!;
		public DbSet<WatchlistItem> WatchlistItems { get; set; } = null!;
		public DbSet<MarketDataCache> MarketDataCache { get; set; } = null!;
		public DbSet<AuditLog> AuditLogs { get; set; } = null!;

		/// <summary>
		/// Configures entity mappings using Fluent API
		/// </summary>
		/// <param name="modelBuilder">Model builder instance</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Apply all entity configurations
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
			modelBuilder.ApplyConfiguration(new AssetConfiguration());
			modelBuilder.ApplyConfiguration(new PortfolioPositionConfiguration());
			modelBuilder.ApplyConfiguration(new TransactionConfiguration());
			modelBuilder.ApplyConfiguration(new PriceAlertConfiguration());
			modelBuilder.ApplyConfiguration(new WatchlistConfiguration());
			modelBuilder.ApplyConfiguration(new WatchlistItemConfiguration());
			modelBuilder.ApplyConfiguration(new MarketDataCacheConfiguration());
			modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
		}

		/// <summary>
		/// Override SaveChanges to update timestamps automatically
		/// </summary>
		public override int SaveChanges()
		{
			UpdateTimestamps();
			return base.SaveChanges();
		}

		/// <summary>
		/// Override SaveChangesAsync to update timestamps automatically
		/// </summary>
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			UpdateTimestamps();
			return base.SaveChangesAsync(cancellationToken);
		}

		/// <summary>
		/// Updates CreatedAt and UpdatedAt timestamps for entities
		/// </summary>
		private void UpdateTimestamps()
		{
			var entries = ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

			foreach (var entry in entries)
			{
				var entity = entry.Entity;

				// Update UpdatedAt for all entities
				if (entry.State == EntityState.Modified)
				{
					var updatedAtProperty = entry.Property("UpdatedAt");
					if (updatedAtProperty != null)
					{
						updatedAtProperty.CurrentValue = DateTime.UtcNow;
					}
				}

				// Set CreatedAt for new entities
				if (entry.State == EntityState.Added)
				{
					var createdAtProperty = entry.Property("CreatedAt");
					if (createdAtProperty != null)
					{
						createdAtProperty.CurrentValue = DateTime.UtcNow;
					}

					var updatedAtProperty = entry.Property("UpdatedAt");
					if (updatedAtProperty != null)
					{
						updatedAtProperty.CurrentValue = DateTime.UtcNow;
					}
				}
			}
		}
	}
}