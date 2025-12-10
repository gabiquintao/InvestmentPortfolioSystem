// =============================================
// IPS.Core/DTOs/Portfolio/PortfolioDto.cs
// Description: DTO for portfolio information
// =============================================

namespace IPS.Core.DTOs.Portfolio
{
	/// <summary>
	/// Data transfer object for portfolio information
	/// </summary>
	public class PortfolioDto
	{
		/// <summary>
		/// Portfolio ID
		/// </summary>
		public int PortfolioId { get; set; }

		/// <summary>
		/// User ID
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Portfolio name
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Portfolio description
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Base currency
		/// </summary>
		public string BaseCurrency { get; set; } = "USD";

		/// <summary>
		/// Is default portfolio
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// Total value of the portfolio
		/// </summary>
		public decimal TotalValue { get; set; }

		/// <summary>
		/// Total invested amount
		/// </summary>
		public decimal TotalInvested { get; set; }

		/// <summary>
		/// Total unrealized gain/loss
		/// </summary>
		public decimal UnrealizedGainLoss { get; set; }

		/// <summary>
		/// Return percentage
		/// </summary>
		public decimal ReturnPercentage { get; set; }

		/// <summary>
		/// Number of positions
		/// </summary>
		public int PositionCount { get; set; }
	}
}