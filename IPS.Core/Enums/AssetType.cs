
// =============================================
// IPS.Core/Enums/AssetType.cs
// Description: Enumeration for asset types
// =============================================

namespace IPS.Core.Enums
{
	/// <summary>
	/// Represents the type of tradable asset
	/// </summary>
	public enum AssetType
	{
		/// <summary>
		/// Company stock
		/// </summary>
		Stock = 1,

		/// <summary>
		/// Cryptocurrency
		/// </summary>
		Crypto = 2,

		/// <summary>
		/// Exchange Traded Fund
		/// </summary>
		ETF = 3,

		/// <summary>
		/// Bond or fixed income security
		/// </summary>
		Bond = 4,

		/// <summary>
		/// Commodity (gold, oil, etc.)
		/// </summary>
		Commodity = 5,

		/// <summary>
		/// Market index
		/// </summary>
		Index = 6
	}
}