using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents an exception generated due to an empty deck.
	/// </summary>
	public sealed class DeckEmptyException : InvalidOperationException {
		internal DeckEmptyException() : base("The deck is empty and this operation is unsupported") {

		}
	}
}