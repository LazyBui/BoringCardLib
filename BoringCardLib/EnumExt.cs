using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	internal static class EnumExt {
		public static IEnumerable<TValue> CastedValues<TValue>() {
			return Enum.GetValues(typeof(TValue)).Cast<TValue>();
		}

		public static void ThrowIfInvalid(this Enum pValue, string pName = null) {
			if (pValue == null) return;
			if (!Enum.IsDefined(pValue.GetType(), pValue)) throw new ArgumentException("Must have a valid value", pName);
		}
	}
}