// =============================================
// IPS.Core/Interfaces/Services/IWatchlistService.cs
// Description: Watchlist service interface
// =============================================

using IPS.Core.DTOs.Market;
using IPS.Core.DTOs.Watchlist;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for watchlist operations
	/// </summary>
	public interface IWatchlistService
	{
		/// <summary>
		/// Creates a new watchlist
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="createDto">Watchlist creation data</param>
		/// <returns>Created watchlist DTO</returns>
		Task<WatchlistDto> CreateWatchlistAsync(int userId, CreateWatchlistDto createDto);

		/// <summary>
		/// Gets all watchlists for a user
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <returns>Collection of watchlist DTOs</returns>
		Task<IEnumerable<WatchlistDto>> GetUserWatchlistsAsync(int userId);

		/// <summary>
		/// Gets a specific watchlist with all items
		/// </summary>
		/// <param name="watchlistId">Watchlist ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Watchlist with items</returns>
		Task<IEnumerable<WatchlistItemDto>> GetWatchlistItemsAsync(int watchlistId, int userId);

		/// <summary>
		/// Adds an asset to a watchlist
		/// </summary>
		/// <param name="watchlistId">Watchlist ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <param name="addItemDto">Item data to add</param>
		/// <returns>Created watchlist item DTO</returns>
		Task<WatchlistItemDto> AddItemToWatchlistAsync(int watchlistId, int userId, AddWatchlistItemDto addItemDto);

		/// <summary>
		/// Removes an item from a watchlist
		/// </summary>
		/// <param name="watchlistItemId">Watchlist item ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> RemoveItemFromWatchlistAsync(int watchlistItemId, int userId);

		/// <summary>
		/// Deletes a watchlist
		/// </summary>
		/// <param name="watchlistId">Watchlist ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> DeleteWatchlistAsync(int watchlistId, int userId);

		/// <summary>
		/// Gets watchlist with real-time quotes
		/// </summary>
		/// <param name="watchlistId">Watchlist ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Collection of quotes for watchlist items</returns>
		Task<IEnumerable<MarketQuoteDto>> GetWatchlistQuotesAsync(int watchlistId, int userId);
	}
}