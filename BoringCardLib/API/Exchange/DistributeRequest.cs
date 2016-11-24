using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DistributeRequest {
		public int Piles { get; private set; }
		public DistributionPolicy Distribution { get; private set; }
		public RemainderPolicy Remainder { get; private set; }

		public DistributeRequest(int numberOfPiles, DistributionPolicy distributionPolicy = DistributionPolicy.Alternating, RemainderPolicy remainderPolicy = RemainderPolicy.Distribute)
		{
			numberOfPiles.ThrowIfBelowOrEqual(1, nameof(numberOfPiles));
			distributionPolicy.ThrowIfInvalid(nameof(distributionPolicy));
			remainderPolicy.ThrowIfInvalid(nameof(remainderPolicy));

			Piles = numberOfPiles;
			Distribution = distributionPolicy;
			Remainder = remainderPolicy;
		}
	}
}