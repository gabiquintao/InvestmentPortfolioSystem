// =============================================
// IPS.Core/DTOs/Transaction/CreateTransactionDto.cs
// Description: DTO for creating a transaction
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Transaction
{
	/// <summary>
	/// Data transfer object for creating a new transaction
	/// </summary>
	public class CreateTransactionDto
	{
		/// <summary>
		/// Portfolio ID
		/// </summary>
		[Required(ErrorMessage = "Portfolio ID is required")]
		public int PortfolioId { get; set; }

		/// <summary>
		/// Asset symbol (e.g., AAPL, BTC-USD)
		/// </summary>
		[Required(ErrorMessage = "Asset symbol is required")]
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Transaction type (Buy/Sell)
		/// </summary>
		[Required(ErrorMessage = "Transaction type is required")]
		public string TransactionType { get; set; } = string.Empty;

		/// <summary>
		/// Quantity to trade
		/// </summary>
		[Required]
		[Range(0.00000001, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
		public decimal Quantity { get; set; }

		/// <summary>
		/// Price per unit
		/// </summary>
		[Required]
		[Range(0.0001, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public decimal PricePerUnit { get; set; }

		/// <summary>
		/// Commission (optional)
		/// </summary>
		[Range(0, double.MaxValue)]
		public decimal Commission { get; set; } = 0;

		/// <summary>
		/// Transaction date (defaults to now)
		/// </summary>
		public DateTime? TransactionDate { get; set; }

		/// <summary>
		/// Optional notes
		/// </summary>
		[StringLength(500)]
		public string? Notes { get; set; }
	}
}