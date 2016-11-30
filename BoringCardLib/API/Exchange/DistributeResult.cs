using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a distribute operation.
	/// </summary>
	public sealed class DistributeResult {
		/// <summary>
		/// The <see cref="CardSet" />s containing the distributed cards.
		/// </summary>
		public IEnumerable<CardSet> Piles { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DistributeResult" /> class.
		/// </summary>
		/// <param name="piles">The <see cref="CardSet" />s containing the distributed cards.</param>
		/// <exception cref="ArgumentException"><paramref name="piles" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="piles" /> is null.</exception>
		[DebuggerStepThrough]
		public DistributeResult(IEnumerable<CardSet> piles) {
			piles.ThrowIfNullOrContainsNulls(nameof(piles));
			Piles = piles.ToList().AsReadOnly();
		}
	}
}