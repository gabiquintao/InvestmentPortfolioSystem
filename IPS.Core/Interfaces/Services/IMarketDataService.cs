// =============================================
// IPS.Core/Interfaces/Services/IMarketDataService.cs
// Description: Market data service interface
// =============================================

using IPS.Core.DTOs.Market;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for market data operations
	/// </summary>
	public interface IMarketDataService
	{
		/// <summary>
		/// Gets real-time quote for an asset
		/// </summary>
		/// <param name="symbol">Asset symbol</param>
		/// <returns>Market quote DTO</returns>
		Task<MarketQuoteDto?> GetQuoteAsync(string symbol);

		/// <summary>
		/// Gets multiple quotes in a batch
		/// </summary>
		/// <param name="symbols">Collection of asset symbols</param>
		/// <returns>Collection of market quotes</returns>
		Task<IEnumerable<MarketQuoteDto>> GetBatchQuotesAsync(IEnumerable<string> symbols);

		/// <summary>
		/// Searches for assets by query string
		/// </summary>
		/// <param name="query">Search query</param>
		/// <returns>Collection of matching assets</returns>
		Task<IEnumerable<MarketQuoteDto>> SearchAssetsAsync(string query);

		/// <summary>
		/// Converts currency amount
		/// </summary>
		/// <param name="from">Source currency</param>
		/// <param name="to">Target currency</param>
		/// <param name="amount">Amount to convert</param>
		/// <returns>Converted amount</returns>
		Task<decimal> ConvertCurrencyAsync(string from, string to, decimal amount);

		/// <summary>
		/// Gets cached quote or fetches from external API
		/// </summary>
		/// <param name="symbol">Asset symbol</param>
		/// <param name="maxCacheAge">Maximum age of cached data in minutes</param>
		/// <returns>Market quote DTO</returns>
		Task<MarketQuoteDto?> GetCachedQuoteAsync(string symbol, int maxCacheAge = 5);

		/// <summary>
		/// Updates cache for multiple symbols
		/// </summary>
		/// <param name="symbols">Collection of asset symbols</param>
		/// <returns>Number of updated quotes</returns>
		Task<int> RefreshCacheAsync(IEnumerable<string> symbols);
	}
}