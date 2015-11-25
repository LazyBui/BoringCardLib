using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class SplitResult {
		public CardGroup Top { get; private set; }
		public CardGroup Bottom { get; private set; }

		public SplitResult(CardGroup top, CardGroup bottom) {
			if (top == null) throw new ArgumentNullException(nameof(top));
			if (bottom == null) throw new ArgumentNullException(nameof(bottom));
			Top = top;
			Bottom = bottom;
		}
	}
}