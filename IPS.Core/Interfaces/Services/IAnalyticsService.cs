// =============================================
// IPS.Core/Interfaces/Services/IAnalyticsService.cs
// Description: Analytics service interface
// =============================================

using IPS.Core.DTOs.Analytics;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for portfolio analytics
	/// </summary>
	public interface IAnalyticsService
	{
		/// <summary>
		/// Calculates portfolio performance metrics
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Performance metrics DTO</returns>
		Task<PortfolioPerformanceDto> GetPortfolioPerformanceAsync(int portfolioId, int userId);

		/// <summary>
		/// Gets asset allocation breakdown
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Collection of asset allocation DTOs</returns>
		Task<IEnumerable<AssetAllocationDto>> GetAssetAllocationAsync(int portfolioId, int userId);

		/// <summary>
		/// Gets portfolio value history over time
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <param name="period">Time period (7d, 1m, 3m, 1y, all)</param>
		/// <returns>Collection of historical data points</returns>
		Task<IEnumerable<PortfolioHistoryDto>> GetPortfolioHistoryAsync(int portfolioId, int userId, string period = "1m");
	}
}