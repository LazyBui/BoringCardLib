using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BoringCardLib {
	internal sealed class Hasher {
		private static int sBaseHashValue;

		static Hasher() {
			RefreshBaseHash();
		}

		// This code ensures that hashes change every program execution so users may not persist them across program sessions
		private static void RefreshBaseHash() {
			var bitGenerator = new RNGCryptoServiceProvider();
			byte[] intValue = new byte[sizeof(int)];
			bitGenerator.GetBytes(intValue);
			sBaseHashValue = BitConverter.ToInt32(intValue, 0);
		}

		private int CurrentHashValue { get; set; }

		public Hasher() { CurrentHashValue = sBaseHashValue; }

		public Hasher Combine<TElement>(IEnumerable<TElement> pValue) {
			this.Combine(0);
			if (pValue == null) return this;
			foreach (var data in pValue) {
				this.Combine(data);
			}
			return this;
		}

		public Hasher Combine<TElement>(IEnumerable<TElement> pValue, IEqualityComparer<TElement> pComparer) {
			this.Combine(0);
			if (pValue == null) return this;
			foreach (var data in pValue) {
				this.Combine(data, pComparer);
			}
			return this;
		}

		public Hasher Combine(object pValue) {
			int hashCode = 0;
			if (pValue != null) hashCode = pValue.GetHashCode();
			return this.Combine(hashCode);
		}

		public Hasher Combine<TElement>(TElement pValue, IEqualityComparer<TElement> pComparer) {
			int hashCode = 0;
			if (pValue != null) hashCode = pComparer.GetHashCode(pValue);
			return this.Combine(hashCode);
		}

		public Hasher Combine(int pHashCode) {
			unchecked { CurrentHashValue = (CurrentHashValue * (int)0xA5555529) + pHashCode; }
			return this;
		}

		public int Hash() { return CurrentHashValue; }
	}
}
