using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	public sealed class DiscardRequest {
		public int? Quantity { get; private set; }
		public int Skip { get; private set; }
		public int Offset { get; private set; }
		public IEnumerable<int> Values { get; private set; }
		public bool NullValues { get; private set; }
		public IEnumerable<Card> Cards { get; private set; }
		public IEnumerable<Suit> Suits { get; private set; }
		public IEnumerable<Rank> Ranks { get; private set; }
		public Direction Direction { get; private set; }

		[DebuggerStepThrough]
		public DiscardRequest(
			Direction direction = Direction.FirstToLast,
			int? quantity = null,
			int? skip = null,
			int? value = null,
			int? offset = null,
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
				quantity.ThrowIfZeroOrLess(nameof(quantity));
			}
			if (suits != null) {
				if (suit != null) throw new ArgumentException($"Must not specify both {nameof(suit)} and {nameof(suits)}");
				suits.ThrowIfEmpty(nameof(suits));
				suits.ThrowIfAnyInvalidEnums(nameof(suits));
			}
			if (ranks != null) {
				if (rank != null) throw new ArgumentException($"Must not specify both {nameof(rank)} and {nameof(ranks)}");
				ranks.ThrowIfEmpty(nameof(ranks));
				ranks.ThrowIfAnyInvalidEnums(nameof(ranks));
			}
			if (values != null) {
				if (value != null) throw new ArgumentException($"Must not specify both {nameof(value)} and {nameof(values)}");
				values.ThrowIfEmpty(nameof(values));
			}
			if (cards != null) {
				if (card != null) throw new ArgumentException($"Must not specify both {nameof(card)} and {nameof(cards)}");
				cards.ThrowIfEmptyOrContainsNulls(nameof(cards));
			}
			if (suit != null) {
				suit.ThrowIfInvalid(nameof(suit));
			}
			if (rank != null) {
				rank.ThrowIfInvalid(nameof(rank));
			}
			if (skip != null) {
				if (nullValues == true) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(nullValues)}");
				if (suits != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(suits)}");
				if (ranks != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(ranks)}");
				if (values != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(values)}");
				if (cards != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(cards)}");
				if (suit != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(suit)}");
				if (rank != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(rank)}");
				if (value != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(value)}");
				if (card != null) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(card)}");
				skip.ThrowIfNegative(nameof(skip));
			}
			if (offset != null) {
				if (nullValues == true) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(nullValues)}");
				if (suits != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(suits)}");
				if (ranks != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(ranks)}");
				if (values != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(values)}");
				if (cards != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(cards)}");
				if (suit != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(suit)}");
				if (rank != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(rank)}");
				if (value != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(value)}");
				if (card != null) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(card)}");
				offset.ThrowIfNegative(nameof(offset));
			}

			if (quantity == null && skip == null && offset == null &&
				nullValues == null &&
				suit == null && suits == null && 
				rank == null && ranks == null &&
				card == null && cards == null && 
				value == null && values == null)
			{
				throw new ArgumentException("Must have at least one discard criterion");
			}

			Direction = direction;
			Quantity = quantity;
			Skip = skip ?? 0;
			Offset = offset ?? 0;
			NullValues = nullValues ?? false;

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