// =============================================
// IPS.Core/DTOs/Analytics/PortfolioHistoryDto.cs
// Description: DTO for portfolio historical data
// =============================================

namespace IPS.Core.DTOs.Analytics
{
	/// <summary>
	/// Data transfer object for portfolio value history
	/// </summary>
	public class PortfolioHistoryDto
	{
		/// <summary>
		/// Date of the data point
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Total portfolio value on this date
		/// </summary>
		public decimal TotalValue { get; set; }

		/// <summary>
		/// Gain or loss on this date
		/// </summary>
		public decimal GainLoss { get; set; }

		/// <summary>
		/// Return percentage on this date
		/// </summary>
		public decimal ReturnPercentage { get; set; }
	}
}
