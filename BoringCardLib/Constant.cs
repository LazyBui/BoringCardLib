using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents various card-related constants.
	/// </summary>
	public static class Constant {
		/// <summary>
		/// Represents how many playing cards, without jokers, are in a typical deck.
		/// </summary>
		public const int StandardDeckSize = 52;
		/// <summary>
		/// Represents how many ranks are in a suit.
		/// </summary>
		public const int StandardSuitSize = 13;
		/// <summary>
		/// Represents how many jokers are in a typical deck.
		/// </summary>
		public const int StandardJokerCount = 2;
		/// <summary>
		/// Represents how many suits are in a typical deck.
		/// </summary>
		public const int StandardSuitCount = 4;
		/// <summary>
		/// Represents how many playing cards, including jokers, are in a typical deck.
		/// </summary>
		public const int StandardDeckSizeWithJokers = StandardDeckSize + StandardJokerCount;
	}
}