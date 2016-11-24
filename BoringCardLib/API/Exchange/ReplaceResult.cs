using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ReplaceResult {
		public CardSet Replaced { get; private set; }
		public CardSet NotFound { get; private set; }

		public ReplaceResult(IEnumerable<Card> replaced, IEnumerable<Card> notFound) {
			replaced.ThrowIfNullOrContainsNulls(nameof(replaced));
			notFound.ThrowIfNullOrContainsNulls(nameof(notFound));
			Replaced = new CardSet(replaced);
			NotFound = new CardSet(notFound);
		}
	}
}