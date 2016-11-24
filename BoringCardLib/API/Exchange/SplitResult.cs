using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class SplitResult {
		public CardSet Top { get; private set; }
		public CardSet Bottom { get; private set; }

		public SplitResult(CardSet top, CardSet bottom) {
			top.ThrowIfNull(nameof(top));
			bottom.ThrowIfNull(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}
	}
}