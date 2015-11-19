using System;

namespace BoringCardLib {
	public sealed class Card : IEquatable<Card> {
		public Suit Suit { get; private set; }
		public Rank Rank { get; private set; }
		public int? Value { get; private set; }

		public Card(Suit pSuit, Rank pRank, int? pPointValue = null) {
			pSuit.ThrowIfInvalid(nameof(pSuit));
			pRank.ThrowIfInvalid(nameof(pRank));
			if ((pSuit == Suit.Joker) != (pRank == Rank.Joker)) throw new ArgumentException("Jokers may only have the joker rank/suit");

			Suit = pSuit;
			Rank = pRank;
			Value = pPointValue;
		}

		public Card WithValue(int pNewValue) {
			return new Card(Suit, Rank, pNewValue);
		}

		public override int GetHashCode() {
			return new Hasher().
				Combine(Suit).
				Combine(Rank).
				Combine(Value).
				Hash();
		}

		public override string ToString() {
			return $"{Rank} of {Suit}";
		}

		public override bool Equals(object obj) {
			return Equals(obj as Card);
		}

		public bool Equals(Card other) {
			if (other == null) return false;
			return new ValueEqualityTester().
				Equal(other.Suit, Suit).
				Equal(other.Rank, Rank).
				Equal(other.Value, Value).
				All();
		}

		public static bool operator ==(Card lhs, Card rhs) {
			return Operators.Equals(lhs, rhs);
		}

		public static bool operator !=(Card lhs, Card rhs) {
			return Operators.NotEquals(lhs, rhs);
		}
	}
}