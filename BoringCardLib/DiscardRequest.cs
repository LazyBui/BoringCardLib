using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DiscardRequest {
		public int? Quantity { get; private set; }
		public IEnumerable<int> Values { get; private set; }
		public bool NullValues { get; private set; }
		public IEnumerable<Card> Cards { get; private set; }
		public IEnumerable<Suit> Suits { get; private set; }
		public IEnumerable<Rank> Ranks { get; private set; }
		public Direction Direction { get; private set; }

		public DiscardRequest(
			Direction direction = Direction.FromTop,
			int? quantity = null,
			int? value = null,
			IEnumerable<int> values = null,
			bool? nullValues = null,
			Card card = null,
			IEnumerable<Card> cards = null,
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
			if (values != null) {
				if (values.Count() == 0) throw new ArgumentException("Must have elements", nameof(values));
				if (value != null) throw new ArgumentException($"Must not specify both {nameof(value)} and {nameof(values)}");
			}
			if (cards != null) {
				if (cards.Count() == 0) throw new ArgumentException("Must have elements", nameof(cards));
				if (card != null) throw new ArgumentException($"Must not specify both {nameof(card)} and {nameof(cards)}");
				if (cards.Any(c => c == null)) throw new ArgumentException("Must not contain nulls", nameof(cards));
			}
			if (suit != null) suit.ThrowIfInvalid(nameof(suit));
			if (rank != null) rank.ThrowIfInvalid(nameof(rank));

			if (quantity == null && nullValues == null &&
				suit == null && suits == null && 
				rank == null && ranks == null &&
				card == null && cards == null && 
				value == null && values == null)
			{
				throw new ArgumentException("Must have at least one discard criterion");
			}

			Direction = direction;
			Quantity = quantity;
			if (nullValues != null) NullValues = nullValues.GetValueOrDefault();

			if (suits != null) Suits = suits;
			else if (suit != null) Suits = new[] { suit.GetValueOrDefault() };
			else Suits = new Suit[0];

			if (ranks != null) Ranks = ranks;
			else if (rank != null) Ranks = new[] { rank.GetValueOrDefault() };
			else Ranks = new Rank[0];

			if (cards != null) Cards = cards;
			else if (card != null) Cards = new[] { card };
			else Cards = new Card[0];

			if (values != null) Values = values;
			else if (value != null) Values = new[] { value.GetValueOrDefault() };
			else Values = new int[0];
		}
	}
}