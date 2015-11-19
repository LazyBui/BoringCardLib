using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class DiscardRequestTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest());
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pDirection: (Direction)(-1), pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pQuantity: -1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pQuantity: 0));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pSuit: (Suit)(-1), pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pSuits: new Suit[0], pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pSuits: new[] { (Suit)(-1) }, pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pSuit: Suit.Spades, pSuits: new[] { Suit.Spades }, pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pRank: (Rank)(-1), pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pRanks: new Rank[0], pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pRanks: new[] { (Rank)(-1) }, pQuantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(pRank: Rank.Ace, pRanks: new[] { Rank.Eight }, pQuantity: 1));

			Assert.DoesNotThrow(() => new DiscardRequest(pQuantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(pSuit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(pSuits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(pRank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(pRanks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pSuit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pSuits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pRank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pRanks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1, pSuit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1, pSuits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1, pRank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1, pRanks: new[] { Rank.Ace }));
		}
	}
}
