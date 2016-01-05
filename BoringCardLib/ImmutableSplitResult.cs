using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableSplitResult {
		public ImmutableCardGroup Top { get; private set; }
		public ImmutableCardGroup Bottom { get; private set; }

		public ImmutableSplitResult(ImmutableCardGroup top, ImmutableCardGroup bottom) {
			if (top == null) throw new ArgumentNullException(nameof(top));
			if (bottom == null) throw new ArgumentNullException(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}

		public ImmutableSplitResult(SplitResult result) {
			if (result == null) throw new ArgumentNullException(nameof(result));
			Top = result.Top.AsImmutable();
			Bottom = result.Bottom.AsImmutable();
		}
	}
}