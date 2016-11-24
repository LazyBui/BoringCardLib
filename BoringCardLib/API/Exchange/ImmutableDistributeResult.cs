using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ImmutableDistributeResult {
		public IEnumerable<ImmutableCardSet> Piles { get; private set; }

		public ImmutableDistributeResult(IEnumerable<CardSet> piles)
		{
			piles.ThrowIfNullOrContainsNulls(nameof(piles));
			Piles = piles.Select(p => p.AsImmutable()).ToList().AsReadOnly();
		}
	}
}