using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		public static bool IsNull<TValue>(this TValue @this) where TValue : class {
			return object.ReferenceEquals(@this, null);
		}

		public static bool IsNotNull<TValue>(this TValue @this) where TValue : class {
			return !object.ReferenceEquals(@this, null);
		}
	}
}
