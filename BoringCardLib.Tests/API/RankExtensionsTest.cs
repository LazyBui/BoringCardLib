using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class RankExtensionsTest {
		[TestMethod]
		public void IsPipRank() {
			Assert.True(Rank.Ace.IsPipRank());
			Assert.True(Rank.Two.IsPipRank());
			Assert.True(Rank.Three.IsPipRank());
			Assert.True(Rank.Four.IsPipRank());
			Assert.True(Rank.Five.IsPipRank());
			Assert.True(Rank.Six.IsPipRank());
			Assert.True(Rank.Seven.IsPipRank());
			Assert.True(Rank.Eight.IsPipRank());
			Assert.True(Rank.Nine.IsPipRank());
			Assert.True(Rank.Ten.IsPipRank());
			Assert.False(Rank.Jack.IsPipRank());
			Assert.False(Rank.Queen.IsPipRank());
			Assert.False(Rank.King.IsPipRank());
			Assert.False(Rank.Joker.IsPipRank());

			Assert.False(((Rank)(-1)).IsPipRank());
		}

		[TestMethod]
		public void IsFaceRank() {
			Assert.False(Rank.Ace.IsFaceRank());
			Assert.False(Rank.Two.IsFaceRank());
			Assert.False(Rank.Three.IsFaceRank());
			Assert.False(Rank.Four.IsFaceRank());
			Assert.False(Rank.Five.IsFaceRank());
			Assert.False(Rank.Six.IsFaceRank());
			Assert.False(Rank.Seven.IsFaceRank());
			Assert.False(Rank.Eight.IsFaceRank());
			Assert.False(Rank.Nine.IsFaceRank());
			Assert.False(Rank.Ten.IsFaceRank());
			Assert.True(Rank.Jack.IsFaceRank());
			Assert.True(Rank.Queen.IsFaceRank());
			Assert.True(Rank.King.IsFaceRank());
			Assert.False(Rank.Joker.IsFaceRank());

			Assert.False(((Rank)(-1)).IsFaceRank());
		}
	}
}
