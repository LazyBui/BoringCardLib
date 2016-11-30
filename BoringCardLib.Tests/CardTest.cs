using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class CardTest {
		private class CardComparer : IComparer<Card> {
			public int Compare(Card x, Card y) {
				if (object.ReferenceEquals(x, y)) return 0;
				if (object.ReferenceEquals(x, null)) return -1;
				if (object.ReferenceEquals(y, null)) return 1;
				if (x.IsBlackSuit != y.IsBlackSuit) return x.IsBlackSuit ? 1 : -1;

				if (x.Suit != y.Suit) {
					return x.IsBlackSuit ?
						(x.Suit == Suit.Spades ? 1 : -1) :
						(x.Suit == Suit.Hearts ? 1 : -1);
				}

				if (x.Rank == y.Rank) return 0;

				if ((int)x.Rank < (int)y.Rank) return -1;
				return 1;
			}
		}

		private static readonly CardComparer Instance = new CardComparer();

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
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithValue(int.MinValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(0));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(-1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(1));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(int.MaxValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(int.MinValue));
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithValue(int.MinValue));

			var card = new Card(Suit.Spades, Rank.Ace);
			var result = card.WithValue(0);

			Assert.False(card.HasValue);
			Assert.True(result.HasValue);
			Assert.Equal(card.Value, null as int?);
			Assert.Equal(result.Value, 0 as int?);

			card = new Card(Suit.Spades, Rank.Ace, pointValue: 3);
			result = card.WithValue(0);

			Assert.True(card.HasValue);
			Assert.True(result.HasValue);
			Assert.Equal(card.Value, 3 as int?);
			Assert.Equal(result.Value, 0 as int?);
		}

		[TestMethod]
		public void WithoutValue() {
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace).WithoutValue());
			Assert.DoesNotThrow(() => new Card(Suit.Spades, Rank.Ace, pointValue: 2).WithoutValue());

			var card = new Card(Suit.Spades, Rank.Ace, pointValue: 3);
			var result = card.WithoutValue();
			
			Assert.True(card.HasValue);
			Assert.False(result.HasValue);
			Assert.Equal(card.Value, 3 as int?);
			Assert.Equal(result.Value, null as int?);
			
			card = new Card(Suit.Spades, Rank.Ace);
			result = card.WithoutValue();
			
			Assert.False(card.HasValue);
			Assert.False(result.HasValue);
			Assert.Equal(card.Value, null as int?);
			Assert.Equal(result.Value, null as int?);
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

		[TestMethod]
		public void IsSuperiorTo() {
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsSuperiorTo(null, Instance));
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsSuperiorTo(Card.Joker, null));

			var jackSpades = new Card(Suit.Spades, Rank.Jack);
			var tenSpades = new Card(Suit.Spades, Rank.Ten);
			var nineSpades = new Card(Suit.Spades, Rank.Nine);
			var jackHearts = new Card(Suit.Hearts, Rank.Jack);
			var tenHearts = new Card(Suit.Hearts, Rank.Ten);
			var nineHearts = new Card(Suit.Hearts, Rank.Nine);
			var jackClubs = new Card(Suit.Clubs, Rank.Jack);
			var tenClubs = new Card(Suit.Clubs, Rank.Ten);
			var nineClubs = new Card(Suit.Clubs, Rank.Nine);
			var jackDiamonds = new Card(Suit.Diamonds, Rank.Jack);
			var tenDiamonds = new Card(Suit.Diamonds, Rank.Ten);
			var nineDiamonds = new Card(Suit.Diamonds, Rank.Nine);

			Assert.False(jackSpades.IsSuperiorTo(jackSpades, Instance));
			Assert.True(jackSpades.IsSuperiorTo(tenSpades, Instance));
			Assert.True(jackSpades.IsSuperiorTo(nineSpades, Instance));
			Assert.True(jackSpades.IsSuperiorTo(jackHearts, Instance));
			Assert.True(jackSpades.IsSuperiorTo(tenHearts, Instance));
			Assert.True(jackSpades.IsSuperiorTo(nineHearts, Instance));
			Assert.True(jackSpades.IsSuperiorTo(jackClubs, Instance));
			Assert.True(jackSpades.IsSuperiorTo(tenClubs, Instance));
			Assert.True(jackSpades.IsSuperiorTo(nineClubs, Instance));
			Assert.True(jackSpades.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(jackSpades.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(jackSpades.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(tenSpades.IsSuperiorTo(jackSpades, Instance));
			Assert.False(tenSpades.IsSuperiorTo(tenSpades, Instance));
			Assert.True(tenSpades.IsSuperiorTo(nineSpades, Instance));
			Assert.True(tenSpades.IsSuperiorTo(jackHearts, Instance));
			Assert.True(tenSpades.IsSuperiorTo(tenHearts, Instance));
			Assert.True(tenSpades.IsSuperiorTo(nineHearts, Instance));
			Assert.True(tenSpades.IsSuperiorTo(jackClubs, Instance));
			Assert.True(tenSpades.IsSuperiorTo(tenClubs, Instance));
			Assert.True(tenSpades.IsSuperiorTo(nineClubs, Instance));
			Assert.True(tenSpades.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(tenSpades.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(tenSpades.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(nineSpades.IsSuperiorTo(jackSpades, Instance));
			Assert.False(nineSpades.IsSuperiorTo(tenSpades, Instance));
			Assert.False(nineSpades.IsSuperiorTo(nineSpades, Instance));
			Assert.True(nineSpades.IsSuperiorTo(jackHearts, Instance));
			Assert.True(nineSpades.IsSuperiorTo(tenHearts, Instance));
			Assert.True(nineSpades.IsSuperiorTo(nineHearts, Instance));
			Assert.True(nineSpades.IsSuperiorTo(jackClubs, Instance));
			Assert.True(nineSpades.IsSuperiorTo(tenClubs, Instance));
			Assert.True(nineSpades.IsSuperiorTo(nineClubs, Instance));
			Assert.True(nineSpades.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(nineSpades.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(nineSpades.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(jackHearts.IsSuperiorTo(jackSpades, Instance));
			Assert.False(jackHearts.IsSuperiorTo(tenSpades, Instance));
			Assert.False(jackHearts.IsSuperiorTo(nineSpades, Instance));
			Assert.False(jackHearts.IsSuperiorTo(jackHearts, Instance));
			Assert.True(jackHearts.IsSuperiorTo(tenHearts, Instance));
			Assert.True(jackHearts.IsSuperiorTo(nineHearts, Instance));
			Assert.False(jackHearts.IsSuperiorTo(jackClubs, Instance));
			Assert.False(jackHearts.IsSuperiorTo(tenClubs, Instance));
			Assert.False(jackHearts.IsSuperiorTo(nineClubs, Instance));
			Assert.True(jackHearts.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(jackHearts.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(jackHearts.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(tenHearts.IsSuperiorTo(jackSpades, Instance));
			Assert.False(tenHearts.IsSuperiorTo(tenSpades, Instance));
			Assert.False(tenHearts.IsSuperiorTo(nineSpades, Instance));
			Assert.False(tenHearts.IsSuperiorTo(jackHearts, Instance));
			Assert.False(tenHearts.IsSuperiorTo(tenHearts, Instance));
			Assert.True(tenHearts.IsSuperiorTo(nineHearts, Instance));
			Assert.False(tenHearts.IsSuperiorTo(jackClubs, Instance));
			Assert.False(tenHearts.IsSuperiorTo(tenClubs, Instance));
			Assert.False(tenHearts.IsSuperiorTo(nineClubs, Instance));
			Assert.True(tenHearts.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(tenHearts.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(tenHearts.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(nineHearts.IsSuperiorTo(jackSpades, Instance));
			Assert.False(nineHearts.IsSuperiorTo(tenSpades, Instance));
			Assert.False(nineHearts.IsSuperiorTo(nineSpades, Instance));
			Assert.False(nineHearts.IsSuperiorTo(jackHearts, Instance));
			Assert.False(nineHearts.IsSuperiorTo(tenHearts, Instance));
			Assert.False(nineHearts.IsSuperiorTo(nineHearts, Instance));
			Assert.False(nineHearts.IsSuperiorTo(jackClubs, Instance));
			Assert.False(nineHearts.IsSuperiorTo(tenClubs, Instance));
			Assert.False(nineHearts.IsSuperiorTo(nineClubs, Instance));
			Assert.True(nineHearts.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(nineHearts.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(nineHearts.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(jackClubs.IsSuperiorTo(jackSpades, Instance));
			Assert.False(jackClubs.IsSuperiorTo(tenSpades, Instance));
			Assert.False(jackClubs.IsSuperiorTo(nineSpades, Instance));
			Assert.True(jackClubs.IsSuperiorTo(jackHearts, Instance));
			Assert.True(jackClubs.IsSuperiorTo(tenHearts, Instance));
			Assert.True(jackClubs.IsSuperiorTo(nineHearts, Instance));
			Assert.False(jackClubs.IsSuperiorTo(jackClubs, Instance));
			Assert.True(jackClubs.IsSuperiorTo(tenClubs, Instance));
			Assert.True(jackClubs.IsSuperiorTo(nineClubs, Instance));
			Assert.True(jackClubs.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(jackClubs.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(jackClubs.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(tenClubs.IsSuperiorTo(jackSpades, Instance));
			Assert.False(tenClubs.IsSuperiorTo(tenSpades, Instance));
			Assert.False(tenClubs.IsSuperiorTo(nineSpades, Instance));
			Assert.True(tenClubs.IsSuperiorTo(jackHearts, Instance));
			Assert.True(tenClubs.IsSuperiorTo(tenHearts, Instance));
			Assert.True(tenClubs.IsSuperiorTo(nineHearts, Instance));
			Assert.False(tenClubs.IsSuperiorTo(jackClubs, Instance));
			Assert.False(tenClubs.IsSuperiorTo(tenClubs, Instance));
			Assert.True(tenClubs.IsSuperiorTo(nineClubs, Instance));
			Assert.True(tenClubs.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(tenClubs.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(tenClubs.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(nineClubs.IsSuperiorTo(jackSpades, Instance));
			Assert.False(nineClubs.IsSuperiorTo(tenSpades, Instance));
			Assert.False(nineClubs.IsSuperiorTo(nineSpades, Instance));
			Assert.True(nineClubs.IsSuperiorTo(jackHearts, Instance));
			Assert.True(nineClubs.IsSuperiorTo(tenHearts, Instance));
			Assert.True(nineClubs.IsSuperiorTo(nineHearts, Instance));
			Assert.False(nineClubs.IsSuperiorTo(jackClubs, Instance));
			Assert.False(nineClubs.IsSuperiorTo(tenClubs, Instance));
			Assert.False(nineClubs.IsSuperiorTo(nineClubs, Instance));
			Assert.True(nineClubs.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(nineClubs.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(nineClubs.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(jackDiamonds.IsSuperiorTo(jackSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(tenSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(nineSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(jackHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(tenHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(nineHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(jackClubs, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(tenClubs, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(nineClubs, Instance));
			Assert.False(jackDiamonds.IsSuperiorTo(jackDiamonds, Instance));
			Assert.True(jackDiamonds.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(jackDiamonds.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(tenDiamonds.IsSuperiorTo(jackSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(tenSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(nineSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(jackHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(tenHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(nineHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(jackClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(tenClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(nineClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(jackDiamonds, Instance));
			Assert.False(tenDiamonds.IsSuperiorTo(tenDiamonds, Instance));
			Assert.True(tenDiamonds.IsSuperiorTo(nineDiamonds, Instance));

			Assert.False(nineDiamonds.IsSuperiorTo(jackSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(tenSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(nineSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(jackHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(tenHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(nineHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(jackClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(tenClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(nineClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(jackDiamonds, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(tenDiamonds, Instance));
			Assert.False(nineDiamonds.IsSuperiorTo(nineDiamonds, Instance));
		}

		[TestMethod]
		public void IsSuperiorOrEqualTo() {
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsSuperiorOrEqualTo(null, Instance));
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsSuperiorOrEqualTo(Card.Joker, null));

			var jackSpades = new Card(Suit.Spades, Rank.Jack);
			var tenSpades = new Card(Suit.Spades, Rank.Ten);
			var nineSpades = new Card(Suit.Spades, Rank.Nine);
			var jackHearts = new Card(Suit.Hearts, Rank.Jack);
			var tenHearts = new Card(Suit.Hearts, Rank.Ten);
			var nineHearts = new Card(Suit.Hearts, Rank.Nine);
			var jackClubs = new Card(Suit.Clubs, Rank.Jack);
			var tenClubs = new Card(Suit.Clubs, Rank.Ten);
			var nineClubs = new Card(Suit.Clubs, Rank.Nine);
			var jackDiamonds = new Card(Suit.Diamonds, Rank.Jack);
			var tenDiamonds = new Card(Suit.Diamonds, Rank.Ten);
			var nineDiamonds = new Card(Suit.Diamonds, Rank.Nine);

			Assert.True(jackSpades.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(jackSpades.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(tenSpades.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(tenSpades.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(nineSpades.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(nineSpades.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(nineSpades.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(jackHearts.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(jackHearts.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(jackHearts.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(jackHearts.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(jackHearts.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(jackHearts.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(jackHearts.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(tenHearts.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(tenHearts.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(tenHearts.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(tenHearts.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(tenHearts.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(tenHearts.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(tenHearts.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(nineHearts.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(nineHearts.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(nineHearts.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(nineHearts.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(nineHearts.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(nineHearts.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(jackClubs.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(jackClubs.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(jackClubs.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(jackClubs.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(tenClubs.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(tenClubs.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(tenClubs.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(tenClubs.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(tenClubs.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(nineClubs.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(nineClubs.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(nineClubs.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(nineClubs.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(nineClubs.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(nineClubs.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(jackDiamonds.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.True(jackDiamonds.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(jackDiamonds.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(jackDiamonds.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.False(tenDiamonds.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(tenDiamonds.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(tenDiamonds.IsSuperiorOrEqualTo(nineDiamonds, Instance));

			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(jackSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(tenSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(nineSpades, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(jackHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(tenHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(nineHearts, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(jackClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(tenClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(nineClubs, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(nineDiamonds.IsSuperiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(nineDiamonds.IsSuperiorOrEqualTo(nineDiamonds, Instance));
		}

		[TestMethod]
		public void IsInferiorTo() {
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsInferiorTo(null, Instance));
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsInferiorTo(Card.Joker, null));

			var jackSpades = new Card(Suit.Spades, Rank.Jack);
			var tenSpades = new Card(Suit.Spades, Rank.Ten);
			var nineSpades = new Card(Suit.Spades, Rank.Nine);
			var jackHearts = new Card(Suit.Hearts, Rank.Jack);
			var tenHearts = new Card(Suit.Hearts, Rank.Ten);
			var nineHearts = new Card(Suit.Hearts, Rank.Nine);
			var jackClubs = new Card(Suit.Clubs, Rank.Jack);
			var tenClubs = new Card(Suit.Clubs, Rank.Ten);
			var nineClubs = new Card(Suit.Clubs, Rank.Nine);
			var jackDiamonds = new Card(Suit.Diamonds, Rank.Jack);
			var tenDiamonds = new Card(Suit.Diamonds, Rank.Ten);
			var nineDiamonds = new Card(Suit.Diamonds, Rank.Nine);

			Assert.False(jackSpades.IsInferiorTo(jackSpades, Instance));
			Assert.False(jackSpades.IsInferiorTo(tenSpades, Instance));
			Assert.False(jackSpades.IsInferiorTo(nineSpades, Instance));
			Assert.False(jackSpades.IsInferiorTo(jackHearts, Instance));
			Assert.False(jackSpades.IsInferiorTo(tenHearts, Instance));
			Assert.False(jackSpades.IsInferiorTo(nineHearts, Instance));
			Assert.False(jackSpades.IsInferiorTo(jackClubs, Instance));
			Assert.False(jackSpades.IsInferiorTo(tenClubs, Instance));
			Assert.False(jackSpades.IsInferiorTo(nineClubs, Instance));
			Assert.False(jackSpades.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(jackSpades.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(jackSpades.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(tenSpades.IsInferiorTo(jackSpades, Instance));
			Assert.False(tenSpades.IsInferiorTo(tenSpades, Instance));
			Assert.False(tenSpades.IsInferiorTo(nineSpades, Instance));
			Assert.False(tenSpades.IsInferiorTo(jackHearts, Instance));
			Assert.False(tenSpades.IsInferiorTo(tenHearts, Instance));
			Assert.False(tenSpades.IsInferiorTo(nineHearts, Instance));
			Assert.False(tenSpades.IsInferiorTo(jackClubs, Instance));
			Assert.False(tenSpades.IsInferiorTo(tenClubs, Instance));
			Assert.False(tenSpades.IsInferiorTo(nineClubs, Instance));
			Assert.False(tenSpades.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(tenSpades.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(tenSpades.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(nineSpades.IsInferiorTo(jackSpades, Instance));
			Assert.True(nineSpades.IsInferiorTo(tenSpades, Instance));
			Assert.False(nineSpades.IsInferiorTo(nineSpades, Instance));
			Assert.False(nineSpades.IsInferiorTo(jackHearts, Instance));
			Assert.False(nineSpades.IsInferiorTo(tenHearts, Instance));
			Assert.False(nineSpades.IsInferiorTo(nineHearts, Instance));
			Assert.False(nineSpades.IsInferiorTo(jackClubs, Instance));
			Assert.False(nineSpades.IsInferiorTo(tenClubs, Instance));
			Assert.False(nineSpades.IsInferiorTo(nineClubs, Instance));
			Assert.False(nineSpades.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(nineSpades.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(nineSpades.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(jackHearts.IsInferiorTo(jackSpades, Instance));
			Assert.True(jackHearts.IsInferiorTo(tenSpades, Instance));
			Assert.True(jackHearts.IsInferiorTo(nineSpades, Instance));
			Assert.False(jackHearts.IsInferiorTo(jackHearts, Instance));
			Assert.False(jackHearts.IsInferiorTo(tenHearts, Instance));
			Assert.False(jackHearts.IsInferiorTo(nineHearts, Instance));
			Assert.True(jackHearts.IsInferiorTo(jackClubs, Instance));
			Assert.True(jackHearts.IsInferiorTo(tenClubs, Instance));
			Assert.True(jackHearts.IsInferiorTo(nineClubs, Instance));
			Assert.False(jackHearts.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(jackHearts.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(jackHearts.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(tenHearts.IsInferiorTo(jackSpades, Instance));
			Assert.True(tenHearts.IsInferiorTo(tenSpades, Instance));
			Assert.True(tenHearts.IsInferiorTo(nineSpades, Instance));
			Assert.True(tenHearts.IsInferiorTo(jackHearts, Instance));
			Assert.False(tenHearts.IsInferiorTo(tenHearts, Instance));
			Assert.False(tenHearts.IsInferiorTo(nineHearts, Instance));
			Assert.True(tenHearts.IsInferiorTo(jackClubs, Instance));
			Assert.True(tenHearts.IsInferiorTo(tenClubs, Instance));
			Assert.True(tenHearts.IsInferiorTo(nineClubs, Instance));
			Assert.False(tenHearts.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(tenHearts.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(tenHearts.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(nineHearts.IsInferiorTo(jackSpades, Instance));
			Assert.True(nineHearts.IsInferiorTo(tenSpades, Instance));
			Assert.True(nineHearts.IsInferiorTo(nineSpades, Instance));
			Assert.True(nineHearts.IsInferiorTo(jackHearts, Instance));
			Assert.True(nineHearts.IsInferiorTo(tenHearts, Instance));
			Assert.False(nineHearts.IsInferiorTo(nineHearts, Instance));
			Assert.True(nineHearts.IsInferiorTo(jackClubs, Instance));
			Assert.True(nineHearts.IsInferiorTo(tenClubs, Instance));
			Assert.True(nineHearts.IsInferiorTo(nineClubs, Instance));
			Assert.False(nineHearts.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(nineHearts.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(nineHearts.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(jackClubs.IsInferiorTo(jackSpades, Instance));
			Assert.True(jackClubs.IsInferiorTo(tenSpades, Instance));
			Assert.True(jackClubs.IsInferiorTo(nineSpades, Instance));
			Assert.False(jackClubs.IsInferiorTo(jackHearts, Instance));
			Assert.False(jackClubs.IsInferiorTo(tenHearts, Instance));
			Assert.False(jackClubs.IsInferiorTo(nineHearts, Instance));
			Assert.False(jackClubs.IsInferiorTo(jackClubs, Instance));
			Assert.False(jackClubs.IsInferiorTo(tenClubs, Instance));
			Assert.False(jackClubs.IsInferiorTo(nineClubs, Instance));
			Assert.False(jackClubs.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(jackClubs.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(jackClubs.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(tenClubs.IsInferiorTo(jackSpades, Instance));
			Assert.True(tenClubs.IsInferiorTo(tenSpades, Instance));
			Assert.True(tenClubs.IsInferiorTo(nineSpades, Instance));
			Assert.False(tenClubs.IsInferiorTo(jackHearts, Instance));
			Assert.False(tenClubs.IsInferiorTo(tenHearts, Instance));
			Assert.False(tenClubs.IsInferiorTo(nineHearts, Instance));
			Assert.True(tenClubs.IsInferiorTo(jackClubs, Instance));
			Assert.False(tenClubs.IsInferiorTo(tenClubs, Instance));
			Assert.False(tenClubs.IsInferiorTo(nineClubs, Instance));
			Assert.False(tenClubs.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(tenClubs.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(tenClubs.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(nineClubs.IsInferiorTo(jackSpades, Instance));
			Assert.True(nineClubs.IsInferiorTo(tenSpades, Instance));
			Assert.True(nineClubs.IsInferiorTo(nineSpades, Instance));
			Assert.False(nineClubs.IsInferiorTo(jackHearts, Instance));
			Assert.False(nineClubs.IsInferiorTo(tenHearts, Instance));
			Assert.False(nineClubs.IsInferiorTo(nineHearts, Instance));
			Assert.True(nineClubs.IsInferiorTo(jackClubs, Instance));
			Assert.True(nineClubs.IsInferiorTo(tenClubs, Instance));
			Assert.False(nineClubs.IsInferiorTo(nineClubs, Instance));
			Assert.False(nineClubs.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(nineClubs.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(nineClubs.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(jackDiamonds.IsInferiorTo(jackSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(tenSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(nineSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(jackHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(tenHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(nineHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(jackClubs, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(tenClubs, Instance));
			Assert.True(jackDiamonds.IsInferiorTo(nineClubs, Instance));
			Assert.False(jackDiamonds.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(jackDiamonds.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(jackDiamonds.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(tenDiamonds.IsInferiorTo(jackSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(tenSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(nineSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(jackHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(tenHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(nineHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(jackClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(tenClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(nineClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorTo(jackDiamonds, Instance));
			Assert.False(tenDiamonds.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(tenDiamonds.IsInferiorTo(nineDiamonds, Instance));

			Assert.True(nineDiamonds.IsInferiorTo(jackSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(tenSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(nineSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(jackHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(tenHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(nineHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(jackClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(tenClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(nineClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(jackDiamonds, Instance));
			Assert.True(nineDiamonds.IsInferiorTo(tenDiamonds, Instance));
			Assert.False(nineDiamonds.IsInferiorTo(nineDiamonds, Instance));
		}

		[TestMethod]
		public void IsInferiorOrEqualTo() {
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsInferiorOrEqualTo(null, Instance));
			Assert.ThrowsExact<ArgumentNullException>(() => Card.Joker.IsInferiorOrEqualTo(Card.Joker, null));

			var jackSpades = new Card(Suit.Spades, Rank.Jack);
			var tenSpades = new Card(Suit.Spades, Rank.Ten);
			var nineSpades = new Card(Suit.Spades, Rank.Nine);
			var jackHearts = new Card(Suit.Hearts, Rank.Jack);
			var tenHearts = new Card(Suit.Hearts, Rank.Ten);
			var nineHearts = new Card(Suit.Hearts, Rank.Nine);
			var jackClubs = new Card(Suit.Clubs, Rank.Jack);
			var tenClubs = new Card(Suit.Clubs, Rank.Ten);
			var nineClubs = new Card(Suit.Clubs, Rank.Nine);
			var jackDiamonds = new Card(Suit.Diamonds, Rank.Jack);
			var tenDiamonds = new Card(Suit.Diamonds, Rank.Ten);
			var nineDiamonds = new Card(Suit.Diamonds, Rank.Nine);

			Assert.True(jackSpades.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(jackSpades.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(tenSpades.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(tenSpades.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(tenSpades.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(nineSpades.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(nineSpades.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(nineSpades.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(nineSpades.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(jackHearts.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(jackHearts.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(jackHearts.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(jackHearts.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(jackHearts.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(jackHearts.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(jackHearts.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(tenHearts.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(tenHearts.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(tenHearts.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(tenHearts.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(tenHearts.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(tenHearts.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(nineHearts.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(nineHearts.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(nineHearts.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(nineHearts.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(nineHearts.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(jackClubs.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(jackClubs.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(jackClubs.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(jackClubs.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(jackClubs.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(tenClubs.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(tenClubs.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(tenClubs.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(tenClubs.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(tenClubs.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(tenClubs.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(nineClubs.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(nineClubs.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(nineClubs.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(nineClubs.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(nineClubs.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(nineClubs.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(nineClubs.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(jackDiamonds.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.True(jackDiamonds.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.False(jackDiamonds.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(jackDiamonds.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(tenDiamonds.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(tenDiamonds.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.False(tenDiamonds.IsInferiorOrEqualTo(nineDiamonds, Instance));

			Assert.True(nineDiamonds.IsInferiorOrEqualTo(jackSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(tenSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(nineSpades, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(jackHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(tenHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(nineHearts, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(jackClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(tenClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(nineClubs, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(jackDiamonds, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(tenDiamonds, Instance));
			Assert.True(nineDiamonds.IsInferiorOrEqualTo(nineDiamonds, Instance));
		}
	}
}
