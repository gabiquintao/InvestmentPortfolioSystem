// =============================================
// IPS.Core/Interfaces/Services/IPortfolioService.cs
// Description: Portfolio service interface
// =============================================

using IPS.Core.DTOs.Portfolio;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for portfolio operations
	/// </summary>
	public interface IPortfolioService
	{
		/// <summary>
		/// Gets all portfolios for a user
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <returns>Collection of portfolio DTOs</returns>
		Task<IEnumerable<PortfolioDto>> GetUserPortfoliosAsync(int userId);

		/// <summary>
		/// Gets a portfolio by ID
		/// </summary>s
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Portfolio DTO if found and owned by user</returns>
		Task<PortfolioDto?> GetPortfolioByIdAsync(int portfolioId, int userId);

		/// <summary>
		/// Creates a new portfolio
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="createDto">Portfolio creation data</param>
		/// <returns>Created portfolio DTO</returns>
		Task<PortfolioDto> CreatePortfolioAsync(int userId, CreatePortfolioDto createDto);

		/// <summary>
		/// Updates an existing portfolio
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <param name="updateDto">Portfolio update data</param>
		/// <returns>Updated portfolio DTO</returns>
		Task<PortfolioDto> UpdatePortfolioAsync(int portfolioId, int userId, CreatePortfolioDto updateDto);

		/// <summary>
		/// Deletes a portfolio
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> DeletePortfolioAsync(int portfolioId, int userId);

		/// <summary>
		/// Gets portfolio summary with performance metrics
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Portfolio summary data</returns>
		Task<PortfolioDto> GetPortfolioSummaryAsync(int portfolioId, int userId);
	}
}