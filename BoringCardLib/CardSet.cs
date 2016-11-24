using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class CardSet : IEnumerable<Card>, IEnumerable {
		private List<Card> mCards = new List<Card>();

		public bool AnyCards { get { return Count > 0; } }
		public int Count { get { return mCards.Count; } }
		public Card First {
			get {
				if (Count == 0) throw new InvalidOperationException();
				return mCards[0];
			}
		}
		public Card Last {
			get {
				if (Count == 0) throw new InvalidOperationException();
				return mCards[mCards.Count - 1];
			}
		}
		public Card this[int index] { get { return mCards[index]; } }

		public CardSet(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards.AddRange(cards);
		}
		public CardSet(params Card[] cards) : this(cards as IEnumerable<Card>) { }

		public static CardSet MakeStandardDeck(int jokerCount = 0, Func<Card, Card> initializeValues = null) {
			jokerCount.ThrowIfNegative(nameof(jokerCount));
			var cards = new List<Card>();
			foreach (var suit in EnumExt<Suit>.GetValues()) {
				if (suit == Suit.Joker) continue;
				cards.AddRange(MakeFullSuit(suit, initializeValues: initializeValues));
			}
			if (jokerCount > 0) {
				if (initializeValues == null) initializeValues = c => c;
				for (int i = 0; i < jokerCount; i++) {
					cards.Add(initializeValues(Card.Joker.Duplicate()));
				}
			}
			return new CardSet(cards);
		}

		public static CardSet MakeFullSuit(Suit suit, Func<Card, Card> initializeValues = null) {
			suit.ThrowIfInvalid(nameof(suit));
			if (suit == Suit.Joker) throw new ArgumentException("Must not be a joker", nameof(suit));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			foreach (var rank in EnumExt<Rank>.GetValues()) {
				if (rank == Rank.Joker) continue;
				cards.Add(initializeValues(new Card(suit, rank)));
			}
			return new CardSet(cards);
		}

		public void Prepend(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			for (int i = cards.Count() - 1; i >= 0; i--) {
				mCards.Insert(0, cards.ElementAt(i));
			}
		}

		public void Prepend(params Card[] cards) {
			Prepend(cards as IEnumerable<Card>);
		}

		public void Append(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			mCards.AddRange(cards.ToList());
		}

		public void Append(params Card[] cards) {
			Append(cards as IEnumerable<Card>);
		}

		public void Insert(int index, IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			index.ThrowIfOutsideRange(0, Count, nameof(index));
			mCards =
				mCards.Take(index).
				Concat(cards).
				Concat(mCards.Skip(index)).
				ToList();
		}

		public void Insert(int index, params Card[] cards) {
			Insert(index, cards as IEnumerable<Card>);
		}

		public void Shuffle(int times = 1) {
			Shuffle(new DefaultRandomSource(), times: times);
		}

		public void Shuffle(IRandomSource sampler, int times = 1) {
			sampler.ThrowIfNull(nameof(sampler));
			times.ThrowIfZeroOrLess(nameof(times));

			int n = mCards.Count;
			do {
				for (int i = 0; i < n; i++) {
					var temp = mCards[i];
					int r = i + sampler.SampleInt32(n - i);
					mCards[i] = mCards[r];
					mCards[r] = temp;
				}
			} while (--times > 0);
		}

		public Card GetRandom() {
			return GetRandom(new DefaultRandomSource());
		}

		public Card GetRandom(IRandomSource sampler) {
			sampler.ThrowIfNull(nameof(sampler));
			if (Count == 0) throw new InvalidOperationException();
			return this[sampler.SampleInt32(Count)];
		}

		public bool Contains(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.Contains(card);
		}
		
		public int IndexOf(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.IndexOf(card);
		}

		public Card Draw() {
			if (mCards.Count == 0) throw new InvalidOperationException();
			var result = mCards[0];
			mCards.RemoveAt(0);
			return result;
		}

		public CardSet Draw(int cards) {
			cards.ThrowIfZeroOrLess(nameof(cards));
			if (cards >= Count) {
				var pile = new CardSet(mCards);
				mCards.Clear();
				return pile;
			}

			var result = mCards.Take(cards).ToArray();
			for (int i = 0; i < cards; i++) {
				mCards.RemoveAt(0);
			}
			return new CardSet(result);
		}

		public ReplaceResult Replace(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		public ReplaceResult Replace(params ReplaceRequest[] requests) {
			requests.ThrowIfNull(nameof(requests));
			return Replace(requests as IEnumerable<ReplaceRequest>);
		}

		public ReplaceResult Replace(IEnumerable<ReplaceRequest> requests) {
			requests.ThrowIfNullOrEmptyOrContainsNulls(nameof(requests));

			var req = requests.ToList();
			var replacements = new List<Card>();
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				var replacement = req.FirstOrDefault(card => c == card.Card);

				if (replacement == null) continue;

				mCards[i] = replacement.Replacement;
				replacements.Add(replacement.Card);
			}

			return new ReplaceResult(
				replacements,
				req.Select(r => r.Card).Except(replacements));
		}

		public DiscardResult Discard(Card card) {
			card.ThrowIfNull(nameof(card));
			return Discard(new DiscardRequest(card: card));
		}

		public DiscardResult Discard(int quantity, Direction direction = Direction.FirstToLast) {
			quantity.ThrowIfZeroOrLess(nameof(quantity));
			direction.ThrowIfInvalid(nameof(direction));
			return Discard(new DiscardRequest(direction: direction, quantity: quantity));
		}

		public DiscardResult Discard(DiscardRequest request) {
			request.ThrowIfNull(nameof(request));
			bool hasQuantity = request.Quantity != null;
			bool hasSkip = request.Skip > 0;
			bool hasOffset = request.Offset > 0;
			if (hasOffset && request.Offset >= Count) throw new ArgumentException($"Must have an {nameof(DiscardRequest.Offset)} of less than the count", nameof(request));
			bool hasRank = request.Ranks.Any();
			bool hasSuit = request.Suits.Any();
			bool hasValues = request.Values.Any() || request.NullValues;
			bool hasSpecificCards = request.Cards.Any();
			bool hasSpecificCardsOnly = hasSpecificCards && !hasValues && !hasSuit && !hasRank;
			int quantity = request.Quantity.GetValueOrDefault(-1);
			var suits = request.Suits;
			var ranks = request.Ranks;
			var cards = request.Cards;
			var values = request.Values;
			var result = new List<Card>();

			if (hasSkip) {
				if (hasQuantity && quantity >= Count) {
					int i = request.Offset;
					do {
						result.Add(mCards[i]);
						mCards.RemoveAt(i);
						i += request.Skip;
					} while (i < mCards.Count);
				}
				else {
					int discarded = 0;

					// This will ensure that we discard everything possible
					quantity = hasQuantity ?
						quantity :
						Count;

					switch (request.Direction) {
						case Direction.FirstToLast: {
							int i = request.Offset;
							do {
								result.Add(mCards[i]);
								mCards.RemoveAt(i);
								i += request.Skip;
								discarded++;
							} while (i < mCards.Count && discarded < quantity);
							break;
						}
						case Direction.LastToFirst: {
							int i = mCards.Count - 1 - request.Offset;
							do {
								result.Add(mCards[i]);
								mCards.RemoveAt(i);
								i -= (request.Skip + 1);
								discarded++;
							} while (i >= 0 && discarded < quantity);
							break;
						}
						default: throw new NotImplementedException();
					}
				}
			}
			else if (!(hasRank || hasSuit || hasSpecificCards || hasValues)) {
				if (!hasQuantity || (quantity >= Count && hasOffset)) {
					switch (request.Direction) {
						case Direction.FirstToLast:
							result.AddRange(mCards.Skip(request.Offset));
							mCards.RemoveRange(request.Offset, mCards.Count - request.Offset);
							break;
						case Direction.LastToFirst:
							result.AddRange(mCards.Take(mCards.Count - request.Offset));
							mCards.RemoveRange(0, mCards.Count - request.Offset);
							break;
						default: throw new NotImplementedException();
					}
				}
				else if (quantity >= Count) {
					result.AddRange(mCards);
					mCards.Clear();
				}
				else {
					int startPoint = -1;
					switch (request.Direction) {
						case Direction.FirstToLast: startPoint = request.Offset; break;
						case Direction.LastToFirst: startPoint = mCards.Count - request.Offset - quantity; break;
						default: throw new NotImplementedException();
					}
					result.AddRange(mCards.Skip(startPoint).Take(quantity));
					mCards.RemoveRange(startPoint, quantity);
				}
			}
			else {
				Func<Card, bool> isDiscard = card => {
					if (hasSpecificCardsOnly) return cards.Contains(card);
					if (hasSpecificCards && cards.Contains(card)) return true;

					bool rankValid = !hasRank || ranks.Contains(card.Rank);
					bool suitValid = !hasSuit || suits.Contains(card.Suit);
					bool valueValid =
						(!hasValues || (card.Value != null && values.Contains(card.Value.GetValueOrDefault()))) ||
						(request.NullValues && card.Value == null);
					return rankValid && suitValid && valueValid;
				};

				switch (request.Direction) {
					case Direction.FirstToLast:
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
					case Direction.LastToFirst:
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

			return new DiscardResult(result);
		}

		public DistributeResult Distribute(int numberOfPiles) {
			return Distribute(new DistributeRequest(numberOfPiles));
		}

		public DistributeResult Distribute(DistributeRequest request) {
			request.ThrowIfNull(nameof(request));

			int numberOfPiles = request.Piles;
			var groups = new List<CardSet>(numberOfPiles + 1);
			int remainder = Count % numberOfPiles;
			switch (request.Distribution) {
				case DistributionPolicy.Alternating:
					for (int i = 0; i < numberOfPiles; i++) {
						groups.Add(new CardSet());
					}
					if (remainder != 0 && request.Remainder == RemainderPolicy.SeparatePile) {
						groups.Add(new CardSet());
					}

					int maxCards = Count - remainder;
					for (int i = 0; i < Count; i++) {
						int group = i % numberOfPiles;
						if (i >= maxCards) {
							bool done = false;
							switch (request.Remainder) {
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
					switch (request.Remainder) {
						case RemainderPolicy.Distribute:
							int lastSequence = numberOfPiles - 1;
							for (int i = 0; i < numberOfPiles; i++) {
								if (i == lastSequence) {
									groups.Add(new CardSet(mCards.Skip(basePoint)));
								}
								else {
									int add = i < remainder ? 1 : 0;
									groups.Add(new CardSet(mCards.Skip(basePoint).Take(chunkPoint + add)));
									basePoint += chunkPoint + add;
								}
							}
							break;
						case RemainderPolicy.SeparatePile:
						case RemainderPolicy.NoRemainder:
							for (int i = 0; i < numberOfPiles; i++) {
								groups.Add(new CardSet(mCards.Skip(basePoint).Take(chunkPoint)));
								basePoint += chunkPoint;
							}
							if (remainder != 0 && request.Remainder != RemainderPolicy.NoRemainder) {
								groups.Add(new CardSet(mCards.Skip(basePoint)));
							}
							break;
						default: throw new NotImplementedException();
					}
					break;
				default: throw new NotImplementedException();
			}

			if (request.Remainder != RemainderPolicy.NoRemainder || remainder == 0) {
				mCards.Clear();
			}
			else {
				mCards = mCards.Skip(Count - remainder).ToArray().ToList(); 
			}

			return new DistributeResult(groups.AsReadOnly());
		}

		public SplitResult Split() {
			return Split(Count / 2);
		}

		public SplitResult Split(int splitPoint) {
			splitPoint.ThrowIfNegative(nameof(splitPoint));
			if (Count < 2) throw new InvalidOperationException("Must have at least 2 items to perform a split");

			var result = new SplitResult(
				new CardSet(mCards.Take(splitPoint).ToArray()),
				new CardSet(mCards.Skip(splitPoint).ToArray()));
			mCards.Clear();
			return result;
		}

		public void Sort<TResult>(Func<Card, TResult> sort) {
			sort.ThrowIfNull(nameof(sort));
			mCards = mCards.
				OrderBy(sort).
				ToList();
		}

		public void Sort<TResult>(Func<Card, int, TResult> sort) {
			sort.ThrowIfNull(nameof(sort));
			mCards = mCards.
				Select((c, i) => new { Index = i, Card = c }).
				OrderBy(c => sort(c.Card, c.Index)).
				Select(c => c.Card).
				ToList();
		}

		public void Reverse() {
			mCards = mCards.Reverse<Card>().ToList();
		}

		public CardSet Duplicate() {
			return new CardSet(mCards);
		}

		public ImmutableCardSet AsImmutable() {
			return new ImmutableCardSet(mCards.ToArray());
		}

		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}