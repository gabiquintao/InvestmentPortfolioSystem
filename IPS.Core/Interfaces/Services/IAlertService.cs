// =============================================
// IPS.Core/Interfaces/Services/IAlertService.cs
// Description: Alert service interface
// =============================================

using IPS.Core.DTOs.Alert;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for price alert operations
	/// </summary>
	public interface IAlertService
	{
		/// <summary>
		/// Creates a new price alert
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="createDto">Alert creation data</param>
		/// <returns>Created alert DTO</returns>
		Task<PriceAlertDto> CreateAlertAsync(int userId, CreatePriceAlertDto createDto);

		/// <summary>
		/// Gets all alerts for a user
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="includeTriggered">Include triggered alerts</param>
		/// <returns>Collection of alert DTOs</returns>
		Task<IEnumerable<PriceAlertDto>> GetUserAlertsAsync(int userId, bool includeTriggered = false);

		/// <summary>
		/// Gets a specific alert by ID
		/// </summary>
		/// <param name="alertId">Alert ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Alert DTO if found</returns>
		Task<PriceAlertDto?> GetAlertByIdAsync(int alertId, int userId);

		/// <summary>
		/// Updates an alert
		/// </summary>
		/// <param name="alertId">Alert ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <param name="updateDto">Alert update data</param>
		/// <returns>Updated alert DTO</returns>
		Task<PriceAlertDto> UpdateAlertAsync(int alertId, int userId, CreatePriceAlertDto updateDto);

		/// <summary>
		/// Deletes an alert
		/// </summary>
		/// <param name="alertId">Alert ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> DeleteAlertAsync(int alertId, int userId);

		/// <summary>
		/// Checks all active alerts and triggers matching ones
		/// </summary>
		/// <returns>Collection of triggered alerts</returns>
		Task<IEnumerable<PriceAlertDto>> CheckAndTriggerAlertsAsync();
	}
}
