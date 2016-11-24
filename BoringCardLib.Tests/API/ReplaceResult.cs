using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ReplaceResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ReplaceResult(null, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentNullException>(() => new ReplaceResult(new[] { Card.Joker }, null));
			Assert.ThrowsExact<ArgumentException>(() => new ReplaceResult(new Card[] { null }, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentException>(() => new ReplaceResult(new[] { Card.Joker }, new Card[] { null }));
		}
	}
}
