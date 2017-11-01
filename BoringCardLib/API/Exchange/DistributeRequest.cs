using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Represents a request to distribute.
	/// </summary>
	public sealed class DistributeRequest {
		/// <summary>
		/// Indicates how many piles there should be without considering remainder.
		/// </summary>
		public int? Piles { get; private set; }
		/// <summary>
		/// Indicates how many cards there should be per pile without considering remainder.
		/// </summary>
		public int? Cards { get; private set; }
		/// <summary>
		/// Indicates how the operation should handle distribution.
		/// </summary>
		public DistributionPolicy Distribution { get; private set; }
		/// <summary>
		/// Indicates what should be done about the remainder if the number of cards is not evenly divisible into <see cref="Piles" />.
		/// </summary>
		public RemainderPolicy Remainder { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DistributeRequest" /> class.
		/// </summary>
		/// <param name="numberOfPiles">How many piles there should be without factoring remainder.</param>
		/// <param name="numberOfCards">How many cards there should be in a pile without factoring remainder.</param>
		/// <param name="distributionPolicy">Indicates how the operation should handle distribution.</param>
		/// <param name="remainderPolicy">Indicates what should be done about the remainder if the number of cards is not evenly divisible into <paramref name="numberOfPiles" />.</param>
		/// <exception cref="ArgumentException"><paramref name="numberOfPiles" /> is 1 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="numberOfCards" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="distributionPolicy" /> is invalid.</exception>
		/// <exception cref="ArgumentException"><paramref name="remainderPolicy" /> is invalid.</exception>
		[DebuggerStepThrough]
		public DistributeRequest(
			int? numberOfPiles = null,
			int? numberOfCards = null,
			DistributionPolicy distributionPolicy = DistributionPolicy.Alternating,
			RemainderPolicy remainderPolicy = RemainderPolicy.Distribute)
		{
			numberOfPiles.ThrowIfBelowOrEqual(1, nameof(numberOfPiles));
			numberOfCards.ThrowIfZeroOrLess(nameof(numberOfCards));
			distributionPolicy.ThrowIfInvalid(nameof(distributionPolicy));
			remainderPolicy.ThrowIfInvalid(nameof(remainderPolicy));
			if (numberOfPiles.IsNull() == numberOfCards.IsNull()) throw new ArgumentException($"Must specify one of {nameof(numberOfPiles)} and {nameof(numberOfCards)}");

			Piles = numberOfPiles;
			Cards = numberOfCards;
			Distribution = distributionPolicy;
			Remainder = remainderPolicy;
		}
	}
}