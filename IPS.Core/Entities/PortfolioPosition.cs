// =============================================
// IPS.Core/Entities/PortfolioPosition.cs
// Description: Represents a holding in a portfolio
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a current position (holding) of an asset in a portfolio
	/// </summary>
	[Table("PortfolioPositions")]
	public class PortfolioPosition
	{
		/// <summary>
		/// Unique identifier for the position
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PositionId { get; set; }

		/// <summary>
		/// ID of the portfolio containing this position
		/// </summary>
		[Required]
		public int PortfolioId { get; set; }

		/// <summary>
		/// ID of the asset being held
		/// </summary>
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// Quantity of the asset held (supports fractional shares)
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 8)")]
		public decimal Quantity { get; set; }

		/// <summary>
		/// Average purchase price per unit
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 4)")]
		public decimal AveragePurchasePrice { get; set; }

		/// <summary>
		/// Current market price (cached)
		/// </summary>
		[Column(TypeName = "decimal(18, 4)")]
		public decimal? CurrentPrice { get; set; }

		/// <summary>
		/// Date and time of the last price update
		/// </summary>
		public DateTime? LastPriceUpdate { get; set; }

		/// <summary>
		/// Date and time when the position was created
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the position was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The portfolio containing this position
		/// </summary>
		[ForeignKey("PortfolioId")]
		public virtual Portfolio? Portfolio { get; set; }

		/// <summary>
		/// The asset being held
		/// </summary>
		[ForeignKey("AssetId")]
		public virtual Asset? Asset { get; set; }

		// Calculated properties
		/// <summary>
		/// Total amount invested in this position
		/// </summary>
		[NotMapped]
		public decimal TotalInvested => Quantity * AveragePurchasePrice;

		/// <summary>
		/// Current total value of this position
		/// </summary>
		[NotMapped]
		public decimal CurrentValue => Quantity * (CurrentPrice ?? AveragePurchasePrice);

		/// <summary>
		/// Unrealized gain or loss on this position
		/// </summary>
		[NotMapped]
		public decimal UnrealizedGainLoss => CurrentValue - TotalInvested;

		/// <summary>
		/// Percentage return on this position
		/// </summary>
		[NotMapped]
		public decimal ReturnPercentage => TotalInvested > 0
			? (UnrealizedGainLoss / TotalInvested) * 100
			: 0;
	}
}