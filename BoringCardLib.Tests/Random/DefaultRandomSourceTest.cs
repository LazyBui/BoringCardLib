using System;
using System.Collections.Generic;
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
				const int exclusiveMaxValue = 67;
				var distribution = new Dictionary<int, int>();
				for (int i = 0; i < iterations; i++) {
					int x = sampler.SampleInt32(exclusiveMaxValue);
					if (x < 0) throw new InvalidOperationException();
					if (x >= exclusiveMaxValue) throw new InvalidOperationException();
					
					int count;
					if (!distribution.TryGetValue(x, out count)) {
						count = 0;
					}

					distribution[x] = ++count;
				}

				// [0-exclusiveMax)
				// exclusive would be -1, but since we're including 0, it's still the value
				Assert.True(distribution.Count == exclusiveMaxValue);
			});
		}
	}
}
