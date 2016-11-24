using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public enum DistributionPolicy {
		/// <summary>
		/// Represents dealing 1 card to pile A, 1 card to pile B, 1 card to pile A, etc. until the supply is exhausted 
		/// </summary>
		Alternating,
		/// <summary>
		/// Represents dealing n cards to pile A and n cards to pile B to exhaust the supply
		/// </summary>
		Heap,
	}
}