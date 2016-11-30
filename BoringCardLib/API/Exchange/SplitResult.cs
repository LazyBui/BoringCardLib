using System;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a split operation.
	/// </summary>
	public sealed class SplitResult {
		/// <summary>
		/// The <see cref="CardSet" /> containing the top of the split.
		/// </summary>
		public CardSet Top { get; private set; }
		/// <summary>
		/// The <see cref="CardSet" /> containing the bottom of the split.
		/// </summary>
		public CardSet Bottom { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SplitResult" /> class.
		/// </summary>
		/// <param name="top">The <see cref="CardSet" /> containing the top of the split.</param>
		/// <param name="bottom">The <see cref="CardSet" /> containing the bottom of the split.</param>
		/// <exception cref="ArgumentNullException"><paramref name="top" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="bottom" /> is null.</exception>
		[DebuggerStepThrough]
		public SplitResult(CardSet top, CardSet bottom) {
			top.ThrowIfNull(nameof(top));
			bottom.ThrowIfNull(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}
	}
}