using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class SplitResult {
		public CardGroup Top { get; private set; }
		public CardGroup Bottom { get; private set; }

		public SplitResult(CardGroup pTop, CardGroup pBottom) {
			if (pTop == null) throw new ArgumentNullException(nameof(pTop));
			if (pBottom == null) throw new ArgumentNullException(nameof(pBottom));
			Top = pTop;
			Bottom = pBottom;
		}
	}
}