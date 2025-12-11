// =============================================
// IPS.Services/Helpers/PasswordHasher.cs
// Description: Standalone password hashing utility
// =============================================

using System;
using System.Security.Cryptography;

namespace IPS.Services.Helpers
{
	/// <summary>
	/// Utility class for secure password hashing and verification
	/// Uses PBKDF2 with SHA256 for cryptographic security
	/// </summary>
	public static class PasswordHasher
	{
		private const int SaltSize = 16; // 128 bits
		private const int HashSize = 32; // 256 bits
		private const int Iterations = 10000;

		/// <summary>
		/// Hashes a password using PBKDF2 with SHA256
		/// </summary>
		/// <param name="password">Plain text password</param>
		/// <returns>Base64 encoded hash with embedded salt</returns>
		public static string HashPassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Password cannot be empty", nameof(password));

			// Generate random salt
			byte[] salt = new byte[SaltSize];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			// Generate hash
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
				password,
				salt,
				Iterations,
				HashAlgorithmName.SHA256,
				HashSize
			);

			// Combine salt and hash
			byte[] hashBytes = new byte[SaltSize + HashSize];
			Array.Copy(salt, 0, hashBytes, 0, SaltSize);
			Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

			return Convert.ToBase64String(hashBytes);
		}

		/// <summary>
		/// Verifies a password against a stored hash
		/// </summary>
		/// <param name="password">Plain text password to verify</param>
		/// <param name="hashedPassword">Stored hashed password</param>
		/// <returns>True if password is correct, false otherwise</returns>
		public static bool VerifyPassword(string password, string hashedPassword)
		{
			if (string.IsNullOrWhiteSpace(password))
				return false;

			if (string.IsNullOrWhiteSpace(hashedPassword))
				return false;

			try
			{
				// Extract bytes from base64 string
				byte[] hashBytes = Convert.FromBase64String(hashedPassword);

				// Verify expected length
				if (hashBytes.Length != SaltSize + HashSize)
					return false;

				// Extract salt
				byte[] salt = new byte[SaltSize];
				Array.Copy(hashBytes, 0, salt, 0, SaltSize);

				// Hash the input password with extracted salt
				byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
					password,
					salt,
					Iterations,
					HashAlgorithmName.SHA256,
					HashSize
				);

				// Compare hashes
				for (int i = 0; i < HashSize; i++)
				{
					if (hashBytes[i + SaltSize] != hash[i])
						return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Generates a random secure password
		/// </summary>
		/// <param name="length">Length of password (minimum 8)</param>
		/// <returns>Random password string</returns>
		public static string GenerateRandomPassword(int length = 12)
		{
			if (length < 8)
				throw new ArgumentException("Password length must be at least 8 characters");

			const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
			var random = new byte[length];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(random);
			}

			var password = new char[length];
			for (int i = 0; i < length; i++)
			{
				password[i] = validChars[random[i] % validChars.Length];
			}

			return new string(password);
		}
	}
}