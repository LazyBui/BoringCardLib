using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class SuitExtensionsTest {
		[TestMethod]
		public void IsBlackSuit() {
			Assert.True(Suit.Clubs.IsBlackSuit());
			Assert.True(Suit.Spades.IsBlackSuit());
			Assert.False(Suit.Diamonds.IsBlackSuit());
			Assert.False(Suit.Hearts.IsBlackSuit());
			Assert.False(Suit.Joker.IsBlackSuit());

			Assert.False(((Suit)(-1)).IsBlackSuit());
		}

		[TestMethod]
		public void IsRedSuit() {
			Assert.False(Suit.Clubs.IsRedSuit());
			Assert.False(Suit.Spades.IsRedSuit());
			Assert.True(Suit.Diamonds.IsRedSuit());
			Assert.True(Suit.Hearts.IsRedSuit());
			Assert.False(Suit.Joker.IsRedSuit());

			Assert.False(((Suit)(-1)).IsRedSuit());
		}
	}
}
