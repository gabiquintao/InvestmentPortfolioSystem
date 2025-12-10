// =============================================
// IPS.Core/DTOs/Analytics/PortfolioPerformanceDto.cs
// Description: DTO for portfolio performance metrics
// =============================================

namespace IPS.Core.DTOs.Analytics
{
	/// <summary>
	/// Data transfer object for portfolio performance metrics
	/// </summary>
	public class PortfolioPerformanceDto
	{
		/// <summary>
		/// Portfolio ID
		/// </summary>
		public int PortfolioId { get; set; }

		/// <summary>
		/// Current total value of the portfolio
		/// </summary>
		public decimal TotalValue { get; set; }

		/// <summary>
		/// Total amount invested
		/// </summary>
		public decimal TotalInvested { get; set; }

		/// <summary>
		/// Total gain or loss
		/// </summary>
		public decimal TotalGainLoss { get; set; }

		/// <summary>
		/// Overall return percentage
		/// </summary>
		public decimal ReturnPercentage { get; set; }

		/// <summary>
		/// Daily change in value
		/// </summary>
		public decimal DailyChange { get; set; }

		/// <summary>
		/// Daily change percentage
		/// </summary>
		public decimal DailyChangePercent { get; set; }

		/// <summary>
		/// When these metrics were calculated
		/// </summary>
		public DateTime CalculatedAt { get; set; }
	}
}
