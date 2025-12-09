// =============================================
// IPS.Core/Entities/PriceAlert.cs
// Description: Represents a price alert
// =============================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.Core.Entities
{
	/// <summary>
	/// Represents a user-defined price alert for an asset
	/// </summary>
	[Table("PriceAlerts")]
	public class PriceAlert
	{
		/// <summary>
		/// Unique identifier for the alert
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AlertId { get; set; }

		/// <summary>
		/// ID of the user who created the alert
		/// </summary>
		[Required]
		public int UserId { get; set; }

		/// <summary>
		/// ID of the asset to monitor
		/// </summary>
		[Required]
		public int AssetId { get; set; }

		/// <summary>
		/// Target price that triggers the alert
		/// </summary>
		[Required]
		[Column(TypeName = "decimal(18, 4)")]
		public decimal TargetPrice { get; set; }

		/// <summary>
		/// Type of alert (Above or Below)
		/// </summary>
		[Required]
		[StringLength(10)]
		public string AlertType { get; set; } = string.Empty;

		/// <summary>
		/// Indicates if the alert is currently active
		/// </summary>
		public bool IsActive { get; set; } = true;

		/// <summary>
		/// Indicates if the alert has been triggered
		/// </summary>
		public bool IsTriggered { get; set; } = false;

		/// <summary>
		/// Date and time when the alert was triggered
		/// </summary>
		public DateTime? TriggeredAt { get; set; }

		/// <summary>
		/// Date and time when the alert was created
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Date and time when the alert was last updated
		/// </summary>
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		// Navigation properties
		/// <summary>
		/// The user who created this alert
		/// </summary>
		[ForeignKey("UserId")]
		public virtual User? User { get; set; }

		/// <summary>
		/// The asset being monitored
		/// </summary>
		[ForeignKey("AssetId")]
		public virtual Asset? Asset { get; set; }
	}
}