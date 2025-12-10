// =============================================
// IPS.Core/DTOs/Auth/LoginResponseDto.cs
// Description: DTO for login response
// =============================================

namespace IPS.Core.DTOs.Auth
{
	/// <summary>
	/// Data transfer object for login response
	/// </summary>
	public class LoginResponseDto
	{
		/// <summary>
		/// JWT authentication token
		/// </summary>
		public string Token { get; set; } = string.Empty;

		/// <summary>
		/// Token expiration date
		/// </summary>
		public DateTime ExpiresAt { get; set; }

		/// <summary>
		/// User ID
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// User's email
		/// </summary>
		public string Email { get; set; } = string.Empty;
	}
}