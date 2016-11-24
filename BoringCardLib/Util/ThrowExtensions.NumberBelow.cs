using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		private static ArgumentException ThrowIfBelowException<TValue>(TValue min, string name) {
			return new ArgumentException($"Must be {min} or greater", name);
		}

		public static void ThrowIfBelow(this float @this, float min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this float? @this, float min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this float @this, double min, string name = null) {
			ThrowIfBelow(@this, (float)min, name: name);
		}

		public static void ThrowIfBelow(this float? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this double @this, double min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this double? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this decimal @this, decimal min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this decimal? @this, decimal min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this decimal @this, double min, string name = null) {
			ThrowIfBelow(@this, (decimal)min, name: name);
		}

		public static void ThrowIfBelow(this decimal? @this, double min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this decimal @this, int min, string name = null) {
			ThrowIfBelow(@this, (decimal)min, name: name);
		}

		public static void ThrowIfBelow(this decimal? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this sbyte @this, sbyte min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this sbyte? @this, sbyte min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this sbyte @this, int min, string name = null) {
			ThrowIfBelow(@this, (sbyte)min, name: name);
		}

		public static void ThrowIfBelow(this sbyte? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this short @this, short min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this short? @this, short min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this short @this, int min, string name = null) {
			ThrowIfBelow(@this, (short)min, name: name);
		}

		public static void ThrowIfBelow(this short? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this int @this, int min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this int? @this, int min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this long @this, long min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this long? @this, long min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this byte @this, byte min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this byte? @this, byte min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this byte @this, uint min, string name = null) {
			ThrowIfBelow(@this, (byte)min, name: name);
		}

		public static void ThrowIfBelow(this byte? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this ushort @this, ushort min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this ushort? @this, ushort min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this ushort @this, uint min, string name = null) {
			ThrowIfBelow(@this, (ushort)min, name: name);
		}

		public static void ThrowIfBelow(this ushort? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this uint @this, uint min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this uint? @this, uint min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow(this ulong @this, ulong min, string name = null) {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow(this ulong? @this, ulong min, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}

		public static void ThrowIfBelow<TValue>(this TValue @this, TValue min, string name = null) where TValue : struct, IComparable<TValue> {
			if (@this.CompareTo(min) < 0) throw ThrowIfBelowException(min, name: name);
		}

		public static void ThrowIfBelow<TValue>(this TValue? @this, TValue min, string name = null) where TValue : struct, IComparable<TValue> {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfBelow(min, name: name);
		}
	}
}
