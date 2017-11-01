using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents a request to discard.
	/// </summary>
	public sealed class DiscardRequest {
		/// <summary>
		/// Indicates the maximum number of cards that may be discarded.
		/// </summary>
		public int? Quantity { get; private set; }
		/// <summary>
		/// Indicates how many cards after a discard should be skipped before resuming a discard.
		/// The default is 0.
		/// </summary>
		public int Skip { get; private set; }
		/// <summary>
		/// Indicates how many cards should be skipped before the first discard.
		/// The default is 0.
		/// </summary>
		public int Offset { get; private set; }
		/// <summary>
		/// Indicates point values that should be discarded.
		/// </summary>
		public IEnumerable<int> Values { get; private set; }
		/// <summary>
		/// Indicates whether cards with null point values should be discarded, assuming they match other criteria.
		/// The default is false.
		/// </summary>
		public bool NullValues { get; private set; }
		/// <summary>
		/// Indicates which cards should be discarded.
		/// </summary>
		public IEnumerable<Card> Cards { get; private set; }
		/// <summary>
		/// Indicates which suits should be discarded.
		/// </summary>
		public IEnumerable<Suit> Suits { get; private set; }
		/// <summary>
		/// Indicates which ranks should be discarded.
		/// </summary>
		public IEnumerable<Rank> Ranks { get; private set; }
		/// <summary>
		/// Indicates which direction the discard should traverse the deck.
		/// </summary>
		public Direction Direction { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscardRequest" /> class.
		/// </summary>
		/// <param name="direction">Indicates which direction the discard should traverse the deck.</param>
		/// <param name="quantity">Indicates the maximum number of cards that may be discarded.</param>
		/// <param name="skip">Indicates how many cards after a discard should be skipped before resuming a discard.</param>
		/// <param name="offset">Indicates how many cards should be skipped before the first discard.</param>
		/// <param name="value">Indicates a point value that should be discarded.</param>
		/// <param name="values">Indicates point values that should be discarded.</param>
		/// <param name="nullValues">Indicates whether cards with null point values should be discarded, assuming they match other criteria.</param>
		/// <param name="card">Indicates which card should be discarded.</param>
		/// <param name="cards">Indicates which cards should be discarded.</param>
		/// <param name="suit">Indicates which suit should be discarded.</param>
		/// <param name="suits">Indicates which suits should be discarded.</param>
		/// <param name="rank">Indicates which rank should be discarded.</param>
		/// <param name="ranks">Indicates which ranks should be discarded.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="quantity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="skip" /> is negative.</exception>
		/// <exception cref="ArgumentException"><paramref name="offset" /> is negative.</exception>
		/// <exception cref="ArgumentException"><paramref name="values" /> is specified and empty.</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is specified and empty or contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="suit" /> is specified and invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="suits" /> is specified and empty or contains invalid suits.</exception>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is specified and invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="ranks" /> is specified and empty or contains invalid ranks.</exception>
		/// <exception cref="ArgumentException"><paramref name="rank" /> and <paramref name="ranks"/> are both specified.</exception>
		/// <exception cref="ArgumentException"><paramref name="suit" /> and <paramref name="suits"/> are both specified.</exception>
		/// <exception cref="ArgumentException"><paramref name="value" /> and <paramref name="values"/> are both specified.</exception>
		/// <exception cref="ArgumentException"><paramref name="card" /> and <paramref name="cards"/> are both specified.</exception>
		/// <exception cref="ArgumentException">At least one of <paramref name="offset" /> or <paramref name="skip"/> are specified and there are specified criteria that aren't <paramref name="quantity" /> or <paramref name="direction" />.</exception>
		/// <exception cref="ArgumentException">No criteria are specified.</exception>
		[DebuggerStepThrough]
		public DiscardRequest(
			Direction direction = Direction.FirstToLast,
			int? quantity = null,
			int? skip = null,
			int? offset = null,
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
			if (quantity.IsNotNull()) {
				quantity.ThrowIfZeroOrLess(nameof(quantity));
			}
			if (suits.IsNotNull()) {
				if (suit.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(suit)} and {nameof(suits)}");
				suits.ThrowIfEmpty(nameof(suits));
				suits.ThrowIfAnyInvalidEnums(nameof(suits));
			}
			if (ranks.IsNotNull()) {
				if (rank.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(rank)} and {nameof(ranks)}");
				ranks.ThrowIfEmpty(nameof(ranks));
				ranks.ThrowIfAnyInvalidEnums(nameof(ranks));
			}
			if (values.IsNotNull()) {
				if (value.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(value)} and {nameof(values)}");
				values.ThrowIfEmpty(nameof(values));
			}
			if (cards.IsNotNull()) {
				if (card.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(card)} and {nameof(cards)}");
				cards.ThrowIfEmptyOrContainsNulls(nameof(cards));
			}
			if (suit.IsNotNull()) {
				suit.ThrowIfInvalid(nameof(suit));
			}
			if (rank.IsNotNull()) {
				rank.ThrowIfInvalid(nameof(rank));
			}
			if (skip.IsNotNull()) {
				if (nullValues == true) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(nullValues)}");
				if (suits.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(suits)}");
				if (ranks.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(ranks)}");
				if (values.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(values)}");
				if (cards.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(cards)}");
				if (suit.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(suit)}");
				if (rank.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(rank)}");
				if (value.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(value)}");
				if (card.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(skip)} and {nameof(card)}");
				skip.ThrowIfNegative(nameof(skip));
			}
			if (offset.IsNotNull()) {
				if (nullValues == true) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(nullValues)}");
				if (suits.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(suits)}");
				if (ranks.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(ranks)}");
				if (values.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(values)}");
				if (cards.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(cards)}");
				if (suit.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(suit)}");
				if (rank.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(rank)}");
				if (value.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(value)}");
				if (card.IsNotNull()) throw new ArgumentException($"Must not specify both {nameof(offset)} and {nameof(card)}");
				offset.ThrowIfNegative(nameof(offset));
			}

			if (quantity.IsNull() && skip.IsNull() && offset.IsNull() &&
				nullValues.IsNull() &&
				suit.IsNull() && suits.IsNull() && 
				rank.IsNull() && ranks.IsNull() &&
				card.IsNull() && cards.IsNull() && 
				value.IsNull() && values.IsNull())
			{
				throw new ArgumentException("Must have at least one discard criterion");
			}

			Direction = direction;
			Quantity = quantity;
			Skip = skip ?? 0;
			Offset = offset ?? 0;
			NullValues = nullValues ?? false;

			if (suits.IsNotNull()) Suits = suits;
			else if (suit.IsNotNull()) Suits = new[] { suit.GetValueOrDefault() };
			else Suits = new Suit[0];

			if (ranks.IsNotNull()) Ranks = ranks;
			else if (rank.IsNotNull()) Ranks = new[] { rank.GetValueOrDefault() };
			else Ranks = new Rank[0];

			if (cards.IsNotNull()) Cards = cards;
			else if (card.IsNotNull()) Cards = new[] { card };
			else Cards = new Card[0];

			if (values.IsNotNull()) Values = values;
			else if (value.IsNotNull()) Values = new[] { value.GetValueOrDefault() };
			else Values = new int[0];
		}
	}
}