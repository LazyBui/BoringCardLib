using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableSplitResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(null, new ImmutableCardSet()));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(new ImmutableCardSet(), null));
			Assert.DoesNotThrow(() => new ImmutableSplitResult(new ImmutableCardSet(), new ImmutableCardSet()));

			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(null));
			Assert.DoesNotThrow(() => new ImmutableSplitResult(new SplitResult(new CardSet(), new CardSet())));
		}
	}
}
