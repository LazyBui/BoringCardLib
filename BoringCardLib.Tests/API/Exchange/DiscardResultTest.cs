using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class DiscardResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new DiscardResult(null, new[] { Card.Joker }));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardResult(new Card[] { null }, new[] { Card.Joker }));

			Assert.ThrowsExact<ArgumentNullException>(() => new DiscardResult(new[] { Card.Joker }, null));
			Assert.ThrowsExact<ArgumentException>(() => new DiscardResult(new[] { Card.Joker }, new Card[] { null }));
		}
	}
}
