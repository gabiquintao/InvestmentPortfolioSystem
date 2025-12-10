// =============================================
// IPS.Core/DTOs/Auth/LoginRequestDto.cs
// Description: DTO for user login
// =============================================

using System.ComponentModel.DataAnnotations;

namespace IPS.Core.DTOs.Auth
{
	/// <summary>
	/// Data transfer object for user login request
	/// </summary>
	public class LoginRequestDto
	{
		/// <summary>
		/// Username or email for login
		/// </summary>
		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// User password
		/// </summary>
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = string.Empty;
	}
}