﻿using System;

namespace BoringCardLib {
	public sealed class Card : IEquatable<Card> {
		public Suit Suit { get; private set; }
		public Rank Rank { get; private set; }
		public int? Value { get; private set; }

		public Card(Suit suit, Rank rank, int? pointValue = null) {
			suit.ThrowIfInvalid(nameof(suit));
			rank.ThrowIfInvalid(nameof(rank));
			if ((suit == Suit.Joker) != (rank == Rank.Joker)) throw new ArgumentException("Jokers may only have the joker rank/suit");

			Suit = suit;
			Rank = rank;
			Value = pointValue;
		}

		public Card WithValue(int newValue) {
			return new Card(Suit, Rank, newValue);
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