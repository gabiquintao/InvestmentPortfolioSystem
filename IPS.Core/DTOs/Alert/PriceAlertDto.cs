// =============================================
// IPS.Core/DTOs/Alert/PriceAlertDto.cs
// Description: DTO for price alert
// =============================================

namespace IPS.Core.DTOs.Alert
{
	/// <summary>
	/// Data transfer object for price alert
	/// </summary>
	public class PriceAlertDto
	{
		/// <summary>
		/// Alert ID
		/// </summary>
		public int AlertId { get; set; }

		/// <summary>
		/// User ID
		/// </summary>
		public int UserId { get; set; }

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
		/// Target price
		/// </summary>
		public decimal TargetPrice { get; set; }

		/// <summary>
		/// Alert type (Above/Below)
		/// </summary>
		public string AlertType { get; set; } = string.Empty;

		/// <summary>
		/// Is active
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Is triggered
		/// </summary>
		public bool IsTriggered { get; set; }

		/// <summary>
		/// Triggered at
		/// </summary>
		public DateTime? TriggeredAt { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }
	}
}