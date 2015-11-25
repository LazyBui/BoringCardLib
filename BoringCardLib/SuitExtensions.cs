using System;

namespace BoringCardLib {
	public static partial class SuitExtensions {
		public static bool IsBlackSuit(this Suit @this) {
			return @this == Suit.Clubs || @this == Suit.Spades;
		}

		public static bool IsRedSuit(this Suit @this) {
			return @this == Suit.Diamonds || @this == Suit.Hearts;
		}
	}
}