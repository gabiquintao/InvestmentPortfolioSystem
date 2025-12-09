// =============================================
// IPS.Core/Entities/Asset.cs
// Description: Represents a tradable asset
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a tradable asset (stock, crypto, ETF, etc.)
	/// </summary>
	[Table("Assets")]
	public class Asset
	{
		/// <summary>
		/// Unique identifier for the asset
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AssetId { get; set; }

		/// <summary>
		/// Trading symbol (e.g., AAPL, BTC-USD)
		/// </summary>
		[Required]
		[StringLength(20)]
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Full name of the asset
		/// </summary>
		[Required]
		[StringLength(200)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Type of asset (Stock, Crypto, ETF, etc.)
		/// </summary>
		[Required]
		[StringLength(20)]
		public string AssetType { get; set; } = string.Empty;

		/// <summary>
		/// Exchange where the asset is traded
		/// </summary>
		[StringLength(50)]
		public string? Exchange { get; set; }

		/// <summary>
		/// Currency in which the asset is traded
		/// </summary>
		[Required]
		[StringLength(3)]
		public string Currency { get; set; } = "USD";

		/// <summary>
		/// Detailed description of the asset
		/// </summary>
		[StringLength(1000)]
		public string? Description { get; set; }

		/// <summary>
		/// Indicates if the asset is currently active for trading
		/// </summary>
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Date and time when the asset was added to the system
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the asset was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// Collection of portfolio positions for this asset
		/// </summary>
		public virtual ICollection<PortfolioPosition> Positions { get; set; } = new List<PortfolioPosition>();

		/// <summary>
		/// Collection of transactions for this asset
		/// </summary>
		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

		/// <summary>
		/// Collection of watchlist items for this asset
		/// </summary>
		public virtual ICollection<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();

		/// <summary>
		/// Collection of price alerts for this asset
		/// </summary>
		public virtual ICollection<PriceAlert> PriceAlerts { get; set; } = new List<PriceAlert>();

		/// <summary>
		/// Market data cache for this asset
		/// </summary>
		public virtual MarketDataCache? MarketDataCache { get; set; }
	}
}