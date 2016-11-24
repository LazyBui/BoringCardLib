using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	internal static partial class Extensions {
		public static void ThrowIfNullOrEmpty<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfEmpty(name);
		}

		public static void ThrowIfNullOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfContainsNulls(name);
		}

		public static void ThrowIfNullOrEmptyOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfEmpty(name);
			@this.ThrowIfContainsNulls(name);
		}

		public static void ThrowIfEmptyOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfEmpty(name);
			@this.ThrowIfContainsNulls(name);
		}

		public static void ThrowIfContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			if (@this.Any(v => object.ReferenceEquals(v, null))) throw new ArgumentException("Must have no null elements", name);
		}

		public static void ThrowIfEmpty<TValue>(this IEnumerable<TValue> @this, string name = null) {
			if (!@this.Any()) throw new ArgumentException("Must have elements", name);
		}
	}
}
