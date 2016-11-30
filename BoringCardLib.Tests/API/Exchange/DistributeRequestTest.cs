using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class DistributeRequestTest {
		[TestMethod]
		public void Construct() {
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest());
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: -1));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: 0));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfCards: -1));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfCards: 0));
			Assert.DoesNotThrow(() => new DistributeRequest(numberOfPiles: 2));
			Assert.DoesNotThrow(() => new DistributeRequest(numberOfCards: 1));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: 2, numberOfCards: 1));

			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: 2, distributionPolicy: (DistributionPolicy)(-1)));
			Assert.ThrowsExact<ArgumentException>(() => new DistributeRequest(numberOfPiles: 2, remainderPolicy: (RemainderPolicy)(-1)));
		}
	}
}
