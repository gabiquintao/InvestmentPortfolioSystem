// =============================================
// IPS.Core/Interfaces/Services/IAuthService.cs
// Description: Authentication service interface
// =============================================

using IPS.Core.DTOs.Auth;

namespace IPS.Core.Interfaces.Services
{
	/// <summary>
	/// Service interface for authentication operations
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Registers a new user
		/// </summary>
		/// <param name="request">Registration request data</param>
		/// <returns>Login response with JWT token</returns>
		Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request);

		/// <summary>
		/// Authenticates a user and generates JWT token
		/// </summary>
		/// <param name="request">Login request data</param>
		/// <returns>Login response with JWT token</returns>
		Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

		/// <summary>
		/// Validates a JWT token
		/// </summary>
		/// <param name="token">JWT token to validate</param>
		/// <returns>True if token is valid, false otherwise</returns>
		Task<bool> ValidateTokenAsync(string token);

		/// <summary>
		/// Gets user ID from JWT token
		/// </summary>
		/// <param name="token">JWT token</param>
		/// <returns>User ID if valid, null otherwise</returns>
		Task<int?> GetUserIdFromTokenAsync(string token);
	}
}
