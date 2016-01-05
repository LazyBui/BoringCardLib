using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ImmutableDrawResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDrawResult(null, new ImmutableCardGroup()));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDrawResult(new ImmutableCardGroup(), null));
			Assert.DoesNotThrow(() => new ImmutableDrawResult(new ImmutableCardGroup(), new ImmutableCardGroup()));
		}
	}
}
