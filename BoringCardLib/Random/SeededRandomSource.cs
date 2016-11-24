using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	public sealed class SeededRandomSource : IRandomSource {
		private Random mInstance = null;
		private int mSeed;

		public SeededRandomSource() {
			mSeed = Environment.TickCount;
			mInstance = new Random(mSeed);
		}

		public SeededRandomSource(int seed) {
			mSeed = seed;
			mInstance = new Random(seed);
		}

		public int SampleInt32(int exclusiveMax) {
			return mInstance.Next(exclusiveMax);
		}

		public void Reset() {
			mInstance = new Random(mSeed);
		}
	}
}