using System;
using System.Collections.Generic;
using System.Linq;
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
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(values: new int[0], quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(value: 1, values: new[] { 1 }, quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(cards: new Card[0], quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(cards: new[] { null as Card }, quantity: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardRequest(card: Card.Joker, cards: new[] { Card.Joker }, quantity: 1));

			Assert.DoesNotThrow(() => new DiscardRequest(quantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades, cards: new[] { Card.Joker }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }, cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace, cards: new[] { Card.Joker }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }, cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1, cards: new[] { Card.Joker }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }, cards: new[] { Card.Joker }));

			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suit: Suit.Spades));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, suits: new[] { Suit.Spades }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, rank: Rank.Ace));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, ranks: new[] { Rank.Ace }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, value: 1));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, values: new[] { 1 }));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, nullValues: true));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, card: Card.Joker));
			Assert.DoesNotThrow(() => new DiscardRequest(direction: Direction.FromBottom, quantity: 1, cards: new[] { Card.Joker }));
		}
	}
}
