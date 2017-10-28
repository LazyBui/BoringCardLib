using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents an exception generated due to an empty deck.
	/// </summary>
	public sealed class DeckEmptyException : InvalidOperationException {
		/// <summary>
		/// Initializes a new instance of the <see cref="DeckEmptyException" /> class. 
		/// </summary>
		public DeckEmptyException() : base("The deck is empty and this operation is unsupported") {

		}
	}
}