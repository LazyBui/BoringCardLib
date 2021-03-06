﻿using System;
using System.Diagnostics;

namespace BoringCardLib {
	internal static partial class Extensions {
		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this float @this, string name = null) {
			@this.ThrowIfBelowOrEqual(0.0f, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this float? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this double @this, string name = null) {
			@this.ThrowIfBelowOrEqual(0.0, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this double? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this decimal @this, string name = null) {
			@this.ThrowIfBelowOrEqual(0.0m, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this decimal? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this sbyte @this, string name = null) {
			@this.ThrowIfBelowOrEqual((sbyte)0, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this sbyte? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this short @this, string name = null) {
			@this.ThrowIfBelowOrEqual((short)0, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this short? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this int @this, string name = null) {
			@this.ThrowIfBelowOrEqual(0, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this int? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this long @this, string name = null) {
			@this.ThrowIfBelowOrEqual(0, name: name);
		}

		[DebuggerStepThrough]
		public static void ThrowIfZeroOrLess(this long? @this, string name = null) {
			if (!@this.HasValue) return;
			@this.GetValueOrDefault().ThrowIfZeroOrLess(name: name);
		}
	}
}
