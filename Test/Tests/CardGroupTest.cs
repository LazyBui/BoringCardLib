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

			deck = new CardGroup();
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Top; });
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Bottom; });
		}

		[TestMethod]
		public void MakeStandardDeck() {
			var deck = CardGroup.MakeStandardDeck();
			Assert.True(deck.Size == 52);

			var ranks = new[] {
				Rank.Two,
				Rank.Three,
				Rank.Four,
				Rank.Five,
				Rank.Six,
				Rank.Seven,
				Rank.Eight,
				Rank.Nine,
				Rank.Ten,
				Rank.Jack,
				Rank.Queen,
				Rank.King,
				Rank.Ace
			};
			Action<Suit> verifySuit = s => {
				Assert.Exactly(deck, 13, v => v.Suit == s);
				Assert.True(ranks.All(r => deck.Any(v => v.Suit == s && v.Rank == r)));
			};

			verifySuit(Suit.Clubs);
			verifySuit(Suit.Diamonds);
			verifySuit(Suit.Hearts);
			verifySuit(Suit.Spades);

			deck = CardGroup.MakeStandardDeck(true);
			Assert.True(deck.Size == 54);

			verifySuit(Suit.Clubs);
			verifySuit(Suit.Diamonds);
			verifySuit(Suit.Hearts);
			verifySuit(Suit.Spades);
			Assert.True(deck.Count(v => v.Suit == Suit.Joker && v.Rank == Rank.Joker) == 2);

			deck = CardGroup.MakeStandardDeck(initializeValues: c => {
				switch (c.Rank) {
					case Rank.Ace: return c.WithValue(1);
					case Rank.Two: return c.WithValue(2);
					case Rank.Three: return c.WithValue(3);
					case Rank.Four: return c.WithValue(4);
					case Rank.Five: return c.WithValue(5);
					case Rank.Six: return c.WithValue(6);
					case Rank.Seven: return c.WithValue(7);
					case Rank.Eight: return c.WithValue(8);
					case Rank.Nine: return c.WithValue(9);
					case Rank.Ten: return c.WithValue(10);
					case Rank.Jack: return c.WithValue(11);
					case Rank.Queen: return c.WithValue(12);
					case Rank.King: return c.WithValue(13);
					default: throw new NotImplementedException();
				}
			});
			Assert.True(deck.Size == 52);
			Assert.None(deck, c => c.Value == null || c.Value < 1 || c.Value > 13);
			for (int i = 0; i < 13; i++) {
				Assert.True(deck.Count(c => c.Value == (i + 1)) == 4);
			}
		}

		[TestMethod]
		public void MakeFullSuit() {
			var deck = CardGroup.MakeFullSuit(Suit.Spades);
			Assert.True(deck.Size == 13);
			Assert.All(deck, v => v.Suit == Suit.Spades);
			var ranks = new[] {
				Rank.Two,
				Rank.Three,
				Rank.Four,
				Rank.Five,
				Rank.Six,
				Rank.Seven,
				Rank.Eight,
				Rank.Nine,
				Rank.Ten,
				Rank.Jack,
				Rank.Queen,
				Rank.King,
				Rank.Ace
			};
			Assert.True(ranks.All(r => deck.Any(v => v.Rank == r)));

			Assert.ThrowsExact<ArgumentException>(() => deck = CardGroup.MakeFullSuit((Suit)(-1)));

			deck = CardGroup.MakeFullSuit(Suit.Spades, initializeValues: c => {
				switch (c.Rank) {
					case Rank.Ace: return c.WithValue(1);
					case Rank.Two: return c.WithValue(2);
					case Rank.Three: return c.WithValue(3);
					case Rank.Four: return c.WithValue(4);
					case Rank.Five: return c.WithValue(5);
					case Rank.Six: return c.WithValue(6);
					case Rank.Seven: return c.WithValue(7);
					case Rank.Eight: return c.WithValue(8);
					case Rank.Nine: return c.WithValue(9);
					case Rank.Ten: return c.WithValue(10);
					case Rank.Jack: return c.WithValue(11);
					case Rank.Queen: return c.WithValue(12);
					case Rank.King: return c.WithValue(13);
					default: throw new NotImplementedException();
				}
			});
			Assert.True(deck.Size == 13);
			Assert.None(deck, c => c.Value == null || c.Value < 1 || c.Value > 13);
			for (int i = 0; i < 13; i++) {
				Assert.True(deck.Count(c => c.Value == (i + 1)) == 1);
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
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append());
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new List<Card>()));
			var card = deck.Draw();
			deck.Append(card);
			Assert.Equal(deck.Bottom, card);

			deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend());
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new List<Card>()));
			card = deck.Draw();
			deck.Prepend(card);
			Assert.Equal(deck.Top, card);
		}

		[TestMethod]
		public void Shuffle() {
			var deck = CardGroup.MakeStandardDeck();
			Assert.Throws<ArgumentException>(() => deck.Shuffle(-1));
			Assert.Throws<ArgumentException>(() => deck.Shuffle(0));
			var ordered = new CardGroup(deck);
			deck.Shuffle();
			Assert.NotEqual(deck, ordered);
		}

		[TestMethod]
		public void Split() {
			var deck = CardGroup.MakeStandardDeck();
			var split = deck.Split();
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Size == 26);
			Assert.True(split.Bottom.Size == 26);

			deck = CardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(0));
			split = deck.Split(13);
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Size == 13);
			Assert.True(split.Bottom.Size == 39);
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
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(2, remainderPolicy: (RemainderPolicy)(-1)));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(2, distributionPolicy: (DistributionPolicy)(-1)));

			CardGroup[] split = null;
			
			// Test distribution ordering
			split = deck.Distribute(2, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Spades, Rank.Three));

			split = deck.Distribute(3, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Spades, Rank.Three));
			Assert.True(split[2].Top == new Card(Suit.Spades, Rank.Four));

			split = deck.Distribute(2, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Hearts, Rank.Two));

			split = deck.Distribute(3, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Clubs, Rank.Six));
			Assert.True(split[2].Top == new Card(Suit.Hearts, Rank.Ten));

			// Test remainder distribution
			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Size == 26);
			Assert.True(split[1].Size == 26);
			
			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Size == 26);
			Assert.True(split[1].Size == 26);

			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Size == 26);
			Assert.True(split[1].Size == 26);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Size == 18);
			Assert.True(split[1].Size == 17);
			Assert.True(split[2].Size == 17);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Size == 17);
			Assert.True(split[1].Size == 17);
			Assert.True(split[2].Size == 17);
			Assert.True(split[3].Size == 1);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Size == 17);
			Assert.True(split[1].Size == 17);
			Assert.True(split[2].Size == 17);

			deck.Draw(2);
			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Size == 17);
			Assert.True(split[1].Size == 17);
			Assert.True(split[2].Size == 16);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Size == 16);
			Assert.True(split[1].Size == 16);
			Assert.True(split[2].Size == 16);
			Assert.True(split[3].Size == 2);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Size == 16);
			Assert.True(split[1].Size == 16);
			Assert.True(split[2].Size == 16);
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
				new DiscardRequest(direction: Direction.FromBottom, quantity: 1));
			Assert.True(discard.Size == 1);
			Assert.True(deck.Size == 51);
			Assert.Equal(bottom, discard.Top);

			deck = CardGroup.MakeStandardDeck();
			top = deck.Top;
			discard = deck.Discard(
				new DiscardRequest(direction: Direction.FromTop, quantity: 1));
			Assert.True(discard.Size == 1);
			Assert.True(deck.Size == 51);
			Assert.Equal(top, discard.Top);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(suit: Suit.Clubs));
			Assert.True(discard.Size == 13);
			Assert.True(deck.Size == 52 - 13);
			Assert.All(discard, v => v.Suit == Suit.Clubs);
			Assert.All(deck, v => v.Suit != Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(suit: Suit.Clubs, quantity: 3));
			Assert.True(discard.Size == 3);
			Assert.True(deck.Size == 52 - 3);
			Assert.All(discard, v => v.Suit == Suit.Clubs);
			Assert.Exactly(deck, 13 - 3, v => v.Suit == Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(rank: Rank.Ace));
			Assert.True(discard.Size == 4);
			Assert.True(deck.Size == 52 - 4);
			Assert.All(discard, v => v.Rank == Rank.Ace);
			Assert.All(deck, v => v.Rank != Rank.Ace);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(rank: Rank.Ace, quantity: 3));
			Assert.True(discard.Size == 3);
			Assert.True(deck.Size == 52 - 3);
			Assert.All(discard, v => v.Rank == Rank.Ace);
			Assert.Exactly(deck, 1, v => v.Rank == Rank.Ace);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(quantity: 100));
			Assert.True(discard.Size == 52);
			Assert.True(deck.Size == 0);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(suits: new[] { Suit.Spades, Suit.Clubs }));
			Assert.True(discard.Size == 26);
			Assert.True(deck.Size == 26);
			Assert.All(discard, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.All(deck, v => v.Suit == Suit.Diamonds || v.Suit == Suit.Hearts);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(suits: new[] { Suit.Spades, Suit.Clubs }, quantity: 6));
			Assert.True(discard.Size == 6);
			Assert.True(deck.Size == 52 - 6);
			Assert.All(discard, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.Exactly(deck, 26 - 6, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(ranks: new[] { Rank.Two, Rank.Three }));
			Assert.True(discard.Size == 8);
			Assert.True(deck.Size == 52 - 8);
			Assert.All(discard, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.None(deck, v => v.Rank == Rank.Two || v.Rank == Rank.Three);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard(
				new DiscardRequest(ranks: new[] { Rank.Two, Rank.Three }, quantity: 6));
			Assert.True(discard.Size == 6);
			Assert.True(deck.Size == 52 - 6);
			Assert.All(discard, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.Exactly(deck, 8 - 6, v => v.Rank == Rank.Two || v.Rank == Rank.Three);

			deck = CardGroup.MakeStandardDeck();
			discard = deck.Discard();
			Assert.True(discard.Size == 52);
			Assert.True(deck.Size == 0);
		}
	}
}
