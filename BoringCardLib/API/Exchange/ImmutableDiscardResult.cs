using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableDiscardResult {
		public ImmutableCardSet Pile { get; private set; }
		public ImmutableCardSet Discarded { get; private set; }

		public ImmutableDiscardResult(IEnumerable<Card> pile, IEnumerable<Card> discarded)
		{
			pile.ThrowIfNullOrContainsNulls(nameof(pile));
			discarded.ThrowIfNullOrContainsNulls(nameof(discarded));
			Pile = new ImmutableCardSet(pile);
			Discarded = new ImmutableCardSet(discarded);
		}
	}
}