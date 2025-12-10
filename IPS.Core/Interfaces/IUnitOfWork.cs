// =============================================
// IPS.Core/Interfaces/IUnitOfWork.cs
// Description: Unit of Work pattern interface
// =============================================

using IPS.Core.Entities;

namespace IPS.Core.Interfaces
{
	/// <summary>
	/// Unit of Work interface for coordinating repository operations
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		// Repository properties
		IRepository<User> Users { get; }
		IRepository<Portfolio> Portfolios { get; }
		IRepository<Asset> Assets { get; }
		IRepository<PortfolioPosition> PortfolioPositions { get; }
		IRepository<Transaction> Transactions { get; }
		IRepository<PriceAlert> PriceAlerts { get; }
		IRepository<Watchlist> Watchlists { get; }
		IRepository<WatchlistItem> WatchlistItems { get; }
		IRepository<MarketDataCache> MarketDataCache { get; }
		IRepository<AuditLog> AuditLogs { get; }

		/// <summary>
		/// Saves all changes made in this unit of work
		/// </summary>
		/// <returns>Number of affected rows</returns>
		Task<int> SaveChangesAsync();

		/// <summary>
		/// Begins a database transaction
		/// </summary>
		Task BeginTransactionAsync();

		/// <summary>
		/// Commits the current transaction
		/// </summary>
		Task CommitTransactionAsync();

		/// <summary>
		/// Rolls back the current transaction
		/// </summary>
		Task RollbackTransactionAsync();
	}
}
