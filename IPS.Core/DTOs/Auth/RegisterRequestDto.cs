// =============================================
// IPS.Core/DTOs/Auth/RegisterRequestDto.cs
// Description: DTO for user registration
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Auth
{
	/// <summary>
	/// Data transfer object for user registration
	/// </summary>
	public class RegisterRequestDto
	{
		/// <summary>
		/// Desired username
		/// </summary>
		[Required(ErrorMessage = "Username is required")]
		[StringLength(50, MinimumLength = 3)]
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// Email address
		/// </summary>
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Password
		/// </summary>
		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Password confirmation
		/// </summary>
		[Required(ErrorMessage = "Password confirmation is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; } = string.Empty;

		/// <summary>
		/// First name
		/// </summary>
		[StringLength(50)]
		public string? FirstName { get; set; }

		/// <summary>
		/// Last name
		/// </summary>
		[StringLength(50)]
		public string? LastName { get; set; }
	}
}
