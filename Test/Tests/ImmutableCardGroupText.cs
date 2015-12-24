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
				else if (elem == deck.Size) {
					Assert.Equal(bottom, card);
				}
				elem++;
			}

			deck = new ImmutableCardGroup();
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Top; });
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Bottom; });
		}

		[TestMethod]
		public void Split() {
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
			var split = deck.Split();
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Size == 26);
			Assert.True(split.Bottom.Size == 26);

			deck = CardGroup.MakeStandardDeck().AsImmutable();
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Split(0));
			split = deck.Split(13);
			Assert.NotNull(split.Top);
			Assert.NotNull(split.Bottom);
			Assert.True(split.Top.Size == 13);
			Assert.True(split.Bottom.Size == 39);
		}

		[TestMethod]
		public void Distribute() {
			var deck = CardGroup.MakeStandardDeck().AsImmutable();
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

			var modifiedDeck = CardGroup.MakeStandardDeck();
			modifiedDeck.Draw(2);
			deck = modifiedDeck.AsImmutable();

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
	}
}
