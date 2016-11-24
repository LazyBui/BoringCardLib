using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableReplaceResult {
		public ImmutableCardSet Pile { get; private set; }
		public ImmutableCardSet Replaced { get; private set; }
		public ImmutableCardSet NotFound { get; private set; }

		public ImmutableReplaceResult(
			IEnumerable<Card> newPile,
			IEnumerable<Card> replaced,
			IEnumerable<Card> notFound)
		{
			newPile.ThrowIfNullOrContainsNulls(nameof(newPile));
			replaced.ThrowIfNullOrContainsNulls(nameof(replaced));
			notFound.ThrowIfNullOrContainsNulls(nameof(notFound));
			Pile = new ImmutableCardSet(newPile);
			Replaced = new ImmutableCardSet(replaced);
			NotFound = new ImmutableCardSet(notFound);
		}
	}
}