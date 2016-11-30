using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a replace operation.
	/// </summary>
	public sealed class ReplaceResult {
		/// <summary>
		/// The <see cref="CardSet" /> containing all replaced cards.
		/// </summary>
		public CardSet Replaced { get; private set; }
		/// <summary>
		/// The <see cref="CardSet" /> containing all cards that weren't found.
		/// </summary>
		public CardSet NotFound { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReplaceResult" /> class.
		/// </summary>
		/// <param name="replaced">The sequence of cards containing all replaced cards.</param>
		/// <param name="notFound">The sequence of cards containing all cards that weren't found.</param>
		/// <exception cref="ArgumentException"><paramref name="replaced" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="notFound" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replaced" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="notFound" /> is null.</exception>
		[DebuggerStepThrough]
		public ReplaceResult(IEnumerable<Card> replaced, IEnumerable<Card> notFound) {
			replaced.ThrowIfNullOrContainsNulls(nameof(replaced));
			notFound.ThrowIfNullOrContainsNulls(nameof(notFound));
			Replaced = new CardSet(replaced);
			NotFound = new CardSet(notFound);
		}
	}
}