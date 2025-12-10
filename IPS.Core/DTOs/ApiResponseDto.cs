// =============================================
// IPS.Core/DTOs/ApiResponseDto.cs
// Description: Generic API response wrapper
// =============================================

namespace IPS.Core.DTOs
{
	/// <summary>
	/// Generic API response wrapper for consistent response format
	/// </summary>
	/// <typeparam name="T">Type of data being returned</typeparam>
	public class ApiResponseDto<T>
	{
		/// <summary>
		/// Indicates if the request was successful
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Response message
		/// </summary>
		public string Message { get; set; } = string.Empty;

		/// <summary>
		/// Response data
		/// </summary>
		public T? Data { get; set; }

		/// <summary>
		/// List of errors (if any)
		/// </summary>
		public List<string>? Errors { get; set; }

		/// <summary>
		/// Timestamp of the response
		/// </summary>
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Creates a successful response
		/// </summary>
		public static ApiResponseDto<T> SuccessResponse(T data, string message = "Success")
		{
			return new ApiResponseDto<T>
			{
				Success = true,
				Message = message,
				Data = data
			};
		}

		/// <summary>
		/// Creates an error response
		/// </summary>
		public static ApiResponseDto<T> ErrorResponse(string message, List<string>? errors = null)
		{
			return new ApiResponseDto<T>
			{
				Success = false,
				Message = message,
				Errors = errors ?? new List<string>()
			};
		}
	}
}