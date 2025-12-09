// =============================================
// IPS.Core/Entities/WatchlistItem.cs
// Description: Represents an item in a watchlist
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents an asset in a watchlist
	/// </summary>
	[Table("WatchlistItems")]
	public class WatchlistItem
	{
		/// <summary>
		/// Unique identifier for the watchlist item
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WatchlistItemId { get; set; }

		/// <summary>
		/// ID of the watchlist containing this item
		/// </summary>
		[Required]
		public int WatchlistId { get; set; }

		/// <summary>
		/// ID of the asset in the watchlist
		/// </summary>
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// Date and time when the item was added
		/// </summary>
		public DateTime AddedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Optional notes about why this asset is watched
		/// </summary>
		[StringLength(500)]
		public string? Notes { get; set; }

		// Navigation properties
		/// <summary>
		/// The watchlist containing this item
		/// </summary>
		[ForeignKey("WatchlistId")]
		public virtual Watchlist? Watchlist { get; set; }

		/// <summary>
		/// The asset being watched
		/// </summary>
		[ForeignKey("AssetId")]
		public virtual Asset? Asset { get; set; }
	}
}