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

		public Hasher Combine<TElement>(IEnumerable<TElement> value) {
			Combine(0);
			if (value == null) return this;
			foreach (var data in value) {
				Combine(data);
			}
			return this;
		}

		public Hasher Combine<TElement>(IEnumerable<TElement> value, IEqualityComparer<TElement> comparer) {
			Combine(0);
			if (object.ReferenceEquals(value, null)) return this;
			foreach (var data in value) {
				Combine(data, comparer);
			}
			return this;
		}

		public Hasher Combine(object value) {
			int hashCode = 0;
			if (!object.ReferenceEquals(value, null)) hashCode = value.GetHashCode();
			return Combine(hashCode);
		}

		public Hasher Combine<TElement>(TElement value, IEqualityComparer<TElement> comparer) {
			int hashCode = 0;
			if (!object.ReferenceEquals(value, null)) hashCode = comparer.GetHashCode(value);
			return Combine(hashCode);
		}

		public Hasher Combine(int hash) {
			unchecked { CurrentHashValue = (CurrentHashValue * (int)0xA5555529) + hash; }
			return this;
		}

		public int Hash() { return CurrentHashValue; }
	}
}
