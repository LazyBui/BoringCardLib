using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents a source of randomness.
	/// </summary>
	public interface IRandomSource {
		/// <summary>
		/// Returns an integer the range [0, <paramref name="exclusiveMax" />)
		/// </summary>
		/// <param name="exclusiveMax">The exclusive maximum value. e.g. if specified as 100, the caller will get back an integer in the range [0, 99].</param>
		/// <returns>An integer in the range [0, <paramref name="exclusiveMax" />).</returns>
		/// <remarks>
		/// Expected to throw <see cref="ArgumentException" /> for values that are less than or equal to 0.
		/// All other exceptions are expected not to be thrown.
		/// </remarks>
		int SampleInt32(int exclusiveMax);
	}
}