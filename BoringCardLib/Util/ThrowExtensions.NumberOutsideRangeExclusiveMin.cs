using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		private static ArgumentException ThrowIfOutsideRangeExclusiveMinException<TValue>(TValue min, TValue max, string name) {
			return new ArgumentException($"Must be in range ({min}, {max}]", name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float @this, float min, float max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float? @this, float min, float max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float @this, float min, double max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (float)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float? @this, float min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float @this, double min, float max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (float)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float? @this, double min, float max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float @this, double min, double max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (float)min, (float)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this float? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this double @this, double min, double max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this double? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, decimal min, decimal max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, decimal min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, decimal min, double max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (decimal)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, decimal min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, double min, decimal max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (decimal)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, double min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, double min, double max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (decimal)min, (decimal)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, decimal min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (decimal)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, decimal min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, int min, decimal max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (decimal)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, int min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal @this, int min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (decimal)min, (decimal)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this decimal? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte @this, sbyte min, sbyte max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte? @this, sbyte min, sbyte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte @this, sbyte min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (sbyte)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte? @this, sbyte min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte @this, int min, sbyte max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (sbyte)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte? @this, int min, sbyte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte @this, int min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (sbyte)min, (sbyte)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this sbyte? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short @this, short min, short max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short? @this, short min, short max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short @this, short min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (short)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short? @this, short min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short @this, int min, short max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (short)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short? @this, int min, short max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short @this, int min, int max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (short)min, (short)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this short? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this int @this, int min, int max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this int? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this long @this, long min, long max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this long? @this, long min, long max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte @this, byte min, byte max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte? @this, byte min, byte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte @this, byte min, uint max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (byte)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte? @this, byte min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte @this, uint min, byte max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (byte)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte? @this, uint min, byte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte @this, uint min, uint max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (byte)min, (byte)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this byte? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort @this, ushort min, ushort max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort? @this, ushort min, ushort max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort @this, ushort min, uint max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, min, (ushort)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort? @this, ushort min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort @this, uint min, ushort max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (ushort)min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort? @this, uint min, ushort max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort @this, uint min, uint max, string name = null) {
			ThrowIfOutsideRangeExclusiveMin(@this, (ushort)min, (ushort)max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ushort? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this uint @this, uint min, uint max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this uint? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ulong @this, ulong min, ulong max, string name = null) {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin(this ulong? @this, ulong min, ulong max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin<TValue>(this TValue @this, TValue min, TValue max, string name = null) where TValue : struct, IComparable<TValue> {
			if (@this.CompareTo(min) <= 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeExclusiveMinException(min, max, name: name);
		}

		public static void ThrowIfOutsideRangeExclusiveMin<TValue>(this TValue? @this, TValue min, TValue max, string name = null) where TValue : struct, IComparable<TValue> {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRangeExclusiveMin(min, max, name: name);
		}
	}
}
