// =============================================
// IPS.Core/Entities/User.cs
// Description: Represents a user in the system
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a registered user in the investment portfolio system.
	/// Users can own multiple portfolios and create watchlists.
	/// </summary>
	[Table("Users")]
	public class User
	{
		/// <summary>
		/// Unique identifier for the user
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

		/// <summary>
		/// Unique username for login
		/// </summary>
		[Required]
		[StringLength(50)]
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// User's email address (unique)
		/// </summary>
		[Required]
		[StringLength(100)]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Hashed password for authentication
		/// </summary>
		[Required]
		[StringLength(256)]
		public string PasswordHash { get; set; } = string.Empty;

		/// <summary>
		/// User's first name
		/// </summary>
		[StringLength(50)]
		public string? FirstName { get; set; }

		/// <summary>
		/// User's last name
		/// </summary>
		[StringLength(50)]
		public string? LastName { get; set; }

		/// <summary>
		/// Indicates if the user account is active
		/// </summary>
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Date and time when the user was created
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the user was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// Collection of portfolios owned by this user
		/// </summary>
		public virtual ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

		/// <summary>
		/// Collection of watchlists created by this user
		/// </summary>
		public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();

		/// <summary>
		/// Collection of price alerts set by this user
		/// </summary>
		public virtual ICollection<PriceAlert> PriceAlerts { get; set; } = new List<PriceAlert>();

		/// <summary>
		/// Gets the user's full name
		/// </summary>
		[NotMapped]
		public string FullName => $"{FirstName} {LastName}".Trim();
	}
}
