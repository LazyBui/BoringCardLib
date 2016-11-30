using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a discard operation.
	/// </summary>
	public sealed class DiscardResult {
		/// <summary>
		/// The cards that were discarded.
		/// </summary>
		public CardSet Discarded { get; private set; }
		/// <summary>
		/// The cards that were not found.
		/// </summary>
		public CardSet NotFound { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscardResult" /> class.
		/// </summary>
		/// <param name="cards">The cards that were discarded.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="notFound" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="notFound" /> is null.</exception>
		[DebuggerStepThrough]
		public DiscardResult(IEnumerable<Card> cards, IEnumerable<Card> notFound) {
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			notFound.ThrowIfNullOrContainsNulls(nameof(notFound));
			Discarded = new CardSet(cards);
			NotFound = new CardSet(notFound);
		}
	}
}