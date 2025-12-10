// =============================================
// IPS.Core/DTOs/Portfolio/CreatePortfolioDto.cs
// Description: DTO for creating a portfolio
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Portfolio
{
	/// <summary>
	/// Data transfer object for creating a new portfolio
	/// </summary>
	public class CreatePortfolioDto
	{
		/// <summary>
		/// Portfolio name
		/// </summary>
		[Required(ErrorMessage = "Portfolio name is required")]
		[StringLength(100, MinimumLength = 1)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Portfolio description
		/// </summary>
		[StringLength(500)]
		public string? Description { get; set; }

		/// <summary>
		/// Base currency (default: USD)
		/// </summary>
		[Required]
		[StringLength(3, MinimumLength = 3)]
		public string BaseCurrency { get; set; } = "USD";

		/// <summary>
		/// Set as default portfolio
		/// </summary>
		public bool IsDefault { get; set; } = false;
	}
}