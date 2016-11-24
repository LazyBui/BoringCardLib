using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		private static ArgumentException ThrowIfBelowOrEqualException<TValue>(TValue min, string name) {
			return new ArgumentException($"Must be greater than {min}", name);
		}

		public static void ThrowIfBelowOrEqual(this float @this, float min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this float? @this, float min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this float @this, double min, string name = null) {
			ThrowIfBelowOrEqual(@this, (float)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this float? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this double @this, double min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this double? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal @this, decimal min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal? @this, decimal min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal @this, double min, string name = null) {
			ThrowIfBelowOrEqual(@this, (decimal)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal @this, int min, string name = null) {
			ThrowIfBelowOrEqual(@this, (decimal)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this decimal? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this sbyte @this, sbyte min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this sbyte? @this, sbyte min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this sbyte @this, int min, string name = null) {
			ThrowIfBelowOrEqual(@this, (sbyte)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this sbyte? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this short @this, short min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this short? @this, short min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this short @this, int min, string name = null) {
			ThrowIfBelowOrEqual(@this, (short)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this short? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this int @this, int min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this int? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this long @this, long min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this long? @this, long min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this byte @this, byte min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this byte? @this, byte min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this byte @this, uint min, string name = null) {
			ThrowIfBelowOrEqual(@this, (byte)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this byte? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ushort @this, ushort min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ushort? @this, ushort min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ushort @this, uint min, string name = null) {
			ThrowIfBelowOrEqual(@this, (ushort)min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ushort? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this uint @this, uint min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this uint? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ulong @this, ulong min, string name = null) {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual(this ulong? @this, ulong min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}

		public static void ThrowIfBelowOrEqual<TValue>(this TValue @this, TValue min, string name = null) where TValue : struct, IComparable<TValue> {
			if (@this.CompareTo(min) <= 0) throw ThrowIfBelowOrEqualException(min, name: name);
		}

		public static void ThrowIfBelowOrEqual<TValue>(this TValue? @this, TValue min, string name = null) where TValue : struct, IComparable<TValue> {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelowOrEqual(min, name: name);
		}
	}
}
