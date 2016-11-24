using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableDiscardResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDiscardResult(null, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDiscardResult(new[] { Card.Joker }, null));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableDiscardResult(new[] { Card.Joker }, new Card[] { null }));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableDiscardResult(new Card[] { null }, new[] { Card.Joker }));
		}
	}
}
