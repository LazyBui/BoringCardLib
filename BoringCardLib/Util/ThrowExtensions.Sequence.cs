using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	internal static partial class Extensions {
		[DebuggerStepThrough]
		public static void ThrowIfNullOrEmpty<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfEmpty(name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfNullOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfContainsNulls(name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfNullOrEmptyOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfNull(name);
			@this.ThrowIfEmpty(name);
			@this.ThrowIfContainsNulls(name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfEmptyOrContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			@this.ThrowIfEmpty(name);
			@this.ThrowIfContainsNulls(name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfContainsNulls<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : class {
			if (@this.Any(v => v.IsNull())) throw new ArgumentException("Must have no null elements", name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfEmpty<TValue>(this IEnumerable<TValue> @this, string name = null) {
			if (!@this.Any()) throw new ArgumentException("Must have elements", name);
		}
	}
}
