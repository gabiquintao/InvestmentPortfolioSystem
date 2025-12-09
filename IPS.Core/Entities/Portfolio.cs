// =============================================
// IPS.Core/Entities/Portfolio.cs
// Description: Represents an investment portfolio
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents an investment portfolio containing multiple asset positions.
	/// Each user can have multiple portfolios with different strategies.
	/// </summary>
	[Table("Portfolios")]
	public class Portfolio
	{
		/// <summary>
		/// Unique identifier for the portfolio
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PortfolioId { get; set; }

		/// <summary>
		/// ID of the user who owns this portfolio
		/// </summary>
		[Required]
		public int UserId { get; set; }

		/// <summary>
		/// Name of the portfolio
		/// </summary>
		[Required]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Description of the portfolio strategy or purpose
		/// </summary>
		[StringLength(500)]
		public string? Description { get; set; }

		/// <summary>
		/// Base currency for the portfolio (e.g., USD, EUR)
		/// </summary>
		[Required]
		[StringLength(3)]
		public string BaseCurrency { get; set; } = "USD";

		/// <summary>
		/// Indicates if this is the user's default portfolio
		/// </summary>
		public bool IsDefault { get; set; } = false;

		/// <summary>
		/// Date and time when the portfolio was created
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the portfolio was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The user who owns this portfolio
		/// </summary>
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		/// <summary>
		/// Collection of asset positions in this portfolio
		/// </summary>
		public virtual ICollection<PortfolioPosition> Positions { get; set; } = new List<PortfolioPosition>();

		/// <summary>
		/// Collection of transactions for this portfolio
		/// </summary>
		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
	}
}