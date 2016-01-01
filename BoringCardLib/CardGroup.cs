using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class CardGroup : IEnumerable<Card> {
		private List<Card> mCards = new List<Card>();

		public int Count { get { return mCards.Count; } }
		public Card Top {
			get {
				if (Count == 0) throw new InvalidOperationException();
				return mCards[0];
			}
		}
		public Card Bottom {
			get {
				if (Count == 0) throw new InvalidOperationException();
				return mCards[mCards.Count - 1];
			}
		}

		public CardGroup(IEnumerable<Card> cards) : this(cards.ToArray()) { }
		public CardGroup(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Any(c => c == null)) throw new ArgumentException("Must have no nulls", nameof(cards));
			mCards.AddRange(cards);
		}

		public static CardGroup MakeStandardDeck(bool includeJokers = false, Func<Card, Card> initializeValues = null) {
			var cards = new List<Card>();
			foreach (var suit in EnumExt<Suit>.GetValues()) {
				if (suit == Suit.Joker) continue;
				cards.AddRange(MakeFullSuit(suit, initializeValues: initializeValues));
			}
			if (includeJokers) {
				if (initializeValues == null) initializeValues = c => c;
				cards.AddRange(new[] {
					initializeValues(new Card(Suit.Joker, Rank.Joker)),
					initializeValues(new Card(Suit.Joker, Rank.Joker)),
				});
			}
			return new CardGroup(cards);
		}

		public static CardGroup MakeFullSuit(Suit suit, Func<Card, Card> initializeValues = null) {
			suit.ThrowIfInvalid(nameof(suit));
			if (suit == Suit.Joker) throw new ArgumentException("Must not be a joker", nameof(suit));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			foreach (var rank in EnumExt<Rank>.GetValues()) {
				if (rank == Rank.Joker) continue;
				cards.Add(initializeValues(new Card(suit, rank)));
			}
			return new CardGroup(cards);
		}

		public void Prepend(IEnumerable<Card> cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			Prepend(cards.ToArray());
		}

		public void Prepend(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Length == 0) throw new ArgumentException("Must have elements", nameof(cards));
			for (int i = cards.Length - 1; i >= 0; i--) {
				mCards.Insert(0, cards[i]);
			}
		}

		public void Append(IEnumerable<Card> cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			Append(cards.ToArray());
		}

		public void Append(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Length == 0) throw new ArgumentException("Must have elements", nameof(cards));
			mCards.AddRange(cards.ToList());
		}

		public Card Draw() {
			if (mCards.Count == 0) throw new InvalidOperationException();
			var result = mCards[0];
			mCards.RemoveAt(0);
			return result;
		}

		public CardGroup Shuffle(int times = 1) {
			return Shuffle(new DefaultRandomSource(), times: times);
		}

		public CardGroup Shuffle(IRandomSource sampler, int times = 1) {
			if (sampler == null) throw new ArgumentNullException(nameof(sampler));
			if (times <= 0) throw new ArgumentException("Must be > 0", nameof(times));

			int n = mCards.Count;
			Card[] cards = mCards.ToArray();
			do {
				for (int i = 0; i < n; i++) {
					var temp = cards[i];
					int r = i + sampler.SampleInt32(n - i);
					cards[i] = cards[r];
					cards[r] = temp;
				}
			} while (--times > 0);

			return new CardGroup(cards);
		}

		public CardGroup Draw(int cards) {
			if (cards <= 0) throw new ArgumentException("Must be > 0", nameof(cards));
			if (cards >= Count) {
				var pile = new CardGroup(mCards);
				mCards.Clear();
				return pile;
			}

			var result = mCards.Take(cards).ToArray();
			for (int i = 0; i < cards; i++) {
				mCards.RemoveAt(0);
			}
			return new CardGroup(result);
		}

		public CardStackOperation Discard(Card card) {
			if (card == null) throw new ArgumentNullException(nameof(card));
			CardStackOperation result = CardStackOperation.CardNotPresent;
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				if (c == card) {
					result = CardStackOperation.Performed;
					mCards.RemoveAt(i);
					break;
				}
			}
			return result;
		}

		public CardStackOperation Replace(Card card, Card replacement) {
			if (card == null) throw new ArgumentNullException(nameof(card));
			if (replacement == null) throw new ArgumentNullException(nameof(replacement));
			CardStackOperation result = CardStackOperation.CardNotPresent;
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				if (c == card) {
					result = CardStackOperation.Performed;
					mCards[i] = replacement;
					break;
				}
			}
			return result;
		}

		public CardGroup Discard() {
			return Discard(Count);
		}

		public CardGroup Discard(int quantity, Direction direction = Direction.FromTop) {
			if (quantity <= 0) throw new ArgumentException("Must be > 0", nameof(quantity));
			direction.ThrowIfInvalid(nameof(direction));
			return Discard(new DiscardRequest(direction: direction, quantity: quantity));
		}

		public CardGroup Discard(DiscardRequest request) {
			if (request == null) throw new ArgumentNullException(nameof(request));
			bool hasQuantity = request.Quantity != null;
			bool hasRank = request.Ranks.Any();
			bool hasSuit = request.Suits.Any();
			int quantity = request.Quantity.GetValueOrDefault(-1);
			var suits = request.Suits;
			var ranks = request.Ranks;
			var result = new List<Card>();

			if (hasQuantity && !(hasRank || hasSuit)) {
				if (quantity >= Count) {
					result.AddRange(mCards);
					mCards.Clear();
				}
				else {
					int startPoint = -1;
					switch (request.Direction) {
						case Direction.FromTop: startPoint = 0; break;
						case Direction.FromBottom: startPoint = mCards.Count - quantity; break;
						default: throw new NotImplementedException();
					}
					result.AddRange(mCards.Skip(startPoint).Take(quantity));
					mCards.RemoveRange(startPoint, quantity);
				}
			}
			else {
				Func<Card, bool> isDiscard = card => {
					bool rankValid = !hasRank || ranks.Contains(card.Rank);
					bool suitValid = !hasSuit || suits.Contains(card.Suit);
					return rankValid && suitValid;
				};

				switch (request.Direction) {
					case Direction.FromTop:
						for (int i = 0; i < mCards.Count; i++) {
							var card = mCards[i];
							if (isDiscard(card)) {
								result.Add(card);
								mCards.RemoveAt(i);
								i--;
								if (hasQuantity && result.Count == quantity) break;
							}
						}
						break;
					case Direction.FromBottom:
						for (int i = mCards.Count - 1; i >= 0; i--) {
							var card = mCards[i];
							if (isDiscard(card)) {
								result.Add(card);
								mCards.RemoveAt(i);
								if (hasQuantity && result.Count == quantity) break;
							}
						}
						break;
					default: throw new NotImplementedException();
				}
			}

			return new CardGroup(result);
		}

		public IEnumerable<CardGroup> Distribute(int numberOfPiles, DistributionPolicy distributionPolicy = DistributionPolicy.Alternating, RemainderPolicy remainderPolicy = RemainderPolicy.Distribute) {
			if (numberOfPiles <= 1) throw new ArgumentException("Must be > 1", nameof(numberOfPiles));
			if (numberOfPiles > Count) throw new ArgumentException("Must be <= current stack size", nameof(numberOfPiles));
			distributionPolicy.ThrowIfInvalid(nameof(distributionPolicy));
			remainderPolicy.ThrowIfInvalid(nameof(remainderPolicy));

			var groups = new List<CardGroup>(numberOfPiles + 1);
			int remainder = Count % numberOfPiles;

			switch (distributionPolicy) {
				case DistributionPolicy.Alternating:
					for (int i = 0; i < numberOfPiles; i++) {
						groups.Add(new CardGroup());
					}
					if (remainder != 0 && remainderPolicy == RemainderPolicy.SeparatePile) {
						groups.Add(new CardGroup());
					}

					int maxCards = Count - remainder;
					for (int i = 0; i < Count; i++) {
						int group = i % numberOfPiles;
						if (i >= maxCards) {
							bool done = false;
							switch (remainderPolicy) {
								case RemainderPolicy.Distribute: groups[group].Append(mCards[i]); break;
								case RemainderPolicy.NoRemainder: done = true; break;
								case RemainderPolicy.SeparatePile: groups[numberOfPiles].Append(mCards[i]); break;
								default: throw new NotImplementedException();
							}
							if (done) break;
						}
						else {
							groups[group].Append(mCards[i]);
						}
					}
					break;
				case DistributionPolicy.Heap:
					int chunkPoint = Count / numberOfPiles;
					int basePoint = 0;
					switch (remainderPolicy) {
						case RemainderPolicy.Distribute:
							int lastSequence = numberOfPiles - 1;
							for (int i = 0; i < numberOfPiles; i++) {
								if (i == lastSequence) {
									groups.Add(new CardGroup(mCards.Skip(basePoint)));
								}
								else {
									int add = i < remainder ? 1 : 0;
									groups.Add(new CardGroup(mCards.Skip(basePoint).Take(chunkPoint + add)));
									basePoint += chunkPoint + add;
								}
							}
							break;
						case RemainderPolicy.SeparatePile:
						case RemainderPolicy.NoRemainder:
							for (int i = 0; i < numberOfPiles; i++) {
								groups.Add(new CardGroup(mCards.Skip(basePoint).Take(chunkPoint)));
								basePoint += chunkPoint;
							}
							if (remainder != 0 && remainderPolicy != RemainderPolicy.NoRemainder) {
								groups.Add(new CardGroup(mCards.Skip(basePoint)));
							}
							break;
						default: throw new NotImplementedException();
					}
					break;
				default: throw new NotImplementedException();
			}

			return groups.AsReadOnly();
		}

		public SplitResult Split() {
			return Split(Count / 2);
		}

		public SplitResult Split(int splitPoint) {
			if (splitPoint <= 0) throw new ArgumentException("Must be > 0", nameof(splitPoint));
			return new SplitResult(
				new CardGroup(mCards.Take(splitPoint).ToArray()),
				new CardGroup(mCards.Skip(splitPoint).ToArray()));
		}

		public CardGroup Reverse() {
			return new CardGroup(mCards.Reverse<Card>().ToArray());
		}

		public CardGroup Duplicate() {
			return new CardGroup(mCards.ToArray());
		}

		public ImmutableCardGroup AsImmutable() {
			return new ImmutableCardGroup(mCards.ToArray());
		}

		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}