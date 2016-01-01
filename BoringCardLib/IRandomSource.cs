using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace BoringCardLib {
	public interface IRandomSource {
		int SampleInt32(int max);
	}
}