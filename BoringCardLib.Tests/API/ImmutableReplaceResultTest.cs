using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableReplaceResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableReplaceResult(null, new[] { Card.Joker }, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableReplaceResult(new[] { Card.Joker }, null, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableReplaceResult(new[] { Card.Joker }, new[] { Card.Joker }, null));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableReplaceResult(new Card[] { null }, new[] { Card.Joker }, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableReplaceResult(new[] { Card.Joker }, new Card[] { null }, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableReplaceResult(new[] { Card.Joker }, new[] { Card.Joker }, new Card[] { null }));
		}
	}
}
