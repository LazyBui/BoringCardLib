using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableSplitResult {
		public ImmutableCardSet Top { get; private set; }
		public ImmutableCardSet Bottom { get; private set; }

		public ImmutableSplitResult(ImmutableCardSet top, ImmutableCardSet bottom) {
			top.ThrowIfNull(nameof(top));
			bottom.ThrowIfNull(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}

		public ImmutableSplitResult(SplitResult result) {
			result.ThrowIfNull(nameof(result));
			Top = result.Top.AsImmutable();
			Bottom = result.Bottom.AsImmutable();
		}
	}
}