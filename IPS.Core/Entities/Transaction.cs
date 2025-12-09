// =============================================
// IPS.Core/Entities/Transaction.cs
// Description: Represents a buy/sell transaction
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a buy or sell transaction of an asset
	/// </summary>
	[Table("Transactions")]
	public class Transaction
	{
		/// <summary>
		/// Unique identifier for the transaction
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TransactionId { get; set; }

		/// <summary>
		/// ID of the portfolio for this transaction
		/// </summary>
		[Required]
		public int PortfolioId { get; set; }

		/// <summary>
		/// ID of the asset being traded
		/// </summary>
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// Type of transaction (Buy or Sell)
		/// </summary>
		[Required]
		[StringLength(10)]
		public string TransactionType { get; set; } = string.Empty;

		/// <summary>
		/// Quantity of the asset traded
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 8)")]
		public decimal Quantity { get; set; }

		/// <summary>
		/// Price per unit at the time of transaction
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 4)")]
		public decimal PricePerUnit { get; set; }

		/// <summary>
		/// Commission or fees paid for the transaction
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal Commission { get; set; } = 0;

		/// <summary>
		/// Date and time when the transaction occurred
		/// </summary>
		public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Optional notes about the transaction
		/// </summary>
		[StringLength(500)]
		public string? Notes { get; set; }

		/// <summary>
		/// Date and time when the transaction was recorded
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The portfolio for this transaction
		/// </summary>
		[ForeignKey("PortfolioId")]
		public virtual Portfolio? Portfolio { get; set; }

		/// <summary>
		/// The asset being traded
		/// </summary>
		[ForeignKey("AssetId")]
		public virtual Asset? Asset { get; set; }

		// Calculated properties
		/// <summary>
		/// Total amount for the transaction (excluding commission)
		/// </summary>
		[NotMapped]
		public decimal TotalAmount => Quantity * PricePerUnit;

		/// <summary>
		/// Total cost including commission
		/// </summary>
		[NotMapped]
		public decimal TotalCost => TotalAmount + Commission;
	}
}
