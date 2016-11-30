using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableDrawSingleResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDrawSingleResult(null, Card.Joker));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDrawSingleResult(new ImmutableCardSet(), null));
			Assert.DoesNotThrow(() => new ImmutableDrawSingleResult(new ImmutableCardSet(), Card.Joker));
		}
	}
}
