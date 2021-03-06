﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ImmutableDistributeResultTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentNullException>(() => new ImmutableDistributeResult(null));
			Assert.ThrowsExact<ArgumentException>(() => new ImmutableDistributeResult(new CardSet[] { null }));
		}
	}
}
