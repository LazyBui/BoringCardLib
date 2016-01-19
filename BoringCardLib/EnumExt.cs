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
		public static void ThrowIfInvalid(this Enum @this, string name = null) {
			if (@this == null) return;
			if (!IsValidValue(@this)) throw new ArgumentException("Must have a valid value", name);
		}

		public static bool IsValidValue(this Enum @this) {
			Type enumType = @this.GetType();
			if (@this.HasFlags(enumType)) return AllFlagsValuesDefined(@this, enumType);
			return Enum.IsDefined(enumType, @this);
		}

		private static bool HasFlags(this Enum @this, Type enumType) {
			return enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
		}

		private static bool AllFlagsValuesDefined(Enum value, Type enumType) {
			try {
				ulong flagValue = Convert.ToUInt64(value);
				foreach (Enum v in Enum.GetValues(enumType)) {
					ulong intValue = Convert.ToUInt64(v);
					if (intValue == 0) continue;

					if ((flagValue & intValue) == intValue) {
						flagValue -= intValue;
					}
				}

				return flagValue == 0;
			}
			catch (OverflowException) { }
			return false;
		}
	}
}