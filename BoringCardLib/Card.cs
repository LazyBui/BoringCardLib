﻿using System;
using System.Linq;

namespace BoringCardLib {
	public sealed class Card : IEquatable<Card> {
		private static readonly CardSet sStandardDeck =
			CardSet.MakeStandardDeck();

		public Suit Suit { get; private set; }
		public Rank Rank { get; private set; }
		public int? Value { get; private set; }

		public bool HasValue { get { return Value.HasValue; } }

		public bool IsPipCard { get { return Rank.IsPipRank(); } }
		public bool IsFaceCard { get { return Rank.IsFaceRank(); } }
		public bool IsBlackSuit { get { return Suit.IsBlackSuit(); } }
		public bool IsRedSuit { get { return Suit.IsRedSuit(); } }
		public bool IsHeart { get { return Suit == Suit.Hearts; } }
		public bool IsSpade { get { return Suit == Suit.Spades; } }
		public bool IsClub { get { return Suit == Suit.Clubs; } }
		public bool IsDiamond { get { return Suit == Suit.Diamonds; } }
		public bool IsJoker { get { return Suit == Suit.Joker; } }

		public static Card Joker { get { return new Card(Suit.Joker, Rank.Joker); } }

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

		public Card Duplicate() {
			return new Card(Suit, Rank, pointValue: Value);
		}

		public static Card GetRandom() {
			return GetRandom(new DefaultRandomSource());
		}

		public static Card GetRandom(IRandomSource sampler) {
			sampler.ThrowIfNull(nameof(sampler));
			return sStandardDeck.GetRandom(sampler);
		}

		public override int GetHashCode() {
			return new Hasher().
				Combine(Suit).
				Combine(Rank).
				Combine(Value).
				Hash();
		}

		public override string ToString() {
			if (IsJoker) return "Joker";
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