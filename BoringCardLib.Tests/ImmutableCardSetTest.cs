using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableCardSetTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableCardSet(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableCardSet(null as List<Card>));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableCardSet(new Card[] { null }));

			Assert.DoesNotThrow(() => new ImmutableCardSet());
			Assert.DoesNotThrow(() => new ImmutableCardSet(
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace)));

			Assert.DoesNotThrow(() => new ImmutableCardSet(new[] {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));

			Assert.DoesNotThrow(() => new ImmutableCardSet(new List<Card>() {
				new Card(Suit.Diamonds, Rank.Ace),
				new Card(Suit.Hearts, Rank.Ace),
				new Card(Suit.Spades, Rank.Ace),
				new Card(Suit.Clubs, Rank.Ace),
			}));
		}

		[TestMethod]
		public void TopAndBottom() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Card top = deck.First;
			Card bottom = deck.Last;

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

			deck = new ImmutableCardSet();
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.First; });
			Assert.ThrowsExact<InvalidOperationException>(() => { var x = deck.Last; });
		}

		[TestMethod]
		public void MakeStandardDeck() {
			var deck = ImmutableCardSet.MakeStandardDeck();
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

			deck = ImmutableCardSet.MakeStandardDeck(jokerCount: 2);
			Assert.True(deck.Count == 54);

			verifySuit(Suit.Clubs);
			verifySuit(Suit.Diamonds);
			verifySuit(Suit.Hearts);
			verifySuit(Suit.Spades);
			Assert.True(deck.Count(v => v.Suit == Suit.Joker && v.Rank == Rank.Joker) == 2);

			deck = ImmutableCardSet.MakeStandardDeck(initializeValues: c => {
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
			var deck = ImmutableCardSet.MakeFullSuit(Suit.Spades);
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

			Assert.ThrowsExact<ArgumentException>(() => deck = ImmutableCardSet.MakeFullSuit((Suit)(-1)));

			deck = ImmutableCardSet.MakeFullSuit(Suit.Spades, initializeValues: c => {
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
			var deck = ImmutableCardSet.MakeStandardDeck();
			int max = deck.Count;
			ImmutableCardSet top = new ImmutableCardSet(deck.First);
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
		public void Append() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			var card = deck.First;
			var mutable = deck.AsMutable();
			mutable.Discard(1);
			var convertedMutable = mutable.AsImmutable();

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Append(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append());
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Append(new List<Card>()));
			var newDeck = convertedMutable.Append(card);
			Assert.Equal(newDeck.Last, card);
			Assert.True(deck.Count == 52);
			Assert.True(convertedMutable.Count == 51);
			Assert.True(newDeck.Count == 52);			
		}

		[TestMethod]
		public void Prepend() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			var card = deck.First;
			var mutable = deck.AsMutable();
			mutable.Discard(1);
			var convertedMutable = mutable.AsImmutable();

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Prepend(null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend());
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Prepend(new List<Card>()));
			var newDeck = convertedMutable.Prepend(card);
			Assert.Equal(newDeck.First, card);
			Assert.True(deck.Count == 52);
			Assert.True(convertedMutable.Count == 51);
			Assert.True(newDeck.Count == 52);			
		}

		[TestMethod]
		public void Insert() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			var card = deck.First;
			var mutable = deck.AsMutable();
			mutable.Discard(1);
			var convertedMutable = mutable.AsImmutable();

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Insert(0, null as Card[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Insert(0, null as IEnumerable<Card>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(0));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(0, new Card[0]));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(0, new List<Card>()));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(-1, Card.Joker));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(-1, new List<Card>() { Card.Joker }));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(Constant.StandardDeckSize + 1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(Constant.StandardDeckSize + 1, Card.Joker));
			Assert.ThrowsExact<ArgumentException>(() => deck.Insert(Constant.StandardDeckSize + 1, new List<Card>() { Card.Joker }));

			var newFirst = convertedMutable.First;
			var newDeck = convertedMutable.Insert(1, card);
			Assert.Equal(newDeck.First, newFirst);
			Assert.Equal(newDeck[1], card);
			Assert.True(deck.Count == Constant.StandardDeckSize);
			Assert.True(convertedMutable.Count == Constant.StandardDeckSize - 1);
			Assert.True(newDeck.Count == Constant.StandardDeckSize);			
		}

		[TestMethod]
		public void Shuffle() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.Throws<ArgumentException>(() => deck.Shuffle(-1));
			Assert.Throws<ArgumentException>(() => deck.Shuffle(0));
			Assert.Throws<ArgumentNullException>(() => deck.Shuffle(null));
			var ordered = deck.Shuffle();
			Assert.NotEqual(deck, ordered);
			Assert.Equal(deck, ImmutableCardSet.MakeStandardDeck());
		}

		[TestMethod]
		public void Replace() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(null, deck.First));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(deck.First, null));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(null as ReplaceRequest[]));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Replace(null as IEnumerable<ReplaceRequest>));
			Assert.ThrowsExact<ArgumentException>(() => deck.Replace(new ReplaceRequest[] { null }));
			Assert.ThrowsExact<ArgumentException>(() => deck.Replace(new ReplaceRequest[] { null } as IEnumerable<ReplaceRequest>));
			Card top = deck.First;
			Card replace = new Card(Suit.Joker, Rank.Joker);

			var result = deck.Replace(top, replace);
			Assert.Contains(result.Replaced, top);
			Assert.Empty(result.NotFound);

			Assert.NotEqual(result.Pile.First, top);
			Assert.Equal(result.Pile.First, replace);

			deck = result.Pile;
			result = deck.Replace(top, replace);
			Assert.Contains(result.NotFound, top);
			Assert.Empty(result.Replaced);
			Assert.Equal(result.Pile, deck);
		}

		[TestMethod]
		public void Discard() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Card bottom = null;
			Card top = null;

			Assert.ThrowsExact<ArgumentNullException>(() => deck.Discard(null as Card));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Discard(null as DiscardRequest));
			Assert.ThrowsExact<ArgumentException>(() => deck.Discard(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Discard(0));
			Assert.DoesNotThrow(() => deck.Discard(1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Discard(1, (Direction)(-1)));

			deck = ImmutableCardSet.MakeStandardDeck();
			bottom = deck.Last;
			var result = deck.Discard(bottom);
			Assert.Contains(result.Discarded, bottom);
			Assert.DoesNotContain(result.Pile, bottom);
			Assert.True(result.Pile.Count == 51);
			result = result.Pile.Discard(bottom);
			Assert.Empty(result.Discarded);

			deck = ImmutableCardSet.MakeStandardDeck();
			bottom = deck.Last;
			result = deck.Discard(
				new DiscardRequest(direction: Direction.LastToFirst, quantity: 1));
			Assert.True(result.Discarded.Count == 1);
			Assert.True(result.Pile.Count == 51);
			Assert.Equal(bottom, result.Discarded.First);

			deck = ImmutableCardSet.MakeStandardDeck();
			top = deck.First;
			result = deck.Discard(
				new DiscardRequest(direction: Direction.FirstToLast, quantity: 1));
			Assert.True(result.Discarded.Count == 1);
			Assert.True(result.Pile.Count == 51);
			Assert.Equal(top, result.Discarded.First);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(suit: Suit.Clubs));
			Assert.True(result.Discarded.Count == 13);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 13);
			Assert.All(result.Discarded, v => v.Suit == Suit.Clubs);
			Assert.All(result.Pile, v => v.Suit != Suit.Clubs);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(suit: Suit.Clubs, quantity: 3));
			Assert.True(result.Discarded.Count == 3);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 3);
			Assert.All(result.Discarded, v => v.Suit == Suit.Clubs);
			Assert.Exactly(result.Pile, 13 - 3, v => v.Suit == Suit.Clubs);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(rank: Rank.Ace));
			Assert.True(result.Discarded.Count == 4);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 4);
			Assert.All(result.Discarded, v => v.Rank == Rank.Ace);
			Assert.All(result.Pile, v => v.Rank != Rank.Ace);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(rank: Rank.Ace, quantity: 3));
			Assert.True(result.Discarded.Count == 3);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 3);
			Assert.All(result.Discarded, v => v.Rank == Rank.Ace);
			Assert.Exactly(result.Pile, 1, v => v.Rank == Rank.Ace);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(quantity: 100));
			Assert.True(result.Discarded.Count == Constant.StandardDeckSize);
			Assert.True(result.Pile.Count == 0);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(suits: new[] { Suit.Spades, Suit.Clubs }));
			Assert.True(result.Discarded.Count == 26);
			Assert.True(result.Pile.Count == 26);
			Assert.All(result.Discarded, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.All(result.Pile, v => v.Suit == Suit.Diamonds || v.Suit == Suit.Hearts);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(suits: new[] { Suit.Spades, Suit.Clubs }, quantity: 6));
			Assert.True(result.Discarded.Count == 6);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 6);
			Assert.All(result.Discarded, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);
			Assert.Exactly(result.Pile, 26 - 6, v => v.Suit == Suit.Spades || v.Suit == Suit.Clubs);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(ranks: new[] { Rank.Two, Rank.Three }));
			Assert.True(result.Discarded.Count == 8);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 8);
			Assert.All(result.Discarded, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.None(result.Pile, v => v.Rank == Rank.Two || v.Rank == Rank.Three);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(
				new DiscardRequest(ranks: new[] { Rank.Two, Rank.Three }, quantity: 6));
			Assert.True(result.Discarded.Count == 6);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 6);
			Assert.All(result.Discarded, v => v.Rank == Rank.Two || v.Rank == Rank.Three);
			Assert.Exactly(result.Pile, 8 - 6, v => v.Rank == Rank.Two || v.Rank == Rank.Three);

			deck = ImmutableCardSet.MakeStandardDeck(initializeValues: card => {
				switch (card.Rank) {
					case Rank.Ace: return card.WithValue(1);
					case Rank.Two: return card.WithValue(2);
					case Rank.Three: return card.WithValue(3);
					case Rank.Four: return card.WithValue(4);
					case Rank.Five: return card.WithValue(5);
					case Rank.Six: return card.WithValue(6);
					case Rank.Seven: return card.WithValue(7);
					case Rank.Eight: return card.WithValue(8);
					case Rank.Nine: return card.WithValue(9);
					case Rank.Ten: return card.WithValue(10);
					case Rank.Jack: return card.WithValue(11);
					case Rank.Queen: return card.WithValue(12);
					case Rank.King: return card.WithValue(13);
					default: throw new NotImplementedException();
				}
			});
			result = deck.Discard(new DiscardRequest(value: 4));
			Assert.True(result.Discarded.Count == 4);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 4);

			deck = ImmutableCardSet.MakeStandardDeck(initializeValues: card => {
				switch (card.Rank) {
					case Rank.Ace: return card.WithValue(1);
					case Rank.Two: return card.WithValue(2);
					case Rank.Three: return card.WithValue(3);
					case Rank.Four: return card.WithValue(4);
					case Rank.Five: return card.WithValue(5);
					case Rank.Six: return card.WithValue(6);
					case Rank.Seven: return card.WithValue(7);
					case Rank.Eight: return card.WithValue(8);
					case Rank.Nine: return card.WithValue(9);
					case Rank.Ten: return card.WithValue(10);
					case Rank.Jack: return card.WithValue(11);
					case Rank.Queen: return card.WithValue(12);
					case Rank.King: return card.WithValue(13);
					default: throw new NotImplementedException();
				}
			});
			result = deck.Discard(new DiscardRequest(values: new[] { 4, 5, 6 }));
			Assert.True(result.Discarded.Count == 12);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 12);

			deck = ImmutableCardSet.MakeStandardDeck(initializeValues: card => {
				switch (card.Rank) {
					case Rank.Ace: return card.WithValue(1);
					case Rank.Two: return card.WithValue(2);
					case Rank.Three: return card.WithValue(3);
					case Rank.Four: return card.WithValue(4);
					case Rank.Five: return card.WithValue(5);
					case Rank.Six: return card.WithValue(6);
					case Rank.Seven: return card.WithValue(7);
					case Rank.Eight: return card.WithValue(8);
					case Rank.Nine: return card.WithValue(9);
					case Rank.Ten: return card.WithValue(10);
					case Rank.Jack: return card.WithValue(11);
					case Rank.Queen: return card.WithValue(12);
					default: return card;
				}
			});
			result = deck.Discard(new DiscardRequest(nullValues: true));
			Assert.True(result.Discarded.Count == 4);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 4);

			deck = ImmutableCardSet.MakeStandardDeck(initializeValues: card => {
				switch (card.Rank) {
					case Rank.Ace: return card.WithValue(1);
					case Rank.Two: return card.WithValue(2);
					case Rank.Three: return card.WithValue(3);
					case Rank.Four: return card.WithValue(4);
					case Rank.Five: return card.WithValue(5);
					case Rank.Six: return card.WithValue(6);
					case Rank.Seven: return card.WithValue(7);
					case Rank.Eight: return card.WithValue(8);
					case Rank.Nine: return card.WithValue(9);
					case Rank.Ten: return card.WithValue(10);
					case Rank.Jack: return card.WithValue(11);
					case Rank.Queen: return card.WithValue(12);
					default: return card;
				}
			});
			result = deck.Discard(new DiscardRequest(values: new[] { 4, 5, 6 }, nullValues: true));
			Assert.True(result.Discarded.Count == 16);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 16);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(card: new Card(Suit.Spades, Rank.Two)));
			Assert.True(result.Discarded.Count == 1);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 1);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(cards: new[] { new Card(Suit.Spades, Rank.Two), new Card(Suit.Spades, Rank.Three), new Card(Suit.Spades, Rank.Four) }));
			Assert.True(result.Discarded.Count == 3);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - 3);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(card: new Card(Suit.Spades, Rank.Two), suit: Suit.Hearts));
			Assert.True(result.Discarded.Count == Constant.StandardSuitSize + 1);
			Assert.True(result.Pile.Count == Constant.StandardDeckSize - (Constant.StandardSuitSize + 1));

			deck = ImmutableCardSet.MakeStandardDeck();
			var original = deck.Duplicate();
			result = deck.Discard(new DiscardRequest(skip: 1));
			Assert.Count(result.Pile, Constant.StandardDeckSize / 2);
			Assert.Count(result.Discarded, Constant.StandardDeckSize / 2);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			for (int i = 0; i < Constant.StandardDeckSize / 2; i++) {
				Assert.Equal(result.Discarded[i], original[i * 2]);
				Assert.Equal(result.Pile[i], original[i * 2 + 1]);
			}

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(skip: 2));
			int chunks = Constant.StandardDeckSize / 3;
			Assert.Count(result.Pile, chunks * 2);
			Assert.Count(result.Discarded, Constant.StandardDeckSize - chunks * 2);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			for (int i = 0; i < Constant.StandardDeckSize / 3; i++) {
				Assert.Equal(result.Discarded[i], original[i * 3]);
				Assert.Equal(result.Pile[i * 2], original[i * 3 + 1]);
				Assert.Equal(result.Pile[i * 2 + 1], original[i * 3 + 2]);
			}

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(offset: 1));
			Assert.Count(result.Pile, 1);
			Assert.Count(result.Discarded, Constant.StandardDeckSize - 1);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			Assert.Equal(original.First, result.Pile.First);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(offset: 1, skip: 1));
			Assert.Count(result.Pile, Constant.StandardDeckSize / 2);
			Assert.Count(result.Discarded, Constant.StandardDeckSize / 2);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			for (int i = 0; i < Constant.StandardDeckSize / 2; i++) {
				Assert.Equal(result.Pile[i], original[i * 2]);
				Assert.Equal(result.Discarded[i], original[i * 2 + 1]);
			}

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(direction: Direction.LastToFirst, offset: 1));
			Assert.Count(result.Pile, 1);
			Assert.Count(result.Discarded, Constant.StandardDeckSize - 1);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			Assert.Equal(original.Last, result.Pile.First);

			deck = ImmutableCardSet.MakeStandardDeck();
			result = deck.Discard(new DiscardRequest(direction: Direction.LastToFirst, offset: 1, skip: 1));
			Assert.Count(result.Pile, Constant.StandardDeckSize / 2);
			Assert.Count(result.Discarded, Constant.StandardDeckSize / 2);
			Assert.OverlapCount(result.Pile, result.Discarded, 0);
			for (int begin = Constant.StandardDeckSize / 2 - 1, i = begin; i >= 0; i--) {
				Assert.Equal(result.Pile[i], original[i * 2 + 1]);
				Assert.Equal(result.Discarded[begin - i], original[i * 2]);
			}
		}

		[TestMethod]
		public void Split() {
			var deck = ImmutableCardSet.MakeStandardDeck();
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
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(-1));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(0));
			Assert.ThrowsExact<ArgumentException>(() => deck.Distribute(1));

			ImmutableCardSet[] split = null;
			
			// Test distribution ordering
			split = deck.Distribute(new DistributeRequest(2, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split[0].First == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].First == new Card(Suit.Spades, Rank.Three));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(3, distributionPolicy: DistributionPolicy.Alternating, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split[0].First == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].First == new Card(Suit.Spades, Rank.Three));
			Assert.True(split[2].First == new Card(Suit.Spades, Rank.Four));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(2, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split[0].First == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].First == new Card(Suit.Hearts, Rank.Two));
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(3, distributionPolicy: DistributionPolicy.Heap, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split[0].First == new Card(Suit.Spades, Rank.Two));
			Assert.True(split[1].First == new Card(Suit.Clubs, Rank.Six));
			Assert.True(split[2].First == new Card(Suit.Hearts, Rank.Ten));
			Assert.True(deck.Count == 52);

			// Test remainder distribution
			split = deck.Distribute(new DistributeRequest(2, remainderPolicy: RemainderPolicy.Distribute)).Piles.ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);
			
			split = deck.Distribute(new DistributeRequest(2, remainderPolicy: RemainderPolicy.SeparatePile)).Piles.ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(2, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split.Length == 2);
			Assert.True(split[0].Count == 26);
			Assert.True(split[1].Count == 26);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.Distribute)).Piles.ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 18);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.SeparatePile)).Piles.ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(split[3].Count == 1);
			Assert.True(deck.Count == 52);

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 17);
			Assert.True(deck.Count == 52);

			var modifiedDeck = CardSet.MakeStandardDeck();
			modifiedDeck.Draw(2);
			deck = modifiedDeck.AsImmutable();

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.Distribute)).Piles.ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 17);
			Assert.True(split[1].Count == 17);
			Assert.True(split[2].Count == 16);
			Assert.True(deck.Count == 50);

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.SeparatePile)).Piles.ToArray();
			Assert.True(split.Length == 4);
			Assert.True(split[0].Count == 16);
			Assert.True(split[1].Count == 16);
			Assert.True(split[2].Count == 16);
			Assert.True(split[3].Count == 2);
			Assert.True(deck.Count == 50);

			split = deck.Distribute(new DistributeRequest(3, remainderPolicy: RemainderPolicy.NoRemainder)).Piles.ToArray();
			Assert.True(split.Length == 3);
			Assert.True(split[0].Count == 16);
			Assert.True(split[1].Count == 16);
			Assert.True(split[2].Count == 16);
			Assert.True(deck.Count == 50);
		}

		[TestMethod]
		public void Reverse() {
			var deck = new ImmutableCardSet();
			Assert.DoesNotThrow(() => deck.Reverse());
			deck = ImmutableCardSet.MakeStandardDeck();
			Assert.DoesNotThrow(() => deck.Reverse());

			var deck2 = deck.Reverse();
			Assert.True(deck.First == deck2.Last);
			Assert.True(deck.Last == deck2.First);
		}

		[TestMethod]
		public void Contains() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.False(deck.Contains(Card.Joker));

			var card = new Card(Suit.Spades, Rank.Ace);
			Assert.True(deck.Contains(card));
		}

		[TestMethod]
		public void GetRandom() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentNullException>(() => deck.GetRandom(null));

			int threshold = 0;
			Card card1 = null;
			Card card2 = null;
			do {
				card1 = deck.GetRandom();
				card2 = deck.GetRandom();
				if (card1 == card2) threshold++;
			} while (card1 == card2);

			// The majority of runs will have no threshold, but it's possible to get a collision
			// You have a 1/52 chance of getting any specific card and another 1/52 chance of getting the same
			// The odds of getting 1 collision are 1/2704
			// The odds of getting 2 collisions are 1/7311616
			// The odds of getting 3 collisions are 1/19770609664
			Assert.True(threshold < 3);
		}

		[TestMethod]
		public void Sort() {
			var deck = ImmutableCardSet.MakeStandardDeck();
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Sort(null as Func<Card, int>));
			Assert.ThrowsExact<ArgumentNullException>(() => deck.Sort(null as Func<Card, int, int>));

			deck = deck.Shuffle(3);

			var sorted = deck.Sort(c => ((int)c.Suit * 20) + (int)c.Rank);
			Assert.NotEqual(sorted, deck);

			sorted = deck.Sort((c, i) => ((int)c.Suit * 20) + (int)c.Rank);
			Assert.NotEqual(sorted, deck);
		}

		[TestMethod]
		public void Duplicate() {
			var deck = new ImmutableCardSet();
			Assert.DoesNotThrow(() => deck.Duplicate());
			deck = ImmutableCardSet.MakeStandardDeck();
			Assert.DoesNotThrow(() => deck.Duplicate());

			var altered = deck.Duplicate();
			Assert.NotSame(altered, deck);
			Assert.Equal(altered, deck);
		}
	}
}
