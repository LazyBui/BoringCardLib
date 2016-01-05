using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableCardGroup : IEnumerable<Card>, IEnumerable {
		private CardGroup mCards = null;

		public int Count { get { return mCards.Count; } }
		public Card Top { get { return mCards.Top; } }
		public Card Bottom { get { return mCards.Bottom; } }

		public ImmutableCardGroup(IEnumerable<Card> cards) : this(cards.ToArray()) { }
		public ImmutableCardGroup(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Any(c => c == null)) throw new ArgumentException("Must have no nulls", nameof(cards));
			mCards = new CardGroup(cards);
		}

		public ImmutableCardGroup Prepend(IEnumerable<Card> cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			return Prepend(cards.ToArray());
		}

		public ImmutableCardGroup Prepend(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Length == 0) throw new ArgumentException("Must have elements", nameof(cards));
			return new ImmutableCardGroup(cards.Concat(mCards));
		}

		public ImmutableCardGroup Append(IEnumerable<Card> cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			return Append(cards.ToArray());
		}

		public ImmutableCardGroup Append(params Card[] cards) {
			if (cards == null) throw new ArgumentNullException(nameof(cards));
			if (cards.Length == 0) throw new ArgumentException("Must have elements", nameof(cards));
			return new ImmutableCardGroup(mCards.Concat(cards));
		}

		public ImmutableCardGroup Shuffle(int times = 1) {
			return Shuffle(new DefaultRandomSource(), times: times);
		}

		public ImmutableCardGroup Shuffle(IRandomSource sampler, int times = 1) {
			if (sampler == null) throw new ArgumentNullException(nameof(sampler));
			if (times <= 0) throw new ArgumentException("Must be > 0", nameof(times));
			var newGroup = mCards.Duplicate();
			newGroup.Shuffle(sampler, times: times);
			return newGroup.AsImmutable();
		}

		public ImmutableDrawResult Draw() {
			return Draw(1);
		}

		public ImmutableDrawResult Draw(int cards) {
			if (cards <= 0) throw new ArgumentException("Must be > 0", nameof(cards));
			if (cards >= Count) {
				return new ImmutableDrawResult(
					new ImmutableCardGroup(),
					Duplicate());
			}

			return new ImmutableDrawResult(
				new ImmutableCardGroup(mCards.Skip(cards).ToArray()),
				new ImmutableCardGroup(mCards.Take(cards).ToArray()));
		}

		public IEnumerable<ImmutableCardGroup> Distribute(int numberOfPiles, DistributionPolicy distributionPolicy = DistributionPolicy.Alternating, RemainderPolicy remainderPolicy = RemainderPolicy.Distribute) {
			return mCards.Duplicate().Distribute(
				numberOfPiles,
				distributionPolicy: distributionPolicy,
				remainderPolicy: remainderPolicy).
				Select(g => g.AsImmutable());
		}

		public ImmutableSplitResult Split() {
			return new ImmutableSplitResult(mCards.Duplicate().Split());
		}

		public ImmutableSplitResult Split(int splitPoint) {
			return new ImmutableSplitResult(mCards.Duplicate().Split(splitPoint));
		}

		public ImmutableCardGroup Reverse() {
			var newGroup = mCards.Duplicate();
			newGroup.Reverse();
			return new ImmutableCardGroup(newGroup);
		}

		public ImmutableCardGroup Duplicate() {
			return new ImmutableCardGroup(mCards.ToArray());
		}

		public CardGroup AsMutable() {
			return new CardGroup(mCards.ToArray());
		}

		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}