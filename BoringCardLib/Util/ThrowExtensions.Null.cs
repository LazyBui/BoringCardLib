using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		public static void ThrowIfNull<TValue>(this TValue @this, string name = null) where TValue : class {
			if (object.ReferenceEquals(@this, null)) throw new ArgumentNullException(name);
		}
	}
}
