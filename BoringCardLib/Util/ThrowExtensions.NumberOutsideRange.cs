using System;
using System.Diagnostics;

namespace BoringCardLib {
	internal static partial class Extensions {
		private static ArgumentException ThrowIfOutsideRangeException<TValue>(TValue min, TValue max, string name) {
			return new ArgumentException($"Must be in range [{min}, {max}]", name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float @this, float min, float max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float? @this, float min, float max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float @this, float min, double max, string name = null) {
			ThrowIfOutsideRange(@this, min, (float)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float? @this, float min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float @this, double min, float max, string name = null) {
			ThrowIfOutsideRange(@this, (float)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float? @this, double min, float max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float @this, double min, double max, string name = null) {
			ThrowIfOutsideRange(@this, (float)min, (float)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this float? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this double @this, double min, double max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this double? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, decimal min, decimal max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, decimal min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, decimal min, double max, string name = null) {
			ThrowIfOutsideRange(@this, min, (decimal)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, decimal min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, double min, decimal max, string name = null) {
			ThrowIfOutsideRange(@this, (decimal)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, double min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, double min, double max, string name = null) {
			ThrowIfOutsideRange(@this, (decimal)min, (decimal)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, double min, double max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, decimal min, int max, string name = null) {
			ThrowIfOutsideRange(@this, min, (decimal)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, decimal min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, int min, decimal max, string name = null) {
			ThrowIfOutsideRange(@this, (decimal)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, int min, decimal max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal @this, int min, int max, string name = null) {
			ThrowIfOutsideRange(@this, (decimal)min, (decimal)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this decimal? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte @this, sbyte min, sbyte max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte? @this, sbyte min, sbyte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte @this, sbyte min, int max, string name = null) {
			ThrowIfOutsideRange(@this, min, (sbyte)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte? @this, sbyte min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte @this, int min, sbyte max, string name = null) {
			ThrowIfOutsideRange(@this, (sbyte)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte? @this, int min, sbyte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte @this, int min, int max, string name = null) {
			ThrowIfOutsideRange(@this, (sbyte)min, (sbyte)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this sbyte? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short @this, short min, short max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short? @this, short min, short max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short @this, short min, int max, string name = null) {
			ThrowIfOutsideRange(@this, min, (short)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short? @this, short min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short @this, int min, short max, string name = null) {
			ThrowIfOutsideRange(@this, (short)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short? @this, int min, short max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short @this, int min, int max, string name = null) {
			ThrowIfOutsideRange(@this, (short)min, (short)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this short? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this int @this, int min, int max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this int? @this, int min, int max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this long @this, long min, long max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this long? @this, long min, long max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte @this, byte min, byte max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte? @this, byte min, byte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte @this, byte min, uint max, string name = null) {
			ThrowIfOutsideRange(@this, min, (byte)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte? @this, byte min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte @this, uint min, byte max, string name = null) {
			ThrowIfOutsideRange(@this, (byte)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte? @this, uint min, byte max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte @this, uint min, uint max, string name = null) {
			ThrowIfOutsideRange(@this, (byte)min, (byte)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this byte? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort @this, ushort min, ushort max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort? @this, ushort min, ushort max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort @this, ushort min, uint max, string name = null) {
			ThrowIfOutsideRange(@this, min, (ushort)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort? @this, ushort min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort @this, uint min, ushort max, string name = null) {
			ThrowIfOutsideRange(@this, (ushort)min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort? @this, uint min, ushort max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort @this, uint min, uint max, string name = null) {
			ThrowIfOutsideRange(@this, (ushort)min, (ushort)max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ushort? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this uint @this, uint min, uint max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this uint? @this, uint min, uint max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ulong @this, ulong min, ulong max, string name = null) {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange(this ulong? @this, ulong min, ulong max, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange<TValue>(this TValue @this, TValue min, TValue max, string name = null) where TValue : struct, IComparable<TValue> {
			if (@this.CompareTo(min) < 0 || @this.CompareTo(max) > 0) throw ThrowIfOutsideRangeException(min, max, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfOutsideRange<TValue>(this TValue? @this, TValue min, TValue max, string name = null) where TValue : struct, IComparable<TValue> {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfOutsideRange(min, max, name: name);
		}
	}
}
