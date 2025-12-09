// =============================================
// IPS.Core/Entities/MarketDataCache.cs
// Description: Caches market data to reduce API calls
// =============================================

using IPS.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Caches real-time market data to reduce external API calls
	/// </summary>
	[Table("MarketDataCache")]
	public class MarketDataCache
	{
		/// <summary>
		/// Unique identifier for the cache entry
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CacheId { get; set; }

		/// <summary>
		/// ID of the asset for this cached data
		/// </summary>
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// Current price of the asset
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 4)")]
		public decimal Price { get; set; }

		/// <summary>
		/// Opening price for the trading day
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal? OpenPrice { get; set; }

		/// <summary>
		/// Highest price for the trading day
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal? HighPrice { get; set; }

		/// <summary>
		/// Lowest price for the trading day
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal? LowPrice { get; set; }

		/// <summary>
		/// Trading volume
		/// </summary>
		public long? Volume { get; set; }

		/// <summary>
		/// Price change from previous close
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal? Change { get; set; }

		/// <summary>
		/// Percentage change from previous close
		/// </summary>
		[Column(TypeName = "decimal(10, 4)")]
		public decimal? ChangePercent { get; set; }

		/// <summary>
		/// Date and time of the last update
		/// </summary>
		public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Source of the data (e.g., AlphaVantage, CoinGecko)
		/// </summary>
		[Required]
		[StringLength(50)]
		public string DataSource { get; set; } = string.Empty;

		// Navigation properties
		/// <summary>
		/// The asset for this cached data
		/// </summary>
		[ForeignKey("AssetId")]
		public virtual Asset? Asset { get; set; }
	}
}
