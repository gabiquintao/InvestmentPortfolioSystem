// =============================================
// IPS.Core/DTOs/Watchlist/WatchlistDto.cs
// Description: DTO for watchlist information
// =============================================

namespace IPS.Core.DTOs.Watchlist
{
	/// <summary>
	/// Data transfer object for watchlist information
	/// </summary>
	public class WatchlistDto
	{
		/// <summary>
		/// Watchlist ID
		/// </summary>
		public int WatchlistId { get; set; }

		/// <summary>
		/// User ID who owns the watchlist
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Watchlist name
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Watchlist description
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Is this the default watchlist
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// Number of items in the watchlist
		/// </summary>
		public int ItemCount { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		public DateTime UpdatedAt { get; set; }
	}
}