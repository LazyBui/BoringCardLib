using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableDrawResult {
		public ImmutableCardSet DrawnFrom { get; private set; }
		public ImmutableCardSet Drawn { get; private set; }

		public ImmutableDrawResult(ImmutableCardSet newDrawnFrom, ImmutableCardSet drawn) {
			newDrawnFrom.ThrowIfNull(nameof(newDrawnFrom));
			drawn.ThrowIfNull(nameof(drawn));
			DrawnFrom = newDrawnFrom;
			Drawn = drawn;
		}
	}
}