// =============================================
// IPS.Services/Implementations/AuthService.cs
// Description: Authentication service implementation with JWT
// =============================================

using IPS.Core.DTOs.Auth;
using IPS.Core.Entities;
using IPS.Core.Interfaces;
using IPS.Core.Interfaces.Services;
using IPS.Services.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolio.Services.Implementations
{
	/// <summary>
	/// Service for handling user authentication and JWT token generation
	/// Implements secure password hashing and token-based authentication
	/// </summary>
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;
		private readonly string _jwtSecretKey;
		private readonly string _jwtIssuer;
		private readonly string _jwtAudience;
		private readonly int _jwtExpirationMinutes;

		/// <summary>
		/// Initializes a new instance of the AuthService
		/// </summary>
		/// <param name="unitOfWork">Unit of work for database operations</param>
		/// <param name="configuration">Application configuration</param>
		public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

			// Load JWT configuration
			_jwtSecretKey = _configuration["Jwt:SecretKey"]
				?? throw new InvalidOperationException("JWT SecretKey not configured");
			_jwtIssuer = _configuration["Jwt:Issuer"] ?? "PortfolioAPI";
			_jwtAudience = _configuration["Jwt:Audience"] ?? "PortfolioClients";
			_jwtExpirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60");
		}

		/// <summary>
		/// Registers a new user in the system
		/// </summary>
		/// <param name="request">Registration request containing user details</param>
		/// <returns>Login response with JWT token</returns>
		/// <exception cref="InvalidOperationException">Thrown when username or email already exists</exception>
		public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto request)
		{
			// Validate request
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			// Check if username already exists
			var existingUser = await _unitOfWork.Users
				.FirstOrDefaultAsync(u => u.Username == request.Username);

			if (existingUser != null)
				throw new InvalidOperationException("Username already exists");

			// Check if email already exists
			existingUser = await _unitOfWork.Users
				.FirstOrDefaultAsync(u => u.Email == request.Email);

			if (existingUser != null)
				throw new InvalidOperationException("Email already exists");

			// Create new user
			var user = new User
			{
				Username = request.Username,
				Email = request.Email,
				PasswordHash = PasswordHasher.HashPassword(request.Password),
				FirstName = request.FirstName,
				LastName = request.LastName,
				IsActive = true,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			// Save user to database
			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.SaveChangesAsync();

			// Generate JWT token for the new user
			return GenerateLoginResponse(user);
		}

		/// <summary>
		/// Authenticates a user and generates a JWT token
		/// </summary>
		/// <param name="request">Login request containing credentials</param>
		/// <returns>Login response with JWT token</returns>
		/// <exception cref="UnauthorizedAccessException">Thrown when credentials are invalid</exception>
		public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
		{
			// Validate request
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			// Find user by username or email
			var user = await _unitOfWork.Users
				.FirstOrDefaultAsync(u =>
					u.Username == request.Username ||
					u.Email == request.Username);

			// Validate user exists and is active
			if (user == null || !user.IsActive)
				throw new UnauthorizedAccessException("Invalid username or password");

			// Verify password using PasswordHasher
			if (!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
				throw new UnauthorizedAccessException("Invalid username or password");

			// Update last login timestamp
			user.UpdatedAt = DateTime.UtcNow;
			await _unitOfWork.Users.UpdateAsync(user);
			await _unitOfWork.SaveChangesAsync();

			// Generate and return JWT token
			return GenerateLoginResponse(user);
		}

		/// <summary>
		/// Validates a JWT token
		/// </summary>
		/// <param name="token">JWT token to validate</param>
		/// <returns>True if token is valid, false otherwise</returns>
		public Task<bool> ValidateTokenAsync(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return Task.FromResult(false);

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = _jwtIssuer,
					ValidateAudience = true,
					ValidAudience = _jwtAudience,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				return Task.FromResult(true);
			}
			catch
			{
				return Task.FromResult(false);
			}
		}

		/// <summary>
		/// Extracts user ID from a JWT token
		/// </summary>
		/// <param name="token">JWT token</param>
		/// <returns>User ID if valid, null otherwise</returns>
		public Task<int?> GetUserIdFromTokenAsync(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return Task.FromResult<int?>(null);

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var jwtToken = tokenHandler.ReadJwtToken(token);

				var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

				if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
				{
					return Task.FromResult<int?>(userId);
				}

				return Task.FromResult<int?>(null);
			}
			catch
			{
				return Task.FromResult<int?>(null);
			}
		}

		#region Private Helper Methods

		/// <summary>
		/// Generates a JWT token and login response for a user
		/// </summary>
		/// <param name="user">User entity</param>
		/// <returns>Login response with token and user details</returns>
		private LoginResponseDto GenerateLoginResponse(User user)
		{
			var token = GenerateJwtToken(user);
			var expirationDate = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes);

			return new LoginResponseDto
			{
				Token = token,
				ExpiresAt = expirationDate,
				UserId = user.UserId,
				Username = user.Username,
				Email = user.Email
			};
		}

		/// <summary>
		/// Generates a JWT token for the specified user
		/// </summary>
		/// <param name="user">User entity</param>
		/// <returns>JWT token string</returns>
		private string GenerateJwtToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

			// Create claims for the token
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
			};

			// Create token descriptor
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes),
				Issuer = _jwtIssuer,
				Audience = _jwtAudience,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			// Generate and return token
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		#endregion
	}
}