using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class CardGroupTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new CardGroup(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => new CardGroup(null as List<Card>));
			Assert.ThrowsExact<ArgumentException>(() => new CardGroup(new Card[] { null }));

			Assert.DoesNotThrow(() => new CardGroup());
			Assert.DoesNotThrow(() => new CardGroup(
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace)));

			Assert.DoesNotThrow(() => new CardGroup(new[] {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));

			Assert.DoesNotThrow(() => new CardGroup(new List<Card>() {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));
		}

		[TestMethod]
		public void TopAndBottom() {
			var deck = CardGroup.MakeStandardDeck();
			Card top = deck.Top;
			Card bottom = deck.Bottom;

			int elem = 0;
			foreach (var card in deck) {
				if (elem == 0) {
					Assert.Equal(top, card);
				}
				else if (elem == deck.Size) {
					Assert.Equal(bottom, card);
				}
				elem++;
			}
		}

		[TestMethod]
		public void Draw() {
			var deck = CardGroup.MakeStandardDeck();
			int max = deck.Size;
			for (int i = 0; i < max; i++) {
				Card top = deck.Top;
				Card draw = deck.Draw();
				Assert.Equal(top, draw);
			}
			Assert.True(deck.Size == 0);

			deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Draw(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Draw(0));
			CardGroup drawn = deck.Draw(5);

			Assert.True(deck.Size == 52 - 5);
			Assert.True(drawn.Size == 5);
			Assert.None(drawn, v => deck.Contains(v));
			Assert.None(deck, v => drawn.Contains(v));
		}

		[TestMethod]
		public void AppendAndPrepend() {
			var deck = CardGroup.MakeStandardDeck();
			var card = deck.Draw();
			deck.Append(card);
			Assert.Equal(deck.Bottom, card);

			deck = CardGroup.MakeStandardDeck();
			card = deck.Draw();
			deck.Prepend(card);
			Assert.Equal(deck.Top, card);
		}

		[TestMethod]
		public void Shuffle() {
			var deck = CardGroup.MakeStandardDeck();
			var ordered = new CardGroup(deck);
			deck.Shuffle();
			Assert.NotEqual(deck, ordered);
		}

		[TestMethod]
		public void Split() {
			var deck = CardGroup.MakeStandardDeck();
			var split = deck.Split();
			Assert.NotNull(split.Item1);
			Assert.NotNull(split.Item2);
			Assert.True(split.Item1.Size == 26);
			Assert.True(split.Item2.Size == 26);

			deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(0));
			split = deck.Split(13);
			Assert.NotNull(split.Item1);
			Assert.NotNull(split.Item2);
			Assert.True(split.Item1.Size == 13);
			Assert.True(split.Item2.Size == 39);
		}

		[TestMethod]
		public void Replace() {
			var deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(null, deck.Top));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(deck.Top, null));
			Card top = deck.Top;
			Card replace = new Card(Suit.Joker, Rank.Joker);
			Assert.True(deck.Replace(top, replace) == CardStackOperation.Performed);
			Assert.True(deck.Replace(top, replace) == CardStackOperation.CardNotPresent);
			Assert.NotEqual(deck.Top, top);
			Assert.Equal(deck.Top, replace);
		}

		[TestMethod]
		public void Distribute() {
			var deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(0));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(1));

			var split = deck.Distribute(2);
			Assert.True(split.Count() == 2);
			Assert.True(split.First().Size == 26);
			Assert.True(split.Last().Size == 26);

			split = deck.Distribute(3);
			Assert.True(split.Count() == 3);
			Assert.True(split.First().Size == 18);
			Assert.True(split.ElementAt(1).Size == 17);
			Assert.True(split.Last().Size == 17);

			deck.Draw(2);
			split = deck.Distribute(3);
			Assert.True(split.Count() == 3);
			Assert.True(split.First().Size == 17);
			Assert.True(split.ElementAt(1).Size == 17);
			Assert.True(split.Last().Size == 16);
		}

		[TestMethod]
		public void Discard() {
			var deck = CardGroup.MakeStandardDeck();
			Card bottom = null;
			Card top = null;
			CardGroup discard = null;

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Discard(null as Card));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Discard(null as DiscardRequest));

			bottom = deck.Bottom;
			Assert.True(deck.Discard(bottom) == CardStackOperation.Performed);
			Assert.True(deck.Size == 51);
			Assert.True(deck.Discard(bottom) == CardStackOperation.CardNotPresent);

			deck = CardGroup.MakeStandardDeck();
			bottom = deck.Bottom;
			discard = deck.Discard(
				new DiscardRequest(pDirection: Direction.FromBottom, pQuantity: 1));
			Assert.True(discard.Size == 1);
			Assert.True(deck.Size == 51);
			Assert.Equal(bottom, discard.Top);

			deck = CardGroup.MakeStandardDeck();
			top = deck.Top;
			discard = deck.Discard(
				new DiscardRequest(pDirection: Direction.FromTop, pQuantity: 1));
			Assert.True(discard.Size == 1);
			Assert.True(deck.Size == 51);
			Assert.Equal(top, discard.Top);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pSuit: Suit.Clubs));
			Assert.True(discard.Size == 13);
			Assert.True(deck.Size == 52 - 13);
			Assert.All(discard, v => v.Suit == Suit.Clubs);
			Assert.All(deck, v => v.Suit != Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pSuit: Suit.Clubs, pQuantity: 3));
			Assert.True(discard.Size == 3);
			Assert.True(deck.Size == 52 - 3);
			Assert.All(discard, v => v.Suit == Suit.Clubs);
			Assert.Exactly(deck, 13 - 3, v => v.Suit == Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pRank: Rank.Ace));
			Assert.True(discard.Size == 4);
			Assert.True(deck.Size == 52 - 4);
			Assert.All(discard, v => v.Rank == Rank.Ace);
			Assert.All(deck, v => v.Rank != Rank.Ace);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pRank: Rank.Ace, pQuantity: 3));
			Assert.True(discard.Size == 3);
			Assert.True(deck.Size == 52 - 3);
			Assert.All(discard, v => v.Rank == Rank.Ace);
			Assert.Exactly(deck, 1, v => v.Rank == Rank.Ace);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pQuantity: 100));
			Assert.True(discard.Size == 52);
			Assert.True(deck.Size == 0);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pSuits: new[] { Suit.Spades, Suit.Clubs }));
			Assert.True(discard.Size == 26);
			Assert.True(deck.Size == 26);
			Assert.All(discard, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.All(deck, v => v.Suit == Suit.Diamonds || v.Suit == Suit.Hearts);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pSuits: new[] { Suit.Spades, Suit.Clubs }, pQuantity: 6));
			Assert.True(discard.Size == 6);
			Assert.True(deck.Size == 52 - 6);
			Assert.All(discard, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.Exactly(deck, 26 - 6, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pRanks: new[] { Rank.Two, Rank.Three }));
			Assert.True(discard.Size == 8);
			Assert.True(deck.Size == 52 - 8);
			Assert.All(discard, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.None(deck, v => v.Rank == Rank.Two || v.Rank == Rank.Three);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(pRanks: new[] { Rank.Two, Rank.Three }, pQuantity: 6));
			Assert.True(discard.Size == 6);
			Assert.True(deck.Size == 52 - 6);
			Assert.All(discard, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.Exactly(deck, 8 - 6, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
		}
	}
}
