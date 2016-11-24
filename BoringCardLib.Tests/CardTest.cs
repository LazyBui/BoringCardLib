using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
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
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: null));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 0));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: -1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: int.MaxValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: int.MinValue));
		}

		[TestMethod]
		public void WithValue() {
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(0));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(-1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(int.MaxValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(int.MinValue));

			var card = new Card(Suit.Spades, Rank.Ace);
			var result = card.WithValue(0);
			
			Assert.False(card.HasValue);
			Assert.True(result.HasValue);

			Assert.Null(card.Value);
			Assert.True(result.Value == 0);
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

		[TestMethod]
		public void GetRandom() {
			var cards = new HashSet<Card>();
			for (int i = 0; i < 500; i++) {
				cards.Add(Card.GetRandom());
			}

			Assert.True(cards.Count == Constant.StandardDeckSize);
		}

		[TestMethod]
		public void Duplicate() {
			var card = new Card(Suit.Spades, Rank.Ace);
			var dupe = card.Duplicate();

			Assert.True(card == dupe);
			Assert.NotSame(card, dupe);
		}

		[TestMethod]
		public void Properties() {
			var aceOfSpades = new Card(Suit.Spades, Rank.Ace);
			Assert.True(aceOfSpades.IsSpade);
			Assert.False(aceOfSpades.IsHeart);
			Assert.False(aceOfSpades.IsDiamond);
			Assert.False(aceOfSpades.IsClub);
			Assert.False(aceOfSpades.IsJoker);

			Assert.True(aceOfSpades.IsBlackSuit);
			Assert.False(aceOfSpades.IsRedSuit);

			Assert.True(aceOfSpades.IsPipCard);
			Assert.False(aceOfSpades.IsFaceCard);

			var tenOfDiamonds = new Card(Suit.Diamonds, Rank.Ten);
			Assert.False(tenOfDiamonds.IsSpade);
			Assert.False(tenOfDiamonds.IsHeart);
			Assert.True(tenOfDiamonds.IsDiamond);
			Assert.False(tenOfDiamonds.IsClub);
			Assert.False(tenOfDiamonds.IsJoker);

			Assert.False(tenOfDiamonds.IsBlackSuit);
			Assert.True(tenOfDiamonds.IsRedSuit);

			Assert.True(tenOfDiamonds.IsPipCard);
			Assert.False(tenOfDiamonds.IsFaceCard);

			var twoOfClubs = new Card(Suit.Clubs, Rank.Two);
			Assert.False(twoOfClubs.IsSpade);
			Assert.False(twoOfClubs.IsHeart);
			Assert.False(twoOfClubs.IsDiamond);
			Assert.True(twoOfClubs.IsClub);
			Assert.False(twoOfClubs.IsJoker);

			Assert.True(twoOfClubs.IsBlackSuit);
			Assert.False(twoOfClubs.IsRedSuit);

			Assert.True(twoOfClubs.IsPipCard);
			Assert.False(twoOfClubs.IsFaceCard);

			var kingOfHearts = new Card(Suit.Hearts, Rank.King);
			Assert.False(kingOfHearts.IsSpade);
			Assert.True(kingOfHearts.IsHeart);
			Assert.False(kingOfHearts.IsDiamond);
			Assert.False(kingOfHearts.IsClub);
			Assert.False(kingOfHearts.IsJoker);

			Assert.False(kingOfHearts.IsBlackSuit);
			Assert.True(kingOfHearts.IsRedSuit);

			Assert.False(kingOfHearts.IsPipCard);
			Assert.True(kingOfHearts.IsFaceCard);

			var joker = Card.Joker;
			Assert.False(joker.IsSpade);
			Assert.False(joker.IsHeart);
			Assert.False(joker.IsDiamond);
			Assert.False(joker.IsClub);
			Assert.True(joker.IsJoker);

			Assert.False(joker.IsBlackSuit);
			Assert.False(joker.IsRedSuit);

			Assert.False(joker.IsPipCard);
			Assert.False(joker.IsFaceCard);
		}
	}
}
