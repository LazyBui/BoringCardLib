using System;

namespace BoringCardLib {
	public enum Direction {
		/// <summary>
		/// Indicates that an operation should be performed from the first card to the last card.
		/// </summary>
		FirstToLast,
		/// <summary>
		/// Indicates that an operation should be performed from the last card to the first card.
		/// </summary>
		LastToFirst,
	}
}