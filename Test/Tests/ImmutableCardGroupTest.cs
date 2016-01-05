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
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
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
		public void AppendAndPrepend() {
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
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
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
			Assert.Throws<ArgumentException>(() => deck.Shuffle(-1));
			Assert.Throws<ArgumentException>(() => deck.Shuffle(0));
			Assert.Throws<ArgumentNullException>(() => deck.Shuffle(null));
			var ordered = deck.Shuffle();
			Assert.NotEqual(deck, ordered);
			Assert.Equal(deck, CardGroup.MakeStandardDeck().AsImmutable());
		}

		[TestMethod]
		public void Split() {
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
			var split = deck.Split();
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Count == 26);
			Assert.True(split.Bottom.Count == 26);
			Assert.True(deck.Count == 52);

			deck = CardGroup.MakeStandardDeck().AsImmutable();
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(0));
			split = deck.Split(13);
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Count == 13);
			Assert.True(split.Bottom.Count == 39);
			Assert.True(deck.Count == 52);
		}

		[TestMethod]
		public void Distribute() {
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
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
			deck = CardGroup.MakeStandardDeck().AsImmutable();
			Assert.DoesNotThrow(() => deck.Reverse());

			var deck2 = deck.Reverse();
			Assert.True(deck.Top == deck2.Bottom);
			Assert.True(deck.Bottom == deck2.Top);
		}

		[TestMethod]
		public void Duplicate() {
			var deck = new ImmutableCardGroup();
			Assert.DoesNotThrow(() => deck.Duplicate());
			deck = CardGroup.MakeStandardDeck().AsImmutable();
			Assert.DoesNotThrow(() => deck.Duplicate());
		}
	}
}
