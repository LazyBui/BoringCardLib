﻿using System;

namespace BoringCardLib {
	internal static partial class Extensions {
		public static void ThrowIfNegative(this float @this, string name = null) {
			@this.ThrowIfBelow(0.0f, name: name);
		}

		public static void ThrowIfNegative(this float? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this double @this, string name = null) {
			@this.ThrowIfBelow(0.0, name: name);
		}

		public static void ThrowIfNegative(this double? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this decimal @this, string name = null) {
			@this.ThrowIfBelow(0.0m, name: name);
		}

		public static void ThrowIfNegative(this decimal? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this sbyte @this, string name = null) {
			@this.ThrowIfBelow((sbyte)0, name: name);
		}

		public static void ThrowIfNegative(this sbyte? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this short @this, string name = null) {
			@this.ThrowIfBelow((short)0, name: name);
		}

		public static void ThrowIfNegative(this short? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this int @this, string name = null) {
			@this.ThrowIfBelow(0, name: name);
		}

		public static void ThrowIfNegative(this int? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}

		public static void ThrowIfNegative(this long @this, string name = null) {
			@this.ThrowIfBelow(0, name: name);
		}

		public static void ThrowIfNegative(this long? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfNegative(name: name);
		}
	}
}
