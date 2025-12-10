// =============================================
// IPS.Core/Interfaces/Services/IPositionService.cs
// Description: Position service interface
// =============================================

using IPS.Core.DTOs.Position;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for portfolio position operations
	/// </summary>
	public interface IPositionService
	{
		/// <summary>
		/// Gets all positions for a portfolio
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Collection of position DTOs</returns>
		Task<IEnumerable<PositionDto>> GetPortfolioPositionsAsync(int portfolioId, int userId);

		/// <summary>
		/// Gets a specific position by ID
		/// </summary>
		/// <param name="positionId">Position ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Position DTO if found</returns>
		Task<PositionDto?> GetPositionByIdAsync(int positionId, int userId);

		/// <summary>
		/// Updates position prices with current market data
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Number of positions updated</returns>
		Task<int> UpdatePositionPricesAsync(int portfolioId, int userId);

		/// <summary>
		/// Removes a position from a portfolio
		/// </summary>
		/// <param name="positionId">Position ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> DeletePositionAsync(int positionId, int userId);
	}
}
