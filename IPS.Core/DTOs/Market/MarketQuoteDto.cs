// =============================================
// IPS.Core/DTOs/Market/MarketQuoteDto.cs
// Description: DTO for market quote
// =============================================

namespace IPS.Core.DTOs.Market
{
	/// <summary>
	/// Data transfer object for real-time market quote
	/// </summary>
	public class MarketQuoteDto
	{
		/// <summary>
		/// Asset symbol
		/// </summary>
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Asset name
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Current price
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Opening price
		/// </summary>
		public decimal? OpenPrice { get; set; }

		/// <summary>
		/// High price
		/// </summary>
		public decimal? HighPrice { get; set; }

		/// <summary>
		/// Low price
		/// </summary>
		public decimal? LowPrice { get; set; }

		/// <summary>
		/// Trading volume
		/// </summary>
		public long? Volume { get; set; }

		/// <summary>
		/// Price change
		/// </summary>
		public decimal? Change { get; set; }

		/// <summary>
		/// Percentage change
		/// </summary>
		public decimal? ChangePercent { get; set; }

		/// <summary>
		/// Data source
		/// </summary>
		public string DataSource { get; set; } = string.Empty;

		/// <summary>
		/// Last updated timestamp
		/// </summary>
		public DateTime LastUpdated { get; set; }
	}
}