// =============================================
// IPS.Core/DTOs/Watchlist/CreateWatchlistDto.cs
// Description: DTO for creating a watchlist
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Watchlist
{
	/// <summary>
	/// Data transfer object for creating a new watchlist
	/// </summary>
	public class CreateWatchlistDto
	{
		/// <summary>
		/// Watchlist name
		/// </summary>
		[Required(ErrorMessage = "Watchlist name is required")]
		[StringLength(100, MinimumLength = 1)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Watchlist description
		/// </summary>
		[StringLength(500)]
		public string? Description { get; set; }

		/// <summary>
		/// Set as default watchlist
		/// </summary>
		public bool IsDefault { get; set; } = false;
	}
}
