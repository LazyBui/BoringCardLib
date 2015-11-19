using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	internal sealed class Sampler : IDisposable {
		private static readonly Sampler Instance = new Sampler();
		private Sampler() { }

		private RNGCryptoServiceProvider mProvider = new RNGCryptoServiceProvider();
		private const int StateSize = 2000;
		private byte[] mState = new byte[StateSize];
		private int mConsumed = StateSize;

		private IEnumerable<byte> SampleInternal() {
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

		private int SampleInt32Internal() {
			return BitConverter.ToInt32(SampleInternal().Take(sizeof(int)).ToArray(), 0) & int.MaxValue;
		}

		private int SampleInt32Internal(int pMaximum) {
			if (pMaximum <= 0) throw new ArgumentException("Must be > 0", nameof(pMaximum));
			const int minimum = 0;
			return (int)(minimum + (pMaximum - minimum) * (SampleInt32Internal() / (double)int.MaxValue));
		}

		public static IEnumerable<byte> Sample() {
			return Instance.SampleInternal();
		}

		public static int SampleInt32() {
			return Instance.SampleInt32Internal();
		}

		public static int SampleInt32(int pMaximum) {
			return Instance.SampleInt32Internal(pMaximum);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					mProvider.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~Sampler() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose() {
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}