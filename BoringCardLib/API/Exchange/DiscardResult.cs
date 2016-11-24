using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DiscardResult {
		public CardSet Cards { get; private set; }

		public DiscardResult(IEnumerable<Card> cards)
		{
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			Cards = new CardSet(cards);
		}
	}
}