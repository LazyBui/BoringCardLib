using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		public static bool IsNull<TValue>(this TValue? @this) where TValue : struct {
			return !@this.HasValue;
		}

		public static bool IsNotNull<TValue>(this TValue? @this) where TValue : struct {
			return @this.HasValue;
		}
	}
}
