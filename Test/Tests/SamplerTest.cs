using System;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class SamplerTest {
		[TestMethod]
		public void Construct() {
			Assert.DoesNotThrow(() => new Sampler());
		}

		[TestMethod]
		public void Sample() {
			Assert.DoesNotThrow(() => {
				var sampler = new Sampler();
				const int iterations = 2000000;
				for (int i = 0; i < iterations; i++) {
					int x = sampler.SampleInt32();
				}

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
