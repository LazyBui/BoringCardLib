using System;

namespace BoringCardLib {
	internal static class Operators {
		public static bool Equals<TValue>(TValue lhs, TValue rhs) where TValue : IEquatable<TValue> {
			return object.ReferenceEquals(lhs, rhs) ||
				(!object.ReferenceEquals(lhs, null) && !object.ReferenceEquals(rhs, null) &&
					lhs.Equals(rhs));
		}

		public static bool NotEquals<TValue>(TValue lhs, TValue rhs) where TValue : IEquatable<TValue> {
			return !Equals(lhs, rhs);
		}
	}
}
