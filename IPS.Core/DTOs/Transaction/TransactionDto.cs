// =============================================
// IPS.Core/DTOs/Transaction/TransactionDto.cs
// Description: DTO for transaction
// =============================================

namespace IPS.Core.DTOs.Transaction
{
	/// <summary>
	/// Data transfer object for transaction information
	/// </summary>
	public class TransactionDto
	{
		/// <summary>
		/// Transaction ID
		/// </summary>
		public int TransactionId { get; set; }

		/// <summary>
		/// Portfolio ID
		/// </summary>
		public int PortfolioId { get; set; }

		/// <summary>
		/// Asset ID
		/// </summary>
		public int AssetId { get; set; }

		/// <summary>
		/// Asset symbol
		/// </summary>
		public string Symbol { get; set; } = string.Empty;

		/// <summary>
		/// Asset name
		/// </summary>
		public string AssetName { get; set; } = string.Empty;

		/// <summary>
		/// Transaction type (Buy/Sell)
		/// </summary>
		public string TransactionType { get; set; } = string.Empty;

		/// <summary>
		/// Quantity traded
		/// </summary>
		public decimal Quantity { get; set; }

		/// <summary>
		/// Price per unit
		/// </summary>
		public decimal PricePerUnit { get; set; }

		/// <summary>
		/// Total amount
		/// </summary>
		public decimal TotalAmount { get; set; }

		/// <summary>
		/// Commission paid
		/// </summary>
		public decimal Commission { get; set; }

		/// <summary>
		/// Transaction date
		/// </summary>
		public DateTime TransactionDate { get; set; }

		/// <summary>
		/// Notes
		/// </summary>
		public string? Notes { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }
	}
}