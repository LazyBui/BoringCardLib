using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class DistributeRequestTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(-1));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(0));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(1));
			Assert.DoesNotThrow(() => new DistributeRequest(2));

			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(2, distributionPolicy: (DistributionPolicy)(-1)));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(2, remainderPolicy: (RemainderPolicy)(-1)));
		}
	}
}
