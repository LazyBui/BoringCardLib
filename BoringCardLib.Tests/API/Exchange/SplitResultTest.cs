using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class SplitResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new SplitResult(null, new CardSet()));
			Assert.ThrowsExact<ArgumentNullException>(() => new SplitResult(new CardSet(), null));
			Assert.DoesNotThrow(() => new SplitResult(new CardSet(), new CardSet()));
		}
	}
}
