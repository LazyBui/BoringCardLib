using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a distribute operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableDistributeResult {
		/// <summary>
		/// The <see cref="ImmutableCardSet" />s containing the distributed cards.
		/// </summary>
		public IEnumerable<ImmutableCardSet> Piles { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableDistributeResult" /> class.
		/// </summary>
		/// <param name="piles">The <see cref="ImmutableCardSet" />s containing the distributed cards.</param>
		/// <exception cref="ArgumentException"><paramref name="piles" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="piles" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableDistributeResult(IEnumerable<CardSet> piles) {
			piles.ThrowIfNullOrContainsNulls(nameof(piles));
			Piles = piles.Select(p => p.AsImmutable()).ToList().AsReadOnly();
		}
	}
}