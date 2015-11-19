using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class SamplerTest {
		[TestMethod]
		public void Sample() {
			Assert.DoesNotThrow(() => {
				const int iterations = 2000000;
				for (int i = 0; i < iterations; i++) {
					int x = Sampler.SampleInt32();
				}

				const int maxValue = 67;
				for (int i = 0; i < iterations; i++) {
					int x = Sampler.SampleInt32(maxValue);
					if (x < 0) throw new InvalidOperationException();
					else if (x > maxValue) throw new InvalidOperationException();
				}
			});
		}
	}
}
