using System;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a draw operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableDrawSingleResult {
		/// <summary>
		/// The new set of cards without the drawn card.
		/// </summary>
		public ImmutableCardSet DrawnFrom { get; private set; }
		/// <summary>
		/// The card that was drawn.
		/// </summary>
		public Card Drawn { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableDrawSingleResult" /> class.
		/// </summary>
		/// <param name="newDrawnFrom">The <see cref="ImmutableCardSet" />s containing the new set of cards without the drawn cards.</param>
		/// <param name="drawn">The <see cref="Card" /> that was drawn.</param>
		/// <exception cref="ArgumentNullException"><paramref name="newDrawnFrom" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="drawn" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableDrawSingleResult(ImmutableCardSet newDrawnFrom, Card drawn) {
			newDrawnFrom.ThrowIfNull(nameof(newDrawnFrom));
			drawn.ThrowIfNull(nameof(drawn));
			DrawnFrom = newDrawnFrom;
			Drawn = drawn;
		}
	}
}