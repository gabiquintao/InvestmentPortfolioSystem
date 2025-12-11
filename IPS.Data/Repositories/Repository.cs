// =============================================
// IPS.Data/Repositories/Repository.cs
// Description: Generic repository implementation
// =============================================

using IPS.Core.Interfaces;
using IPS.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IPS.Data.Repositories
{
	/// <summary>
	/// Generic repository implementation for common CRUD operations
	/// Provides abstraction layer between business logic and data access
	/// </summary>
	/// <typeparam name="T">Entity type</typeparam>
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly IPSDbContext _context;
		protected readonly DbSet<T> _dbSet;

		/// <summary>
		/// Initializes a new instance of the Repository
		/// </summary>
		/// <param name="context">Database context</param>
		public Repository(IPSDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_dbSet = _context.Set<T>();
		}

		/// <summary>
		/// Gets an entity by its ID
		/// </summary>
		/// <param name="id">Entity ID</param>
		/// <returns>The entity if found, null otherwise</returns>
		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		/// <summary>
		/// Gets all entities from the database
		/// </summary>
		/// <returns>Collection of all entities</returns>
		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		/// <summary>
		/// Finds entities that match the specified predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>Collection of matching entities</returns>
		public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Gets the first entity that matches the predicate or null
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>The first matching entity or null</returns>
		public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		/// <summary>
		/// Adds a new entity to the database
		/// </summary>
		/// <param name="entity">Entity to add</param>
		/// <returns>The added entity</returns>
		public virtual async Task<T> AddAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			await _dbSet.AddAsync(entity);
			return entity;
		}

		/// <summary>
		/// Updates an existing entity in the database
		/// </summary>
		/// <param name="entity">Entity to update</param>
		/// <returns>The updated entity</returns>
		public virtual Task<T> UpdateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			_dbSet.Update(entity);
			return Task.FromResult(entity);
		}

		/// <summary>
		/// Deletes an entity from the database
		/// </summary>
		/// <param name="entity">Entity to delete</param>
		public virtual Task DeleteAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			_dbSet.Remove(entity);
			return Task.CompletedTask;
		}

		/// <summary>
		/// Checks if any entity matches the predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>True if any entity matches, false otherwise</returns>
		public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		/// <summary>
		/// Counts entities that match the predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>Count of matching entities</returns>
		public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.CountAsync(predicate);
		}
	}
}

