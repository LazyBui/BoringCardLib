using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ImmutableSplitResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(null, new ImmutableCardGroup()));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(new ImmutableCardGroup(), null));
			Assert.DoesNotThrow(() => new ImmutableSplitResult(new ImmutableCardGroup(), new ImmutableCardGroup()));

			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableSplitResult(null));
			Assert.DoesNotThrow(() => new ImmutableSplitResult(new SplitResult(new CardGroup(), new CardGroup())));
		}
	}
}
