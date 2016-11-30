using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a replace operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableReplaceResult {
		/// <summary>
		/// The new set of cards with applied replacements.
		/// </summary>
		public ImmutableCardSet Pile { get; private set; }
		/// <summary>
		/// The <see cref="ImmutableCardSet" /> containing all replaced cards.
		/// </summary>
		public ImmutableCardSet Replaced { get; private set; }
		/// <summary>
		/// The <see cref="ImmutableCardSet" /> containing all cards that weren't found.
		/// </summary>
		public ImmutableCardSet NotFound { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableReplaceResult" /> class.
		/// </summary>
		/// <param name="newPile">The new sequence of cards with applied replacements.</param>
		/// <param name="replaced">The sequence of cards containing all replaced cards.</param>
		/// <param name="notFound">The sequence of cards containing all cards that weren't found.</param>
		/// <exception cref="ArgumentException"><paramref name="newPile" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="replaced" /> contains nulls.</exception>
		/// <exception cref="ArgumentException"><paramref name="notFound" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="newPile" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replaced" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="notFound" /> is null.</exception>
		[DebuggerStepThrough]
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