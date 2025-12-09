// =============================================
// IPS.Core/Entities/AuditLog.cs
// Description: Tracks all system changes
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Audit log for tracking all system changes and user actions
	/// </summary>
	[Table("AuditLog")]
	public class AuditLog
	{
		/// <summary>
		/// Unique identifier for the audit entry
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AuditId { get; set; }

		/// <summary>
		/// ID of the user who performed the action (null for system actions)
		/// </summary>
		public int? UserId { get; set; }

		/// <summary>
		/// Type of action performed (Create, Update, Delete, etc.)
		/// </summary>
		[Required]
		[StringLength(50)]
		public string ActionType { get; set; } = string.Empty;

		/// <summary>
		/// Type of entity affected (User, Portfolio, Transaction, etc.)
		/// </summary>
		[Required]
		[StringLength(50)]
		public string EntityType { get; set; } = string.Empty;

		/// <summary>
		/// ID of the affected entity
		/// </summary>
		public int? EntityId { get; set; }

		/// <summary>
		/// Serialized old value (JSON)
		/// </summary>
		public string? OldValue { get; set; }

		/// <summary>
		/// Serialized new value (JSON)
		/// </summary>
		public string? NewValue { get; set; }

		/// <summary>
		/// IP address of the user
		/// </summary>
		[StringLength(50)]
		public string? IpAddress { get; set; }

		/// <summary>
		/// Date and time when the action occurred
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The user who performed the action
		/// </summary>
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }
	}
}