﻿using System;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a draw operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableDrawResult {
		/// <summary>
		/// The new set of cards without the drawn cards.
		/// </summary>
		public ImmutableCardSet DrawnFrom { get; private set; }
		/// <summary>
		/// The cards that were drawn.
		/// </summary>
		public ImmutableCardSet Drawn { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableDrawResult" /> class.
		/// </summary>
		/// <param name="newDrawnFrom">The <see cref="ImmutableCardSet" />s containing the new set of cards without the drawn cards.</param>
		/// <param name="drawn">The <see cref="ImmutableCardSet" />s containing the drawn cards.</param>
		/// <exception cref="ArgumentNullException"><paramref name="newDrawnFrom" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="drawn" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableDrawResult(ImmutableCardSet newDrawnFrom, ImmutableCardSet drawn) {
			newDrawnFrom.ThrowIfNull(nameof(newDrawnFrom));
			drawn.ThrowIfNull(nameof(drawn));
			DrawnFrom = newDrawnFrom;
			Drawn = drawn;
		}
	}
}