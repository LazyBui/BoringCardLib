using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class DefaultRandomSourceTest {
		[TestMethod]
		public void Sample() {
			Assert.DoesNotThrow(() => {
				const int iterations = 2000000;
				var sampler = new DefaultRandomSource();
				const int maxValue = 67;
				for (int i = 0; i < iterations; i++) {
					int x = sampler.SampleInt32(maxValue);
					if (x < 0) throw new InvalidOperationException();
					else if (x > maxValue) throw new InvalidOperationException();
				}
			});
		}
	}
}
