using System;
using System.Collections.Generic;

namespace BoringCardLib {
	/// <summary>
	/// Represents a default comparer for the <see cref="Card" /> class.
	/// </summary>
	public sealed class DefaultCardComparer : IComparer<Card>, IEqualityComparer<Card> {
		public static readonly DefaultCardComparer Instance = new DefaultCardComparer();

		/// <summary>
		/// Compares two <see cref="Card" /> instances and returns a value indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>-1 if <paramref name="x"/> is less than <paramref name="y" />, 0 if they are equal, and 1 if <paramref name="x"/> is greater than <paramref name="y" />.</returns>
		public int Compare(Card x, Card y) {
			if (object.ReferenceEquals(x, y)) return 0;
			if (object.ReferenceEquals(x, null)) return -1;
			if (object.ReferenceEquals(y, null)) return 1;
			return GetCardIndex(x).CompareTo(GetCardIndex(y));
		}

		/// <summary>
		/// Determines whether two <see cref="Card" /> instances are equal.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		public bool Equals(Card x, Card y) {
			if (object.ReferenceEquals(x, y)) return true;
			if (object.ReferenceEquals(x, null)) return false;
			if (object.ReferenceEquals(y, null)) return false;
			return x.Equals(y);
		}

		/// <summary>
		/// Serves as a hash function for hashing algorithms and data structures, such as a hash table.
		/// </summary>
		/// <param name="obj">The object for which to get a hash code.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="obj"/> is null.</exception>
		public int GetHashCode(Card obj) {
			obj.ThrowIfNull(nameof(obj));
			return obj.GetHashCode();
		}

		private int GetCardIndex(Card value) {
			return GetSuitValue(value.Suit) * 100 + GetRankValue(value.Rank);
		}

		private int GetSuitValue(Suit suit) {
			switch (suit) {
				case Suit.Joker: return 0;
				case Suit.Spades: return 4;
				case Suit.Hearts: return 3;
				case Suit.Clubs: return 2;
				case Suit.Diamonds: return 1;
			}
			throw new NotImplementedException();
		}

		private int GetRankValue(Rank rank) {
			return (int)rank;
		}
	}
}