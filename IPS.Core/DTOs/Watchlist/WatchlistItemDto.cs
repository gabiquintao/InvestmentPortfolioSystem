// =============================================
// IPS.Core/DTOs/Watchlist/WatchlistItemDto.cs
// Description: DTO for watchlist item
// =============================================

using IPS.Core.DTOs.Market;

namespace IPS.Core.DTOs.Watchlist
{
	/// <summary>
	/// Data transfer object for a watchlist item with market data
	/// </summary>
	public class WatchlistItemDto
	{
		/// <summary>
		/// Watchlist item ID
		/// </summary>
		public int WatchlistItemId { get; set; }

		/// <summary>
		/// Watchlist ID
		/// </summary>
		public int WatchlistId { get; set; }

		/// <summary>
		/// Asset ID
		/// </summary>
		public int AssetId { get; set; }

		/// <summary>
		/// Asset symbol
		/// </summary>
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Asset name
		/// </summary>
		public string AssetName { get; set; } = string.Empty;

		/// <summary>
		/// Asset type
		/// </summary>
		public string AssetType { get; set; } = string.Empty;

		/// <summary>
		/// Optional notes about why this asset is watched
		/// </summary>
		public string? Notes { get; set; }

		/// <summary>
		/// Date when the item was added to the watchlist
		/// </summary>
		public DateTime AddedAt { get; set; }

		/// <summary>
		/// Current market quote for this asset (optional)
		/// </summary>
		public MarketQuoteDto? CurrentQuote { get; set; }
	}
}