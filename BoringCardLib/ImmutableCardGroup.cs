using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableCardGroup : IEnumerable<Card> {
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

		public IEnumerable<CardGroup> Distribute(int numberOfPiles, DistributionPolicy distributionPolicy = DistributionPolicy.Alternating, RemainderPolicy remainderPolicy = RemainderPolicy.Distribute) {
			return mCards.Distribute(numberOfPiles, distributionPolicy: distributionPolicy, remainderPolicy: remainderPolicy);
		}

		public SplitResult Split() {
			return mCards.Split();
		}

		public SplitResult Split(int splitPoint) {
			return mCards.Split(splitPoint);
		}

		public ImmutableCardGroup Reverse() {
			return new ImmutableCardGroup(mCards.Reverse<Card>().ToArray());
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

		public static ImmutableCardGroup Shuffle(ImmutableCardGroup group, int times = 1) {
			return Shuffle(new DefaultRandomSource(), group, times: times);
		}

		public static ImmutableCardGroup Shuffle(IRandomSource sampler, ImmutableCardGroup group, int times = 1) {
			if (sampler == null) throw new ArgumentNullException(nameof(sampler));
			if (group == null) throw new ArgumentNullException(nameof(group));
			if (times <= 0) throw new ArgumentException("Must be > 0", nameof(times));

			int n = group.Count;
			Card[] cards = group.ToArray();
			do {
				for (int i = 0; i < n; i++) {
					var temp = cards[i];
					int r = i + sampler.SampleInt32(n - i);
					cards[i] = cards[r];
					cards[r] = temp;
				}
			} while (--times > 0);

			return new ImmutableCardGroup(cards);
		}
	}
}