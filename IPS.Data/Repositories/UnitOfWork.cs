// =============================================
// IPS.Data/Repositories/UnitOfWork.cs
// Description: Unit of Work pattern implementation
// =============================================

using IPS.Core.Entities;
using IPS.Core.Interfaces;
using IPS.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IPS.Data.Repositories
{
	/// <summary>
	/// Unit of Work implementation for coordinating multiple repository operations
	/// Ensures all operations succeed or fail together (transaction management)
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IPSDbContext _context;
		private IDbContextTransaction? _transaction;
		private bool _disposed = false;

		// Repository instances (lazy initialization)
		private IRepository<User>? _users;
		private IRepository<Core.Entities.Portfolio>? _portfolios;
		private IRepository<Asset>? _assets;
		private IRepository<PortfolioPosition>? _portfolioPositions;
		private IRepository<Transaction>? _transactions;
		private IRepository<PriceAlert>? _priceAlerts;
		private IRepository<Watchlist>? _watchlists;
		private IRepository<WatchlistItem>? _watchlistItems;
		private IRepository<MarketDataCache>? _marketDataCache;
		private IRepository<AuditLog>? _auditLogs;

		/// <summary>
		/// Initializes a new instance of the UnitOfWork
		/// </summary>
		/// <param name="context">Database context</param>
		public UnitOfWork(IPSDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		/// Gets the User repository (lazy initialization)
		/// </summary>
		public IRepository<User> Users
		{
			get { return _users ??= new Repository<User>(_context); }
		}

		/// <summary>
		/// Gets the Portfolio repository (lazy initialization)
		/// </summary>
		public IRepository<Core.Entities.Portfolio> Portfolios
		{
			get { return _portfolios ??= new Repository<Core.Entities.Portfolio>(_context); }
		}

		/// <summary>
		/// Gets the Asset repository (lazy initialization)
		/// </summary>
		public IRepository<Asset> Assets
		{
			get { return _assets ??= new Repository<Asset>(_context); }
		}

		/// <summary>
		/// Gets the PortfolioPosition repository (lazy initialization)
		/// </summary>
		public IRepository<PortfolioPosition> PortfolioPositions
		{
			get { return _portfolioPositions ??= new Repository<PortfolioPosition>(_context); }
		}

		/// <summary>
		/// Gets the Transaction repository (lazy initialization)
		/// </summary>
		public IRepository<Transaction> Transactions
		{
			get { return _transactions ??= new Repository<Transaction>(_context); }
		}

		/// <summary>
		/// Gets the PriceAlert repository (lazy initialization)
		/// </summary>
		public IRepository<PriceAlert> PriceAlerts
		{
			get { return _priceAlerts ??= new Repository<PriceAlert>(_context); }
		}

		/// <summary>
		/// Gets the Watchlist repository (lazy initialization)
		/// </summary>
		public IRepository<Watchlist> Watchlists
		{
			get { return _watchlists ??= new Repository<Watchlist>(_context); }
		}

		/// <summary>
		/// Gets the WatchlistItem repository (lazy initialization)
		/// </summary>
		public IRepository<WatchlistItem> WatchlistItems
		{
			get { return _watchlistItems ??= new Repository<WatchlistItem>(_context); }
		}

		/// <summary>
		/// Gets the MarketDataCache repository (lazy initialization)
		/// </summary>
		public IRepository<MarketDataCache> MarketDataCache
		{
			get { return _marketDataCache ??= new Repository<MarketDataCache>(_context); }
		}

		/// <summary>
		/// Gets the AuditLog repository (lazy initialization)
		/// </summary>
		public IRepository<AuditLog> AuditLogs
		{
			get { return _auditLogs ??= new Repository<AuditLog>(_context); }
		}

		/// <summary>
		/// Saves all changes made in this unit of work to the database
		/// </summary>
		/// <returns>Number of affected rows</returns>
		public async Task<int> SaveChangesAsync()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				// Log concurrency exception
				throw new InvalidOperationException(
					"A concurrency error occurred while saving changes. The record may have been modified by another user.",
					ex);
			}
			catch (DbUpdateException ex)
			{
				// Log database update exception
				throw new InvalidOperationException(
					"An error occurred while saving changes to the database.",
					ex);
			}
		}

		/// <summary>
		/// Begins a new database transaction
		/// </summary>
		public async Task BeginTransactionAsync()
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("A transaction is already in progress.");
			}

			_transaction = await _context.Database.BeginTransactionAsync();
		}

		/// <summary>
		/// Commits the current transaction
		/// </summary>
		public async Task CommitTransactionAsync()
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("No transaction is currently in progress.");
			}

			try
			{
				await _context.SaveChangesAsync();
				await _transaction.CommitAsync();
			}
			catch
			{
				await RollbackTransactionAsync();
				throw;
			}
			finally
			{
				if (_transaction != null)
				{
					await _transaction.DisposeAsync();
					_transaction = null;
				}
			}
		}

		/// <summary>
		/// Rolls back the current transaction
		/// </summary>
		public async Task RollbackTransactionAsync()
		{
			if (_transaction == null)
			{
				throw new InvalidOperationException("No transaction is currently in progress.");
			}

			try
			{
				await _transaction.RollbackAsync();
			}
			finally
			{
				if (_transaction != null)
				{
					await _transaction.DisposeAsync();
					_transaction = null;
				}
			}
		}

		/// <summary>
		/// Disposes the unit of work and releases resources
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Protected dispose method for cleanup
		/// </summary>
		/// <param name="disposing">True if disposing managed resources</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// Dispose transaction if still active
					if (_transaction != null)
					{
						_transaction.Dispose();
						_transaction = null;
					}

					// Dispose context
					_context.Dispose();
				}

				_disposed = true;
			}
		}

		/// <summary>
		/// Destructor to ensure resources are released
		/// </summary>
		~UnitOfWork()
		{
			Dispose(false);
		}
	}
}
