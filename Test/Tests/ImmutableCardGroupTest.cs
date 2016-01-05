using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ImmutableCardGroupTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableCardGroup(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableCardGroup(null as List<Card>));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableCardGroup(new Card[] { null }));

			Assert.DoesNotThrow(() => new ImmutableCardGroup());
			Assert.DoesNotThrow(() => new ImmutableCardGroup(
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace)));

			Assert.DoesNotThrow(() => new ImmutableCardGroup(new[] {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));

			Assert.DoesNotThrow(() => new ImmutableCardGroup(new List<Card>() {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));
		}

		[TestMethod]
		public void TopAndBottom() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			Card top = deck.Top;
			Card bottom = deck.Bottom;

			int elem = 0;
			foreach (var card in deck) {
				if (elem == 0) {
					Assert.Equal(top, card);
				}
				else if (elem == deck.Count) {
					Assert.Equal(bottom, card);
				}
				elem++;
			}

			deck = new ImmutableCardGroup();
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Top; });
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Bottom; });
		}

		[TestMethod]
		public void MakeStandardDeck() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			Assert.True(deck.Count == 52);

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

			deck = ImmutableCardGroup.MakeStandardDeck(true);
			Assert.True(deck.Count == 54);

			verifySuit(Suit.Clubs);
			verifySuit(Suit.Diamonds);
			verifySuit(Suit.Hearts);
			verifySuit(Suit.Spades);
			Assert.True(deck.Count(v => v.Suit == Suit.Joker && v.Rank == Rank.Joker) == 2);

			deck = ImmutableCardGroup.MakeStandardDeck(initializeValues: c => {
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
			Assert.True(deck.Count == 52);
			Assert.None(deck, c => c.Value == null || c.Value < 1 || c.Value > 13);
			for (int i = 0; i < 13; i++) {
				Assert.True(deck.Count(c => c.Value == (i + 1)) == 4);
			}
		}

		[TestMethod]
		public void MakeFullSuit() {
			var deck = ImmutableCardGroup.MakeFullSuit(Suit.Spades);
			Assert.True(deck.Count == 13);
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

			Assert.ThrowsExact<ArgumentException>(() => deck = ImmutableCardGroup.MakeFullSuit((Suit)(-1)));

			deck = ImmutableCardGroup.MakeFullSuit(Suit.Spades, initializeValues: c => {
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
			Assert.True(deck.Count == 13);
			Assert.None(deck, c => c.Value == null || c.Value < 1 || c.Value > 13);
			for (int i = 0; i < 13; i++) {
				Assert.True(deck.Count(c => c.Value == (i + 1)) == 1);
			}
		}

		[TestMethod]
		public void Draw() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			int max = deck.Count;
			ImmutableCardGroup top = new ImmutableCardGroup(deck.Top);
			var result = deck.Draw();
			Assert.True(deck.Count == 52);
			Assert.NotNull(result.Drawn);
			Assert.NotNull(result.DrawnFrom);
			Assert.True(result.DrawnFrom.Count == 51);
			Assert.True(result.Drawn.Count == 1);
			Assert.Equal(result.Drawn, top);

			Assert.ThrowsExact<ArgumentException>(() => deck.Draw(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Draw(0));
			result = deck.Draw(5);

			Assert.True(deck.Count == 52);
			Assert.True(result.DrawnFrom.Count == 52 - 5);
			Assert.True(result.Drawn.Count == 5);
			Assert.None(result.Drawn, v => result.DrawnFrom.Contains(v));
			Assert.None(result.DrawnFrom, v => result.Drawn.Contains(v));
		}

		[TestMethod]
		public void AppendAndPrepend() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			var card = deck.Top;
			var mutable = deck.AsMutable();
			mutable.Discard(1);
			var convertedMutable = mutable.AsImmutable();

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append());
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new List<Card>()));
			var newDeck = convertedMutable.Append(card);
			Assert.Equal(newDeck.Bottom, card);
			Assert.True(deck.Count == 52);
			Assert.True(convertedMutable.Count == 51);
			Assert.True(newDeck.Count == 52);			

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend());
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new List<Card>()));
			newDeck = convertedMutable.Prepend(card);
			Assert.Equal(newDeck.Top, card);
			Assert.True(deck.Count == 52);
			Assert.True(convertedMutable.Count == 51);
			Assert.True(newDeck.Count == 52);			
		}

		[TestMethod]
		public void Shuffle() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			Assert.Throws<ArgumentException>(() => deck.Shuffle(-1));
			Assert.Throws<ArgumentException>(() => deck.Shuffle(0));
			Assert.Throws<ArgumentNullException>(() => deck.Shuffle(null));
			var ordered = deck.Shuffle();
			Assert.NotEqual(deck, ordered);
			Assert.Equal(deck, ImmutableCardGroup.MakeStandardDeck());
		}

		[TestMethod]
		public void Split() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			var split = deck.Split();
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Count == 26);
			Assert.True(split.Bottom.Count == 26);
			Assert.True(deck.Count == 52);

			Assert.ThrowsExact<ArgumentException>(() => deck.Split(-1));
			split = deck.Split(0);
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Count == 0);
			Assert.True(split.Bottom.Count == 52);
			Assert.True(deck.Count == 52);

			split = deck.Split(13);
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Count == 13);
			Assert.True(split.Bottom.Count == 39);
			Assert.True(deck.Count == 52);
		}

		[TestMethod]
		public void Distribute() {
			var deck = ImmutableCardGroup.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(0));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(2, remainderPolicy: (RemainderPolicy)(-1)));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(2, distributionPolicy: (DistributionPolicy)(-1)));

			ImmutableCardGroup[] split = null;
			
			// Test distribution ordering
			split = deck.Distribute(2, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Spades, Rank.Three));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(3, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Spades, Rank.Three));
			Assert.True(split[2].Top == new Card(Suit.Spades, Rank.Four));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(2, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Hearts, Rank.Two));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(3, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split[0].Top == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].Top == new Card(Suit.Clubs, Rank.Six));
			Assert.True(split[2].Top == new Card(Suit.Hearts, Rank.Ten));
			Assert.True(deck.Count == 52);

			// Test remainder distribution
			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);
			
			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(2, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 18);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(split[3].Count == 1);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(deck.Count == 52);

			var modifiedDeck = CardGroup.MakeStandardDeck();
			modifiedDeck.Draw(2);
			deck = modifiedDeck.AsImmutable();

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.Distribute).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 16);
			Assert.True(deck.Count == 50);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.SeparatePile).ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Count == 16);
			Assert.True(split[1].Count == 16);
			Assert.True(split[2].Count == 16);
			Assert.True(split[3].Count == 2);
			Assert.True(deck.Count == 50);

			split = deck.Distribute(3, remainderPolicy: RemainderPolicy.NoRemainder).ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 16);
			Assert.True(split[1].Count == 16);
			Assert.True(split[2].Count == 16);
			Assert.True(deck.Count == 50);
		}

		[TestMethod]
		public void Reverse() {
			var deck = new ImmutableCardGroup();
			Assert.DoesNotThrow(() => deck.Reverse());
			deck = ImmutableCardGroup.MakeStandardDeck();
			Assert.DoesNotThrow(() => deck.Reverse());

			var deck2 = deck.Reverse();
			Assert.True(deck.Top == deck2.Bottom);
			Assert.True(deck.Bottom == deck2.Top);
		}

		[TestMethod]
		public void Duplicate() {
			var deck = new ImmutableCardGroup();
			Assert.DoesNotThrow(() => deck.Duplicate());
			deck = ImmutableCardGroup.MakeStandardDeck();
			Assert.DoesNotThrow(() => deck.Duplicate());
		}
	}
}
