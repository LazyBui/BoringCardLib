using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	internal sealed class Sampler {
		private RNGCryptoServiceProvider mProvider = new RNGCryptoServiceProvider();
		private const int StateSize = 2000;
		private byte[] mState = new byte[StateSize];
		private int mConsumed = StateSize;

		public IEnumerable<byte> Sample() {
			while (true) {
				if (mConsumed >= StateSize) {
					mProvider.GetBytes(mState);
					mConsumed = 0;
				}
				for (int i = mConsumed; i < StateSize; i++) {
					mConsumed++;
					yield return mState[i];
				}
			}
		}

		public int SampleInt32() {
			return BitConverter.ToInt32(Sample().Take(sizeof(int)).ToArray(), 0) & int.MaxValue;
		}

		public int SampleInt32(int pMaximum) {
			const int minimum = 0;
			return (int)(minimum + (pMaximum - minimum) * (SampleInt32() / (double)int.MaxValue));
		}
	}
}