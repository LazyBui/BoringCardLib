using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableCardSet : IEnumerable<Card>, IEnumerable {
		private CardSet mCards = null;

		public int Count { get { return mCards.Count; } }
		public Card First { get { return mCards.First; } }
		public Card Last { get { return mCards.Last; } }
		public Card this[int index] { get { return mCards[index]; } }

		public ImmutableCardSet(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards = new CardSet(cards);		
		}
		public ImmutableCardSet(params Card[] cards) : this(cards as IEnumerable<Card>) { }

		public static ImmutableCardSet MakeStandardDeck(int jokerCount = 0, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeStandardDeck(
				jokerCount: jokerCount,
				initializeValues: initializeValues).
				AsImmutable();
		}

		public static ImmutableCardSet MakeFullSuit(Suit suit, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeFullSuit(
				suit,
				initializeValues: initializeValues).
				AsImmutable();
		}

		public ImmutableCardSet Prepend(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			return new ImmutableCardSet(cards.Concat(mCards));
		}

		public ImmutableCardSet Prepend(params Card[] cards) {
			return Prepend(cards as IEnumerable<Card>);
		}

		public ImmutableCardSet Append(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			return new ImmutableCardSet(mCards.Concat(cards));
		}

		public ImmutableCardSet Append(params Card[] cards) {
			return Append(cards as IEnumerable<Card>);
		}

		public ImmutableCardSet Insert(int index, IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			index.ThrowIfOutsideRange(0, Count - 1, nameof(index));
			return new ImmutableCardSet(mCards.Take(index).
				Concat(cards).
				Concat(mCards.Skip(index)));
		}

		public ImmutableCardSet Insert(int index, params Card[] cards) {
			return Insert(index, cards as IEnumerable<Card>);
		}

		public ImmutableCardSet Shuffle(int times = 1) {
			return Shuffle(new DefaultRandomSource(), times: times);
		}

		public ImmutableCardSet Shuffle(IRandomSource sampler, int times = 1) {
			sampler.ThrowIfNull(nameof(sampler));
			times.ThrowIfZeroOrLess(nameof(times));
			var newGroup = mCards.Duplicate();
			newGroup.Shuffle(sampler, times: times);
			return newGroup.AsImmutable();
		}

		public Card GetRandom() {
			return GetRandom(new DefaultRandomSource());
		}

		public Card GetRandom(IRandomSource sampler) {
			return mCards.GetRandom(sampler);
		}

		public bool Contains(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.Contains(card);
		}

		public int IndexOf(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.IndexOf(card);
		}

		public ImmutableDrawResult Draw() {
			return Draw(1);
		}

		public ImmutableDrawResult Draw(int cards) {
			cards.ThrowIfZeroOrLess(nameof(cards));
			if (cards >= Count) {
				return new ImmutableDrawResult(
					new ImmutableCardSet(),
					Duplicate());
			}

			return new ImmutableDrawResult(
				new ImmutableCardSet(mCards.Skip(cards).ToArray()),
				new ImmutableCardSet(mCards.Take(cards).ToArray()));
		}

		public ImmutableReplaceResult Replace(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		public ImmutableReplaceResult Replace(params ReplaceRequest[] requests) {
			requests.ThrowIfNull(nameof(requests));
			return Replace(requests as IEnumerable<ReplaceRequest>);
		}

		public ImmutableReplaceResult Replace(IEnumerable<ReplaceRequest> requests) {
			requests.ThrowIfNullOrEmptyOrContainsNulls(nameof(requests));

			var deck = mCards.Duplicate();
			var result = deck.Replace(requests);
			return new ImmutableReplaceResult(
				deck,
				result.Replaced,
				result.NotFound);
		}

		public ImmutableDiscardResult Discard(Card card) {
			card.ThrowIfNull(nameof(card));
			return Discard(new DiscardRequest(card: card));
		}

		public ImmutableDiscardResult Discard(int quantity, Direction direction = Direction.FirstToLast) {
			quantity.ThrowIfZeroOrLess(nameof(quantity));
			direction.ThrowIfInvalid(nameof(direction));
			return Discard(new DiscardRequest(direction: direction, quantity: quantity));
		}

		public ImmutableDiscardResult Discard(DiscardRequest request) {
			request.ThrowIfNull(nameof(request));
			var deck = mCards.Duplicate();
			var result = deck.Discard(request);
			return new ImmutableDiscardResult(
				deck.AsImmutable(),
				result.Cards);
		}

		public ImmutableDistributeResult Distribute(int numberOfPiles) {
			return Distribute(new DistributeRequest(numberOfPiles));
		}

		public ImmutableDistributeResult Distribute(DistributeRequest request) {
			return new ImmutableDistributeResult(mCards.
				Duplicate().
				Distribute(request).Piles);
		}

		public ImmutableSplitResult Split() {
			return new ImmutableSplitResult(mCards.Duplicate().Split());
		}

		public ImmutableSplitResult Split(int splitPoint) {
			return new ImmutableSplitResult(mCards.Duplicate().Split(splitPoint));
		}

		public ImmutableCardSet Reverse() {
			var newGroup = mCards.Duplicate();
			newGroup.Reverse();
			return new ImmutableCardSet(newGroup);
		}

		public ImmutableCardSet Duplicate() {
			return new ImmutableCardSet(mCards.ToArray());
		}

		public ImmutableCardSet Sort<TResult>(Func<Card, TResult> sort) {
			var @new = mCards.Duplicate();
			@new.Sort(sort);
			return @new.AsImmutable();
		}

		public ImmutableCardSet Sort<TResult>(Func<Card, int, TResult> sort) {
			var @new = mCards.Duplicate();
			@new.Sort(sort);
			return @new.AsImmutable();
		}

		public CardSet AsMutable() {
			return new CardSet(mCards.ToArray());
		}

		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}