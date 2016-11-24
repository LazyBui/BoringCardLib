using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DistributeResult {
		public IEnumerable<CardSet> Piles { get; private set; }

		public DistributeResult(IEnumerable<CardSet> piles)
		{
			piles.ThrowIfNullOrContainsNulls(nameof(piles));
			Piles = piles.ToList().AsReadOnly();
		}
	}
}