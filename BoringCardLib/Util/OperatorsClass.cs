using System;

namespace BoringCardLib {
	internal static class OperatorsClass {
		public static bool Equal<TValue>(TValue lhs, TValue rhs) where TValue : class, IEquatable<TValue> {
			return object.ReferenceEquals(lhs, rhs) ||
				(lhs.IsNotNull() && rhs.IsNotNull() && lhs.Equals(rhs));
		}

		public static bool NotEqual<TValue>(TValue lhs, TValue rhs) where TValue : class, IEquatable<TValue> {
			return !object.ReferenceEquals(lhs, rhs) &&
				(lhs.IsNull() || rhs.IsNull() || !lhs.Equals(rhs));
		}
	}
}
