// =============================================
// IPS.Core/Enums/AlertType.cs
// Description: Enumeration for price alert types
// =============================================

namespace IPS.Core.Enums
{
	/// <summary>
	/// Represents the type of price alert
	/// </summary>
	public enum AlertType
	{
		/// <summary>
		/// Alert when price goes above target
		/// </summary>
		Above = 1,

		/// <summary>
		/// Alert when price goes below target
		/// </summary>
		Below = 2
	}
}