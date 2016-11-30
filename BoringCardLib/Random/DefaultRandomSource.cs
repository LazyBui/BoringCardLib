using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	/// <summary>
	/// Represents using the <see cref="RNGCryptoServiceProvider" /> class as a source of randomness.
	/// </summary>
	public sealed class DefaultRandomSource : IDisposable, IRandomSource {
		private static RNGCryptoServiceProvider mProvider = new RNGCryptoServiceProvider();
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

		private int SampleInt32Internal(int exclusiveMax) {
			if (exclusiveMax <= 0) throw new ArgumentException("Must be > 0", nameof(exclusiveMax));

			int bitsRequiredForMax = (int)Math.Floor(Math.Log(exclusiveMax, 2)) + 1;
			int powerOfTwo = 1 << bitsRequiredForMax;
			int mask = powerOfTwo - 1;
			int result = int.MaxValue;
			do {
				result = SampleInt32Internal() & mask;
			} while (result >= exclusiveMax);

			return result;
		}

		/// <summary>
		/// Returns an integer the range [0, <paramref name="exclusiveMax" />)
		/// </summary>
		/// <param name="exclusiveMax">The exclusive maximum value. e.g. if specified as 100, the caller will get back an integer in the range [0, 99].</param>
		/// <returns>An integer in the range [0, <paramref name="exclusiveMax" />).</returns>
		/// <exception cref="ArgumentException"><paramref name="exclusiveMax" /> is less than or equal to 0.</exception>
		public int SampleInt32(int exclusiveMax) {
			return SampleInt32Internal(exclusiveMax);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					#if NETFX_40_OR_HIGHER
					mProvider.Dispose();
					#endif
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