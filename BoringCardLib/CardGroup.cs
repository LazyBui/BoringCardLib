using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class CardGroup : IEnumerable<Card> {
		private List<Card> mCards = new List<Card>();

		public int Size { get { return mCards.Count; } }
		public Card Top {
			get {
				if (Size == 0) throw new InvalidOperationException();
				return mCards[0];
			}
		}
		public Card Bottom {
			get {
				if (Size == 0) throw new InvalidOperationException();
				return mCards[mCards.Count - 1];
			}
		}

		public CardGroup(IEnumerable<Card> pCards) : this(pCards.ToArray()) { }
		public CardGroup(params Card[] pCards) {
			if (pCards == null) throw new ArgumentNullException(nameof(pCards));
			if (pCards.Any(c => c == null)) throw new ArgumentException("Must have no nulls", nameof(pCards));
			mCards.AddRange(pCards);
		}

		public static CardGroup MakeStandardDeck(bool pIncludeJokers = false) {
			var cards = new List<Card>();
			foreach (var suit in EnumExt<Suit>.GetValues()) {
				if (suit == Suit.Joker) continue;
				cards.AddRange(MakeFullSuit(suit));
			}
			if (pIncludeJokers) {
				cards.AddRange(new[] {
					new Card(Suit.Joker, Rank.Joker),
					new Card(Suit.Joker, Rank.Joker),
				});
			}
			return new CardGroup(cards);
		}

		public static CardGroup MakeFullSuit(Suit pSuit) {
			pSuit.ThrowIfInvalid(nameof(pSuit));
			if (pSuit == Suit.Joker) throw new ArgumentException("Must not be a joker", nameof(pSuit));

			var cards = new List<Card>();
			foreach (var rank in EnumExt<Rank>.GetValues()) {
				if (rank == Rank.Joker) continue;
				cards.Add(new Card(pSuit, rank));
			}
			return new CardGroup(cards);
		}

		public void Prepend(IEnumerable<Card> pCards) {
			if (pCards == null) throw new ArgumentNullException(nameof(pCards));
			Prepend(pCards.ToArray());
		}

		public void Prepend(params Card[] pCards) {
			if (pCards == null) throw new ArgumentNullException(nameof(pCards));
			if (pCards.Length == 0) throw new ArgumentException("Must have elements", nameof(pCards));
			for (int i = pCards.Length - 1; i >= 0; i--) {
				mCards.Insert(0, pCards[i]);
			}
		}

		public void Append(IEnumerable<Card> pCards) {
			if (pCards == null) throw new ArgumentNullException(nameof(pCards));
			Append(pCards.ToArray());
		}

		public void Append(params Card[] pCards) {
			if (pCards == null) throw new ArgumentNullException(nameof(pCards));
			if (pCards.Length == 0) throw new ArgumentException("Must have elements", nameof(pCards));
			mCards.AddRange(pCards.ToList());
		}

		public void Shuffle(int pTimes = 1) {
			if (pTimes <= 0) throw new ArgumentException("Must be > 0", nameof(pTimes));

			int n = mCards.Count;
			var rand = new Sampler();

			do {
				for (int i = 0; i < n; i++) {
					var temp = mCards[i];
					int r = i + rand.SampleInt32(n - i);
					mCards[i] = mCards[r];
					mCards[r] = temp;
				}
			} while (--pTimes > 0);
		}

		public Card Draw() {
			if (mCards.Count == 0) throw new InvalidOperationException();
			var result = mCards[0];
			mCards.RemoveAt(0);
			return result;
		}

		public CardGroup Draw(int pCards) {
			if (pCards <= 0) throw new ArgumentException("Must be > 0", nameof(pCards));
			if (pCards >= Size) {
				var cards = new CardGroup(mCards);
				mCards.Clear();
				return cards;
			}

			var result = mCards.Take(pCards).ToArray();
			for (int i = 0; i < pCards; i++) {
				mCards.RemoveAt(0);
			}
			return new CardGroup(result);
		}

		public CardStackOperation Discard(Card pCard) {
			if (pCard == null) throw new ArgumentNullException(nameof(pCard));
			CardStackOperation result = CardStackOperation.CardNotPresent;
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				if (c == pCard) {
					result = CardStackOperation.Performed;
					mCards.RemoveAt(i);
					break;
				}
			}
			return result;
		}

		public CardStackOperation Replace(Card pCard, Card pNewCard) {
			if (pCard == null) throw new ArgumentNullException(nameof(pCard));
			if (pNewCard == null) throw new ArgumentNullException(nameof(pNewCard));
			CardStackOperation result = CardStackOperation.CardNotPresent;
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				if (c == pCard) {
					result = CardStackOperation.Performed;
					mCards[i] = pNewCard;
					break;
				}
			}
			return result;
		}

		public CardGroup Discard(DiscardRequest pRequest) {
			if (pRequest == null) throw new ArgumentNullException(nameof(pRequest));
			bool hasQuantity = pRequest.Quantity != null;
			bool hasRank = pRequest.Ranks.Any();
			bool hasSuit = pRequest.Suits.Any();
			int quantity = pRequest.Quantity.GetValueOrDefault(-1);
			var suits = pRequest.Suits;
			var ranks = pRequest.Ranks;
			var result = new List<Card>();

			if (hasQuantity && !(hasRank || hasSuit)) {
				if (quantity >= Size) {
					result.AddRange(mCards);
					mCards.Clear();
				}
				else {
					int startPoint = -1;
					switch (pRequest.Direction) {
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

				switch (pRequest.Direction) {
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

		public IEnumerable<CardGroup> Distribute(int pNumberOfPiles) {
			if (pNumberOfPiles <= 1) throw new ArgumentException("Must be > 1", nameof(pNumberOfPiles));
			if (pNumberOfPiles > Size) throw new ArgumentException("Must be <= current stack size", nameof(pNumberOfPiles));

			var groups = new List<CardGroup>();
			int chunkPoint = Size / pNumberOfPiles;
			int remainder = Size % pNumberOfPiles;
			int basePoint = 0;

			int lastSequence = pNumberOfPiles - 1;
			for (int i = 0; i < pNumberOfPiles; i++) {
				if (i == lastSequence) {
					groups.Add(new CardGroup(mCards.Skip(basePoint)));
				}
				else {
					int add = i < remainder ? 1 : 0;
					groups.Add(new CardGroup(mCards.Skip(basePoint).Take(chunkPoint + add)));
					basePoint += chunkPoint + add;
				}
			}

			return groups.AsReadOnly();
		}

		public SplitResult Split() {
			return Split(Size / 2);
		}

		public SplitResult Split(int pSplitPoint) {
			if (pSplitPoint <= 0) throw new ArgumentException("Must be > 0", nameof(pSplitPoint));
			return new SplitResult(
				new CardGroup(mCards.Take(pSplitPoint).ToArray()),
				new CardGroup(mCards.Skip(pSplitPoint).ToArray()));
		}

		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}