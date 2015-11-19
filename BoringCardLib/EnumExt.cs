using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	internal static class EnumExt<TValue> {
		private static Lazy<IEnumerable<TValue>> sCache =
			new Lazy<IEnumerable<TValue>>(() =>
				Enum.GetValues(typeof(TValue)).
					Cast<TValue>().
					ToList().
					AsReadOnly());

		public static IEnumerable<TValue> GetValues() {
			return sCache.Value;
		}
	}

	internal static class EnumExt {
		public static void ThrowIfInvalid(this Enum pValue, string pName = null) {
			if (pValue == null) return;
			if (!Enum.IsDefined(pValue.GetType(), pValue)) {
				throw new ArgumentException("Must have a valid value", pName);
			}
		}
	}
}