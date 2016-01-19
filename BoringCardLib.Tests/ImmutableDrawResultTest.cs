using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
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
