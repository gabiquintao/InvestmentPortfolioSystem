// =============================================
// IPS.Core/Entities/Watchlist.cs
// Description: Represents a user's watchlist
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a user-created watchlist for monitoring assets
	/// </summary>
	[Table("Watchlists")]
	public class Watchlist
	{
		/// <summary>
		/// Unique identifier for the watchlist
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WatchlistId { get; set; }

		/// <summary>
		/// ID of the user who owns the watchlist
		/// </summary>
		[Required]
		public int UserId { get; set; }

		/// <summary>
		/// Name of the watchlist
		/// </summary>
		[Required]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Description of the watchlist
		/// </summary>
		[StringLength(500)]
		public string? Description { get; set; }

		/// <summary>
		/// Indicates if this is the user's default watchlist
		/// </summary>
		public bool IsDefault { get; set; } = false;

		/// <summary>
		/// Date and time when the watchlist was created
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the watchlist was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The user who owns this watchlist
		/// </summary>
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		/// <summary>
		/// Collection of items in this watchlist
		/// </summary>
		public virtual ICollection<WatchlistItem> Items { get; set; } = new List<WatchlistItem>();
	}
}
