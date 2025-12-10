// =============================================
// IPS.Core/DTOs/Watchlist/AddWatchlistItemDto.cs
// Description: DTO for adding item to watchlist
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Watchlist
{
	/// <summary>
	/// Data transfer object for adding an item to a watchlist
	/// </summary>
	public class AddWatchlistItemDto
	{
		/// <summary>
		/// Asset symbol to add
		/// </summary>
		[Required(ErrorMessage = "Asset symbol is required")]
		[StringLength(20)]
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Optional notes
		/// </summary>
		[StringLength(500)]
		public string? Notes { get; set; }
	}
}