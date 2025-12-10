// =============================================
// IPS.Core/Interfaces/IRepository.cs
// Description: Generic repository interface
// =============================================

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IPS.Core.Interfaces
{
	/// <summary>
	/// Generic repository interface for common CRUD operations
	/// </summary>
	/// <typeparam name="T">Entity type</typeparam>
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Gets an entity by its ID
		/// </summary>
		/// <param name="id">Entity ID</param>
		/// <returns>The entity if found, null otherwise</returns>
		Task<T?> GetByIdAsync(int id);

		/// <summary>
		/// Gets all entities
		/// </summary>
		/// <returns>Collection of all entities</returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Finds entities that match the specified predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>Collection of matching entities</returns>
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Gets the first entity that matches the predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>The first matching entity or null</returns>
		Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Adds a new entity
		/// </summary>
		/// <param name="entity">Entity to add</param>
		/// <returns>The added entity</returns>
		Task<T> AddAsync(T entity);

		/// <summary>
		/// Updates an existing entity
		/// </summary>
		/// <param name="entity">Entity to update</param>
		/// <returns>The updated entity</returns>
		Task<T> UpdateAsync(T entity);

		/// <summary>
		/// Deletes an entity
		/// </summary>
		/// <param name="entity">Entity to delete</param>
		/// <returns>Task representing the async operation</returns>
		Task DeleteAsync(T entity);

		/// <summary>
		/// Checks if any entity matches the predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>True if any entity matches, false otherwise</returns>
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Counts entities that match the predicate
		/// </summary>
		/// <param name="predicate">Filter expression</param>
		/// <returns>Count of matching entities</returns>
		Task<int> CountAsync(Expression<Func<T, bool>> predicate);
	}
}
