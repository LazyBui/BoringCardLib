using System;
using System.Linq;

namespace BoringCardLib {
	public static partial class RankExtensions {
		private static readonly Rank[] sFaceRanks = new[] {
			Rank.Jack,
			Rank.Queen,
			Rank.King,
		};

		public static bool IsFaceRank(this Rank @this) {
			return sFaceRanks.Contains(@this);
		}

		public static bool IsPipRank(this Rank @this) {
			return !IsFaceRank(@this) && @this != Rank.Joker;
		}
	}
}