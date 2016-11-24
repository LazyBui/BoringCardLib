using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ReplaceRequestTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ReplaceRequest(null, Card.Joker));
			Assert.ThrowsExact<ArgumentNullException>(() => new ReplaceRequest(Card.Joker, null));
		}
	}
}
