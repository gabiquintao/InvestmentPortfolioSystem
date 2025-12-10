// =============================================
// IPS.Core/DTOs/Analytics/AssetAllocationDto.cs
// Description: DTO for asset allocation breakdown
// =============================================

namespace IPS.Core.DTOs.Analytics
{
	/// <summary>
	/// Data transfer object for asset allocation by type
	/// </summary>
	public class AssetAllocationDto
	{
		/// <summary>
		/// Type of asset (Stock, Crypto, ETF, etc.)
		/// </summary>
		public string AssetType { get; set; } = string.Empty;

		/// <summary>
		/// Number of different assets of this type
		/// </summary>
		public int AssetCount { get; set; }

		/// <summary>
		/// Total value of assets of this type
		/// </summary>
		public decimal TotalValue { get; set; }

		/// <summary>
		/// Percentage of portfolio allocated to this type
		/// </summary>
		public decimal Percentage { get; set; }
	}
}