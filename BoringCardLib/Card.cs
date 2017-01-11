using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents a playing card.
	/// </summary>
	public sealed class Card : IEquatable<Card> {
		private static readonly CardSet sStandardDeck =
			CardSet.MakeStandardDeck();

		/// <summary>
		/// The <see cref="Suit" /> of the card.
		/// </summary>
		public Suit Suit { get; private set; }
		/// <summary>
		/// The <see cref="Rank" /> of the card.
		/// </summary>
		public Rank Rank { get; private set; }
		/// <summary>
		/// The point value of the card.
		/// </summary>
		public int? Value { get; private set; }

		/// <summary>
		/// Indicates whether the card has a point value.
		/// </summary>
		public bool HasValue { get { return Value.HasValue; } }

		/// <summary>
		/// Indicates whether the <see cref="Rank" /> is a pip rank.
		/// </summary>
		public bool IsPipCard { get { return Rank.IsPipRank(); } }
		/// <summary>
		/// Indicates whether the <see cref="Rank" /> is a face rank.
		/// </summary>
		public bool IsFaceCard { get { return Rank.IsFaceRank(); } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is a black suit.
		/// </summary>
		public bool IsBlackSuit { get { return Suit.IsBlackSuit(); } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is a red suit.
		/// </summary>
		public bool IsRedSuit { get { return Suit.IsRedSuit(); } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is <see cref="Suit.Hearts" />.
		/// </summary>
		public bool IsHeart { get { return Suit == Suit.Hearts; } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is <see cref="Suit.Spades" />.
		/// </summary>
		public bool IsSpade { get { return Suit == Suit.Spades; } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is <see cref="Suit.Clubs" />.
		/// </summary>
		public bool IsClub { get { return Suit == Suit.Clubs; } }
		/// <summary>
		/// Indicates whether the <see cref="Suit" /> is <see cref="Suit.Diamonds" />.
		/// </summary>
		public bool IsDiamond { get { return Suit == Suit.Diamonds; } }
		/// <summary>
		/// Indicates whether the card is a joker.
		/// </summary>
		public bool IsJoker { get { return Suit == Suit.Joker; } }

		/// <summary>
		/// Represents a joker card.
		/// </summary>
		public static Card Joker { get; } = new Card(Suit.Joker, Rank.Joker);

		/// <summary>
		/// Initializes a new instance of the <see cref="Card" /> class.
		/// </summary>
		/// <param name="suit">The suit of the card.</param>
		/// <param name="rank">The rank of the card.</param>
		/// <param name="pointValue">The point value of the card if applicable.</param>
		/// <exception cref="ArgumentException"><paramref name="suit" /> is invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="suit" /> or <paramref name="rank"/> is joker, but not both.</exception>
		[DebuggerStepThrough]
		public Card(Suit suit, Rank rank, int? pointValue = null) {
			suit.ThrowIfInvalid(nameof(suit));
			rank.ThrowIfInvalid(nameof(rank));
			if ((suit == Suit.Joker) != (rank == Rank.Joker)) throw new ArgumentException("Jokers may only have the joker rank/suit");
			Suit = suit;
			Rank = rank;
			Value = pointValue;
		}

		/// <summary>
		/// Returns a new instance of the <see cref="Card" /> class with a new point value.
		/// </summary>
		/// <param name="newValue">The point value of the card.</param>
		/// <returns>A new <see cref="Card" /> instance.</returns>
		public Card WithValue(int newValue) {
			return new Card(Suit, Rank, newValue);
		}

		/// <summary>
		/// Returns a new instance of the <see cref="Card" /> class with no point value.
		/// </summary>
		/// <returns>A new <see cref="Card" /> instance.</returns>
		public Card WithoutValue() {
			return new Card(Suit, Rank);
		}

		/// <summary>
		/// Returns a new instance of the <see cref="Card" /> class with the same properties as the current instance.
		/// </summary>
		/// <returns>A new <see cref="Card" /> instance.</returns>
		public Card Duplicate() {
			return new Card(Suit, Rank, pointValue: Value);
		}

		/// <summary>
		/// Duplicates the current instance <paramref name="duplicationCount" /> times.
		/// </summary>
		/// <param name="duplicationCount">The number of times to duplicate.</param>
		/// <returns>A sequence containing the specified number of duplicates.</returns>
		/// <exception cref="ArgumentException"><paramref name="duplicationCount" /> is 0 or less.</exception>
		public CardSet Generate(int duplicationCount) {
			duplicationCount.ThrowIfZeroOrLess(nameof(duplicationCount));
			var result = new List<Card>(duplicationCount);
			for (int i = 0; i < duplicationCount; i++) {
				result.Add(Duplicate());
			}
			return new CardSet(result);
		}

		/// <summary>
		/// Duplicates the current instance <paramref name="duplicationCount" /> times.
		/// </summary>
		/// <param name="duplicationCount">The number of times to duplicate.</param>
		/// <returns>A sequence containing the specified number of duplicates.</returns>
		/// <exception cref="ArgumentException"><paramref name="duplicationCount" /> is 0 or less.</exception>
		public ImmutableCardSet GenerateImmutable(int duplicationCount) {
			return Generate(duplicationCount).AsImmutable();
		}

		/// <summary>
		/// Indicates whether a card is superior to another based on the passed in rules.
		/// </summary>
		/// <param name="other">The card to compare against.</param>
		/// <param name="comparer">The comparer to use for comparison.</param>
		/// <returns>true if this card is superior to the other, false otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="other" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public bool IsSuperiorTo(Card other, IComparer<Card> comparer) {
			other.ThrowIfNull(nameof(other));
			comparer.ThrowIfNull(nameof(comparer));
			return comparer.Compare(this, other) > 0;
		}

		/// <summary>
		/// Indicates whether a card is superior to or equal to another based on the passed in rules.
		/// </summary>
		/// <param name="other">The card to compare against.</param>
		/// <param name="comparer">The comparer to use for comparison.</param>
		/// <returns>true if this card is superior to or equal to the other, false otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="other" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public bool IsSuperiorOrEqualTo(Card other, IComparer<Card> comparer) {
			other.ThrowIfNull(nameof(other));
			comparer.ThrowIfNull(nameof(comparer));
			return comparer.Compare(this, other) >= 0;
		}

		/// <summary>
		/// Indicates whether a card is inferior to another based on the passed in rules.
		/// </summary>
		/// <param name="other">The card to compare against.</param>
		/// <param name="comparer">The comparer to use for comparison.</param>
		/// <returns>true if this card is inferior to the other, false otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="other" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public bool IsInferiorTo(Card other, IComparer<Card> comparer) {
			other.ThrowIfNull(nameof(other));
			comparer.ThrowIfNull(nameof(comparer));
			return comparer.Compare(this, other) < 0;
		}

		/// <summary>
		/// Indicates whether a card is inferior to or equal to another based on the passed in rules.
		/// </summary>
		/// <param name="other">The card to compare against.</param>
		/// <param name="comparer">The comparer to use for comparison.</param>
		/// <returns>true if this card is inferior to or equal to the other, false otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="other" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public bool IsInferiorOrEqualTo(Card other, IComparer<Card> comparer) {
			other.ThrowIfNull(nameof(other));
			comparer.ThrowIfNull(nameof(comparer));
			return comparer.Compare(this, other) <= 0;
		}

		/// <summary>
		/// Returns a random card from a standard deck using a <see cref="DefaultRandomSource" /> instance.
		/// </summary>
		/// <returns>A <see cref="Card" /> instance.</returns>
		public static Card GetRandom() {
			return GetRandom(new DefaultRandomSource());
		}

		/// <summary>
		/// Returns a random card from a standard deck using the specified <see cref="IRandomSource" />.
		/// </summary>
		/// <param name="sampler">The source of randomness.</param>
		/// <returns>A <see cref="Card" /> instance.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="sampler" /> is null.</exception>
		public static Card GetRandom(IRandomSource sampler) {
			sampler.ThrowIfNull(nameof(sampler));
			return sStandardDeck.GetRandom(sampler);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() {
			return new Hasher().
				Combine(Suit).
				Combine(Rank).
				Combine(Value).
				Hash();
		}

		/// <summary>
		/// Converts the information this instance to its equivalent string representation.
		/// </summary>
		/// <returns>
		///		A string representing the current instance.
		///		If the current instance is a joker, "Joker" is returned.
		///		Otherwise, "<see cref="Rank" /> of <see cref="Suit" />" is returned.
		/// </returns>
		public override string ToString() {
			if (IsJoker) return "Joker";
			return $"{Rank} of {Suit}";
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified <see cref="object" /> value.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare.</param>
		/// <returns>true if the object is of type <see cref="Card" />, not null, and its <see cref="Rank" />, <see cref="Suit" />, and value match. Otheriwse, it returns false.</returns>
		public override bool Equals(object obj) {
			return Equals(obj as Card);
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified <see cref="Card" /> value.
		/// </summary>
		/// <param name="other">The <see cref="Card" /> to compare.</param>
		/// <returns>true if the object is not null and its <see cref="Rank" />, <see cref="Suit" />, and value match. Otheriwse, it returns false.</returns>
		public bool Equals(Card other) {
			if (other == null) return false;
			return new ValueEqualityTester().
				Equal(other.Suit, Suit).
				Equal(other.Rank, Rank).
				Equal(other.Value, Value).
				All();
		}

#pragma warning disable 1591
		public static bool operator ==(Card lhs, Card rhs) {
			return Operators.Equals(lhs, rhs);
		}

		public static bool operator !=(Card lhs, Card rhs) {
			return Operators.NotEquals(lhs, rhs);
		}
#pragma warning restore 1591
	}
}