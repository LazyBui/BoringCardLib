using System;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Provides some utility methods for <see cref="Rank" />.
	/// </summary>
	public static partial class RankExtensions {
		private static readonly Rank[] sFaceRanks = new[] {
			Rank.Jack,
			Rank.Queen,
			Rank.King,
		};

		/// <summary>
		/// Indicates whether the <see cref="Rank" /> is a face rank.
		/// </summary>
		/// <param name="this">The <see cref="Rank" /> instance.</param>
		/// <returns>true if <paramref name="this"/> is <see cref="Rank.Jack" />, <see cref="Rank.Queen" />, or <see cref="Rank.King" />. Otherwise, it returns false.</returns>
		public static bool IsFaceRank(this Rank @this) {
			return sFaceRanks.Contains(@this);
		}

		/// <summary>
		/// Indicates whether the <see cref="Rank" /> is a pip rank.
		/// </summary>
		/// <param name="this">The <see cref="Rank" /> instance.</param>
		/// <returns>true if <paramref name="this"/> is <see cref="Rank.Ace" />, <see cref="Rank.Two" />, <see cref="Rank.Three" />, <see cref="Rank.Four" />, <see cref="Rank.Five" />, <see cref="Rank.Six" />, <see cref="Rank.Seven" />, <see cref="Rank.Eight" />, <see cref="Rank.Nine" />, or <see cref="Rank.Ten" />. Otherwise, it returns false.</returns>
		public static bool IsPipRank(this Rank @this) {
			return
				EnumExt<Rank>.IsDefined(@this) &&
				!IsFaceRank(@this) &&
				@this != Rank.Joker;
		}
	}
}