// =============================================
// IPS.Core/Interfaces/Services/ITransactionService.cs
// Description: Transaction service interface
// =============================================

using IPS.Core.DTOs.Transaction;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for transaction operations
	/// </summary>
	public interface ITransactionService
	{
		/// <summary>
		/// Creates a new transaction (buy/sell)
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="createDto">Transaction creation data</param>
		/// <returns>Created transaction DTO</returns>
		Task<TransactionDto> CreateTransactionAsync(int userId, CreateTransactionDto createDto);

		/// <summary>
		/// Gets all transactions for a portfolio
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Collection of transaction DTOs</returns>
		Task<IEnumerable<TransactionDto>> GetPortfolioTransactionsAsync(int portfolioId, int userId);

		/// <summary>
		/// Gets a specific transaction by ID
		/// </summary>
		/// <param name="transactionId">Transaction ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>Transaction DTO if found</returns>
		Task<TransactionDto?> GetTransactionByIdAsync(int transactionId, int userId);

		/// <summary>
		/// Gets transaction history with filters
		/// </summary>
		/// <param name="portfolioId">Portfolio ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <param name="startDate">Start date filter</param>
		/// <param name="endDate">End date filter</param>
		/// <param name="assetSymbol">Optional asset symbol filter</param>
		/// <returns>Filtered transaction collection</returns>
		Task<IEnumerable<TransactionDto>> GetTransactionHistoryAsync(
			int portfolioId,
			int userId,
			DateTime? startDate = null,
			DateTime? endDate = null,
			string? assetSymbol = null);

		/// <summary>
		/// Deletes a transaction and adjusts portfolio positions
		/// </summary>
		/// <param name="transactionId">Transaction ID</param>
		/// <param name="userId">User ID for ownership validation</param>
		/// <returns>True if deleted successfully</returns>
		Task<bool> DeleteTransactionAsync(int transactionId, int userId);
	}
}