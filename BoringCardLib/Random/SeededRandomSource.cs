using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents using the <see cref="Random" /> class as a source of randomness.
	/// </summary>
	public sealed class SeededRandomSource : IRandomSource {
		private Random mInstance = null;
		private int mSeed;

		/// <summary>
		/// Initializes a new instance of the <see cref="SeededRandomSource" /> class with a default seed.
		/// </summary>
		public SeededRandomSource() {
			mSeed = Environment.TickCount;
			mInstance = new Random(mSeed);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SeededRandomSource" /> class with a specified seed.
		/// </summary>
		/// <param name="seed">The seed to initialize the state of the source with.</param>
		public SeededRandomSource(int seed) {
			mSeed = seed;
			mInstance = new Random(seed);
		}

		/// <summary>
		/// Returns an integer the range [0, <paramref name="exclusiveMax" />)
		/// </summary>
		/// <param name="exclusiveMax">The exclusive maximum value. e.g. if specified as 100, the caller will get back an integer in the range [0, 99].</param>
		/// <returns>An integer in the range [0, <paramref name="exclusiveMax" />).</returns>
		/// <exception cref="ArgumentException"><paramref name="exclusiveMax" /> is less than or equal to 0.</exception>
		public int SampleInt32(int exclusiveMax) {
			if (exclusiveMax <= 0) throw new ArgumentException("Must be > 0", nameof(exclusiveMax));
			return mInstance.Next(exclusiveMax);
		}

		/// <summary>
		/// Resets the state of the source to the original seed.
		/// </summary>
		public void Reset() {
			mInstance = new Random(mSeed);
		}

		/// <summary>
		/// Resets the state of the source to the specified seed.
		/// </summary>
		/// <param name="seed">The new seed.</param>
		public void Reset(int seed) {
			mSeed = seed;
			mInstance = new Random(seed);
		}
	}
}