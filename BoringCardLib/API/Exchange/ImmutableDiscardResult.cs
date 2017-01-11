using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a discard operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableDiscardResult {
		/// <summary>
		/// The new set of cards without the discarded cards.
		/// </summary>
		public ImmutableCardSet Pile { get; private set; }
		/// <summary>
		/// The cards that were discarded.
		/// </summary>
		public ImmutableCardSet Discarded { get; private set; }
		/// <summary>
		/// The cards that were not found.
		/// </summary>
		public ImmutableCardSet NotFound { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableDiscardResult" /> class.
		/// </summary>
		/// <param name="pile">The new set of cards without the discarded cards.</param>
		/// <param name="discarded">The cards that were discarded.</param>
		/// <param name="notFound">The cards that were not found in the pile.</param>
		/// <exception cref="ArgumentException"><paramref name="pile" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="discarded" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="notFound" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="pile" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="discarded" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="notFound" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableDiscardResult(IEnumerable<Card> pile, IEnumerable<Card> discarded, IEnumerable<Card> notFound) {
			pile.ThrowIfNullOrContainsNulls(nameof(pile));
			discarded.ThrowIfNullOrContainsNulls(nameof(discarded));
			notFound.ThrowIfNullOrContainsNulls(nameof(notFound));
			Pile = new ImmutableCardSet(pile);
			Discarded = new ImmutableCardSet(discarded);
			NotFound = new ImmutableCardSet(notFound);
		}
	}
}