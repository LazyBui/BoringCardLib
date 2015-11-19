using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class DiscardRequest {
		public int? Quantity { get; private set; }
		public IEnumerable<Suit> Suits { get; private set; }
		public IEnumerable<Rank> Ranks { get; private set; }
		public Direction Direction { get; private set; }

		public DiscardRequest(
			Direction pDirection = Direction.FromTop,
			int? pQuantity = null,
			Suit? pSuit = null,
			IEnumerable<Suit> pSuits = null,
			Rank? pRank = null,
			IEnumerable<Rank> pRanks = null)
		{
			pDirection.ThrowIfInvalid(nameof(pDirection));
			if (pQuantity != null) {
				if (pQuantity <= 0) throw new ArgumentException("Must be > 0", nameof(pQuantity));
			}
			if (pSuits != null) {
				if (pSuits.Count() == 0) throw new ArgumentException("Must have elements", nameof(pSuits));
				if (pSuit != null) throw new ArgumentException($"Must not specify both {nameof(pSuit)} and {nameof(pSuits)}");
				foreach (var suit in pSuits) {
					suit.ThrowIfInvalid(nameof(pSuits));
				}
			}
			if (pRanks != null) {
				if (pRanks.Count() == 0) throw new ArgumentException("Must have elements", nameof(pRanks));
				if (pRank != null) throw new ArgumentException($"Must not specify both {nameof(pRank)} and {nameof(pRanks)}");
				foreach (var rank in pRanks) {
					rank.ThrowIfInvalid(nameof(pRanks));
				}
			}
			pSuit.ThrowIfInvalid(nameof(pSuit));
			pRank.ThrowIfInvalid(nameof(pRank));

			if (pQuantity == null && pSuit == null && pRank == null && pSuits == null && pRanks == null) {
				throw new ArgumentException("Must have at least one discard criterion");
			}

			Direction = pDirection;
			Quantity = pQuantity;
			if (pSuits != null) Suits = pSuits;
			else if (pSuit != null) Suits = new[] { pSuit.GetValueOrDefault() };
			else Suits = new Suit[0];
			if (pRanks != null) Ranks = pRanks;
			else if (pRank != null) Ranks = new[] { pRank.GetValueOrDefault() };
			else Ranks = new Rank[0];
		}
	}
}