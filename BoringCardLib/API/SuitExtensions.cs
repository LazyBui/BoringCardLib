using System;

namespace BoringCardLib {
	public static partial class SuitExtensions {
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is a black suit.
		/// </summary>
		/// <param name="this">The <see cref="Suit" /> instance.</param>
		/// <returns>true if <paramref name="this"/> is <see cref="Suit.Clubs" /> or <see cref="Suit.Spades" />. Otherwise, it returns false.</returns>
		public static bool IsBlackSuit(this Suit @this) {
			return @this == Suit.Clubs || @this == Suit.Spades;
		}

		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is a red suit.
		/// </summary>
		/// <param name="this">The <see cref="Suit" /> instance.</param>
		/// <returns>true if <paramref name="this"/> is <see cref="Suit.Diamonds" /> or <see cref="Suit.Hearts" />. Otherwise, it returns false.</returns>
		public static bool IsRedSuit(this Suit @this) {
			return @this == Suit.Diamonds || @this == Suit.Hearts;
		}
	}
}