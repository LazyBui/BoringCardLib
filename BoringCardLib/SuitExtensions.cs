using System;

namespace BoringCardLib {
	public static partial class SuitExtensions {
		public static bool IsBlackSuit(this Suit pSuit) {
			return pSuit == Suit.Clubs || pSuit == Suit.Spades;
		}

		public static bool IsRedSuit(this Suit pSuit) {
			return pSuit == Suit.Diamonds || pSuit == Suit.Hearts;
		}
	}
}