using System;
using System.Diagnostics;

namespace BoringCardLib {
	/// <summary>
	/// Represents the result of a split operation on <see cref="ImmutableCardSet" />.
	/// </summary>
	public sealed class ImmutableSplitResult {
		/// <summary>
		/// The <see cref="ImmutableCardSet" /> containing the top of the split.
		/// </summary>
		public ImmutableCardSet Top { get; private set; }
		/// <summary>
		/// The <see cref="ImmutableCardSet" /> containing the bottom of the split.
		/// </summary>
		public ImmutableCardSet Bottom { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableSplitResult" /> class.
		/// </summary>
		/// <param name="top">The <see cref="ImmutableCardSet" /> containing the top of the split.</param>
		/// <param name="bottom">The <see cref="ImmutableCardSet" /> containing the bottom of the split.</param>
		/// <exception cref="ArgumentNullException"><paramref name="top" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="bottom" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableSplitResult(ImmutableCardSet top, ImmutableCardSet bottom) {
			top.ThrowIfNull(nameof(top));
			bottom.ThrowIfNull(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableSplitResult" /> class.
		/// </summary>
		/// <param name="result">The <see cref="SplitResult" /> containing the result of the operation upon a <see cref="CardSet"/>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="result" /> is null.</exception>
		[DebuggerStepThrough]
		public ImmutableSplitResult(SplitResult result) {
			result.ThrowIfNull(nameof(result));
			Top = result.Top.AsImmutable();
			Bottom = result.Bottom.AsImmutable();
		}
	}
}