using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class DiscardRequestTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest());
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(direction: (Direction)(-1), quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(quantity: -1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(quantity: 0));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(suit: (Suit)(-1), quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(suits: new Suit[0], quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(suits: new[] { (Suit)(-1) }, quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(suit: Suit.Spades, suits: new[] { Suit.Spades }, quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(rank: (Rank)(-1), quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(ranks: new Rank[0], quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(ranks: new[] { (Rank)(-1) }, quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(rank: Rank.Ace, ranks: new[] { Rank.Eight }, quantity: 1));

			Assert.DoesNotThrow(() => new DiscardRequest(quantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }));
		}
	}
}
