using System;
using System.Diagnostics;

namespace BoringCardLib {
	internal static partial class Extensions {
		[DebuggerStepThrough]
		public static void ThrowIfNull<TValue>(this TValue @this, string name = null) where TValue : class {
			if (@this.IsNull()) throw new ArgumentNullException(name);
		}
	}
}
