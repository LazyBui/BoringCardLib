using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents a request to replace a card.
	/// </summary>
	public sealed class ReplaceRequest {
		/// <summary>
		/// The card to find.
		/// </summary>
		public Card Card { get; private set; }
		/// <summary>
		/// The card to take the place of the previous card.
		/// </summary>
		public Card Replacement { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReplaceRequest" /> class.
		/// </summary>
		/// <param name="card">The card to find.</param>
		/// <param name="replacement">The card to take the place of the previous card.</param>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replacement" /> is null.</exception>
		[DebuggerStepThrough]
		public ReplaceRequest(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			Card = card;
			Replacement = replacement;
		}
	}
}