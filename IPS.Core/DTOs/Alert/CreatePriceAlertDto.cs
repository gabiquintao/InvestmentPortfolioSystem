// =============================================
// IPS.Core/DTOs/Alert/CreatePriceAlertDto.cs
// Description: DTO for creating a price alert
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Alert
{
	/// <summary>
	/// Data transfer object for creating a new price alert
	/// </summary>
	public class CreatePriceAlertDto
	{
		/// <summary>
		/// Asset symbol
		/// </summary>
		[Required(ErrorMessage = "Asset symbol is required")]
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Target price
		/// </summary>
		[Required]
		[Range(0.0001, double.MaxValue, ErrorMessage = "Target price must be greater than 0")]
		public decimal TargetPrice { get; set; }

		/// <summary>
		/// Alert type (Above/Below)
		/// </summary>
		[Required(ErrorMessage = "Alert type is required")]
		public string AlertType { get; set; } = string.Empty;
	}
}