using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class CardTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentException>(() => new Card((Suit)(-1), Rank.Two));
			Assert.ThrowsExact<ArgumentException>(() => new Card(Suit.Diamonds, (Rank)(-1)));
			Assert.ThrowsExact<ArgumentException>(() => new Card(Suit.Diamonds, Rank.Joker));
			Assert.ThrowsExact<ArgumentException>(() => new Card(Suit.Joker, Rank.Two));
			Assert.DoesNotThrow(() => new Card(Suit.Joker, Rank.Joker));
			Assert.DoesNotThrow(() => new Card(Suit.Diamonds, Rank.Two));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: null));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: 0));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: -1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: 1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: int.MaxValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pPointValue: int.MinValue));
		}

		[TestMethod]
		public void WithValue() {
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(0));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(-1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(int.MaxValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(int.MinValue));
		}

		[TestMethod]
		public void Operators() {
			var ace = new Card(Suit.Spades, Rank.Ace);
			var aceAlias = ace;
			Assert.True(ace == aceAlias);
			Assert.True(ace == new Card(Suit.Spades, Rank.Ace));
			Assert.False(ace == new Card(Suit.Spades, Rank.Two));
			Assert.False(ace == new Card(Suit.Clubs, Rank.Ace));

			Assert.False(ace != aceAlias);
			Assert.False(ace != new Card(Suit.Spades, Rank.Ace));
			Assert.True(ace != new Card(Suit.Spades, Rank.Two));
			Assert.True(ace != new Card(Suit.Clubs, Rank.Ace));

			Assert.False(ace == new Card(Suit.Spades, Rank.Ace).WithValue(2));
			Assert.False(ace == new Card(Suit.Spades, Rank.Ace).WithValue(1));
			Assert.False(ace == new Card(Suit.Spades, Rank.Ace).WithValue(0));
			Assert.False(ace == new Card(Suit.Spades, Rank.Ace).WithValue((int)Rank.Ace));

			Assert.True(ace != new Card(Suit.Spades, Rank.Ace).WithValue(2));
			Assert.True(ace != new Card(Suit.Spades, Rank.Ace).WithValue(1));
			Assert.True(ace != new Card(Suit.Spades, Rank.Ace).WithValue(0));
			Assert.True(ace != new Card(Suit.Spades, Rank.Ace).WithValue((int)Rank.Ace));
		}
	}
}
