using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	public interface IRandomSource {
		/// <summary>
		/// Returns a uniformly distributed integer in the range [0, <paramref name="exclusiveMax" />)
		/// </summary>
		/// <param name="exclusiveMax">The exclusive maximum value. e.g. if specified as 100, the caller will get back an integer in the range [0, 99].</param>
		int SampleInt32(int exclusiveMax);
	}
}