using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DiscardRequest {
		public int? Quantity { get; private set; }
		public IEnumerable<Suit> Suits { get; private set; }
		public IEnumerable<Rank> Ranks { get; private set; }
		public Direction Direction { get; private set; }

		public DiscardRequest(
			Direction direction = Direction.FromTop,
			int? quantity = null,
			Suit? suit = null,
			IEnumerable<Suit> suits = null,
			Rank? rank = null,
			IEnumerable<Rank> ranks = null)
		{
			direction.ThrowIfInvalid(nameof(direction));
			if (quantity != null) {
				if (quantity <= 0) throw new ArgumentException("Must be > 0", nameof(quantity));
			}
			if (suits != null) {
				if (suits.Count() == 0) throw new ArgumentException("Must have elements", nameof(suits));
				if (suit != null) throw new ArgumentException($"Must not specify both {nameof(suit)} and {nameof(suits)}");
				foreach (var s in suits) {
					s.ThrowIfInvalid(nameof(suits));
				}
			}
			if (ranks != null) {
				if (ranks.Count() == 0) throw new ArgumentException("Must have elements", nameof(ranks));
				if (rank != null) throw new ArgumentException($"Must not specify both {nameof(rank)} and {nameof(ranks)}");
				foreach (var r in ranks) {
					r.ThrowIfInvalid(nameof(ranks));
				}
			}
			suit.ThrowIfInvalid(nameof(suit));
			rank.ThrowIfInvalid(nameof(rank));

			if (quantity == null && suit == null && rank == null && suits == null && ranks == null) {
				throw new ArgumentException("Must have at least one discard criterion");
			}

			Direction = direction;
			Quantity = quantity;
			if (suits != null) Suits = suits;
			else if (suit != null) Suits = new[] { suit.GetValueOrDefault() };
			else Suits = new Suit[0];
			if (ranks != null) Ranks = ranks;
			else if (rank != null) Ranks = new[] { rank.GetValueOrDefault() };
			else Ranks = new Rank[0];
		}
	}
}