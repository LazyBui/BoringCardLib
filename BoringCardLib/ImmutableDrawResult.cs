using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableDrawResult {
		public ImmutableCardGroup DrawnFrom { get; private set; }
		public ImmutableCardGroup Drawn { get; private set; }

		public ImmutableDrawResult(ImmutableCardGroup newDrawnFrom, ImmutableCardGroup drawn) {
			if (newDrawnFrom == null) throw new ArgumentNullException(nameof(newDrawnFrom));
			if (drawn == null) throw new ArgumentNullException(nameof(drawn));
			DrawnFrom = newDrawnFrom;
			Drawn = drawn;
		}
	}
}