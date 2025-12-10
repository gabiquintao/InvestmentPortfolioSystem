// =============================================
// IPS.Core/DTOs/Position/PositionDto.cs
// Description: DTO for portfolio position
// =============================================

namespace IPS.Core.DTOs.Position
{
	/// <summary>
	/// Data transfer object for portfolio position information
	/// </summary>
	public class PositionDto
	{
		/// <summary>
		/// Position ID
		/// </summary>
		public int PositionId { get; set; }

		/// <summary>
		/// Portfolio ID
		/// </summary>
		public int PortfolioId { get; set; }

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
		/// Quantity held
		/// </summary>
		public decimal Quantity { get; set; }

		/// <summary>
		/// Average purchase price
		/// </summary>
		public decimal AveragePurchasePrice { get; set; }

		/// <summary>
		/// Current market price
		/// </summary>
		public decimal CurrentPrice { get; set; }

		/// <summary>
		/// Total invested
		/// </summary>
		public decimal TotalInvested { get; set; }

		/// <summary>
		/// Current value
		/// </summary>
		public decimal CurrentValue { get; set; }

		/// <summary>
		/// Unrealized gain/loss
		/// </summary>
		public decimal UnrealizedGainLoss { get; set; }

		/// <summary>
		/// Return percentage
		/// </summary>
		public decimal ReturnPercentage { get; set; }

		/// <summary>
		/// Last price update
		/// </summary>
		public DateTime? LastPriceUpdate { get; set; }
	}
}