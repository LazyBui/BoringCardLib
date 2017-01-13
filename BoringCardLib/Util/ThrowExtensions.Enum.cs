using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoringCardLib {
	internal static partial class Extensions {
		private static Dictionary<Type, List<ulong>> sValuesCache =
			new Dictionary<Type, List<ulong>>();

		[DebuggerStepThrough]
		public static void ThrowIfInvalid(this Enum @this, string name = null) {
			if (object.ReferenceEquals(@this, null)) throw new ArgumentNullException(name);
			if (!HasValidValue(@this)) throw new ArgumentException("Must have a valid value", name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfAnyInvalidEnums<TValue>(this IEnumerable<TValue> @this, string name = null) where TValue : struct, IComparable {
			Type type = typeof(TValue);
			if (!type.IsEnum) throw new InvalidOperationException("Must be used with an enum type");
			bool anyFailed =
				@this.Any(v => {
					try {
						return !AllFlagsValuesDefined(Convert.ToUInt64(v), type);
					}
					catch (OverflowException) { }
					return true;
				});
			if (anyFailed) throw new ArgumentException("Must not have any invalid enum values", name);
		}

		public static bool HasValidValue(this Enum @this) {
			if (object.ReferenceEquals(@this, null)) throw new ArgumentNullException(nameof(@this));
			Type enumType = @this.GetType();
			return HasValidValue(@this, enumType);
		}

		internal static bool HasValidValue(this Enum @this, Type enumType) {
			if (object.ReferenceEquals(@this, null)) throw new ArgumentNullException(nameof(@this));
			if (object.ReferenceEquals(enumType, null)) throw new ArgumentNullException(nameof(enumType));
			if (HasFlags(enumType)) return AllFlagsValuesDefined(@this, enumType);
			return Enum.IsDefined(enumType, @this);
		}

		private static bool HasFlags(Type enumType) {
			return enumType.GetCustomAttribute<FlagsAttribute>() != null;
		}

		private static bool AllFlagsValuesDefined(Enum value, Type enumType) {
			try {
				return AllFlagsValuesDefined(Convert.ToUInt64(value), enumType);
			}
			catch (OverflowException) { }
			return false;
		}

		private static bool AllFlagsValuesDefined(ulong value, Type enumType) {
			List<ulong> values = GetCachedValues(enumType);
			if (value == 0) {
				return values.Any(v => v == 0);
			}

			ulong consumed = 0;
			ulong remaining = value;
			foreach (var v in values) {
				if (v == 0) continue;

				if ((remaining & v) == v) {
					remaining &= ~v;
					consumed |= v;
				}
				else if ((consumed & v) != 0) {
					// At least one bit of the current combination flag has been applied to other flags
					// We reconstruct the original to see if all flags from the current apply
					// If so, we mask all bits off the value and add them to consumed
					if (((consumed | remaining) & v) == v) {
						remaining &= ~v;
						consumed |= v;
					}
				}
			}

			return remaining == 0;
		}

		private static List<ulong> GetCachedValues(Type enumType) {
			List<ulong> values = null;
			if (!sValuesCache.TryGetValue(enumType, out values)) {
				values = new List<ulong>();
				foreach (Enum v in Enum.GetValues(enumType)) {
					values.Add(Convert.ToUInt64(v));
				}
				sValuesCache[enumType] = values;
			}
			return values;
		}

		#if NETFX_40_OR_LOWER
		public static TAttribute GetCustomAttribute<TAttribute>(this Type @this) where TAttribute : Attribute {
			if (object.ReferenceEquals(@this, null)) throw new ArgumentNullException(nameof(@this));
			var attrs = @this.GetCustomAttributes(typeof(TAttribute), false);
			if (attrs.Length == 0) return null;
			if (attrs.Length > 1) throw new InvalidOperationException();
			return (TAttribute)attrs[0];
		}
		#endif
	}
}
