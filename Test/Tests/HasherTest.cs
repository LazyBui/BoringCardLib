using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoringCardLib;

namespace Test {
	[TestClass]
	public class HasherTest {
		// Testing this is a little nebulous without tying to specific hashes (which is a very bad idea)
		// In general, we're only testing the behavior of the hasher here - that it changes hashes and produces consistent results

		[TestMethod]
		public void Hasher() {
			Assert.DoesNotThrow(() => new Hasher());
		}

		[TestMethod]
		public void BasicHash() {
			int hash = 0;
			int oldHash = 0;
			var hasher = new Hasher();

			oldHash = hasher.Hash();
			Assert.DoesNotThrow(() => hasher.Combine(5));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(5));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(null as string));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);
		}

		[TestMethod]
		public void ConsistentBasicHash() {
			var hasher = new Hasher();
			var hasher2 = new Hasher();

			Assert.DoesNotThrow(() => {
				hasher.
					Combine(1).
					Combine(2).
					Combine(8).
					Combine(null as string).
					Combine(9).
					Combine(7).
					Combine("hello").
					Combine("HELLO");

				hasher2.
					Combine(1).
					Combine(2).
					Combine(8).
					Combine(null as string).
					Combine(9).
					Combine(7).
					Combine("hello").
					Combine("HELLO");
			});
			Assert.Equal(hasher.Hash(), hasher2.Hash());

			hasher = new Hasher();
			hasher2 = new Hasher();
			Assert.DoesNotThrow(() => {
				hasher.
					Combine(1).
					Combine(2);

				hasher2.
					Combine(2).
					Combine(1);
			});
			Assert.NotEqual(hasher.Hash(), hasher2.Hash());
		}

		[TestMethod]
		public void ComparerHash() {
			int hash = 0;
			int oldHash = 0;
			var hasher = new Hasher();

			oldHash = hasher.Hash();
			Assert.DoesNotThrow(() => hasher.Combine(null as string, StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(null as string, StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine("hello", StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);
		}

		[TestMethod]
		public void ConsistentComparerHash() {
			var hasher = new Hasher();
			var hasher2 = new Hasher();

			Assert.DoesNotThrow(() => {
				hasher.
					Combine(null as string, StringComparer.InvariantCultureIgnoreCase).
					Combine("hello", StringComparer.InvariantCultureIgnoreCase).
					Combine("HELLO", StringComparer.InvariantCultureIgnoreCase);

				hasher2.
					Combine(null as string, StringComparer.InvariantCultureIgnoreCase).
					Combine("hello", StringComparer.InvariantCultureIgnoreCase).
					Combine("hello", StringComparer.InvariantCultureIgnoreCase);
			});
			Assert.Equal(hasher.Hash(), hasher2.Hash());

			hasher = new Hasher();
			hasher2 = new Hasher();
			Assert.DoesNotThrow(() => {
				hasher.
					Combine("hello", StringComparer.InvariantCultureIgnoreCase).
					Combine(null as string, StringComparer.InvariantCultureIgnoreCase).
					Combine("HELLO", StringComparer.InvariantCultureIgnoreCase);

				hasher2.
					Combine(null as string, StringComparer.InvariantCultureIgnoreCase).
					Combine("hello", StringComparer.InvariantCultureIgnoreCase).
					Combine("hello", StringComparer.InvariantCultureIgnoreCase);
			});
			Assert.NotEqual(hasher.Hash(), hasher2.Hash());
		}

		[TestMethod]
		public void EnumerableHash() {
			int hash = 0;
			int oldHash = 0;
			var hasher = new Hasher();

			oldHash = hasher.Hash();
			Assert.DoesNotThrow(() => hasher.Combine(new int[] { }));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(null as int[]));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(null as int[]));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(new List<int>() { 1, 2, 3, 4 }));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);

			oldHash = hash;
			Assert.DoesNotThrow(() => hasher.Combine(new int[] { 1, 2, 3, 4 }));
			Assert.DoesNotThrow(() => hash = hasher.Hash());
			Assert.NotEqual(oldHash, hash);
		}

		[TestMethod]
		public void ConsistentEnumerableHash() {
			var hasher = new Hasher();
			var hasher2 = new Hasher();

			Assert.DoesNotThrow(() => {
				hasher.
					Combine(new List<int>() { 1, 2, 3, 4, 5 }).
					Combine(null).
					Combine(8).
					Combine(new List<int>() { 5, 4, 3, 2, 1 });

				hasher2.
					Combine(new List<int>() { 1, 2, 3, 4, 5 }).
					Combine(null).
					Combine(8).
					Combine(new List<int>() { 5, 4, 3, 2, 1 });
			});
			Assert.Equal(hasher.Hash(), hasher2.Hash());

			hasher = new Hasher();
			hasher2 = new Hasher();
			Assert.DoesNotThrow(() => {
				hasher.
					Combine(new int[] { 5, 4, 3, 2, 1 }).
					Combine(8).
					Combine(null).
					Combine(new int[] { 1, 2, 3, 4, 5 });

				hasher2.
					Combine(new int[] { 1, 2, 3, 4, 5 }).
					Combine(null).
					Combine(8).
					Combine(new int[] { 5, 4, 3, 2, 1 });
			});
			Assert.NotEqual(hasher.Hash(), hasher2.Hash());

			// This test may be a bit confusing
			// In order for these two value-equality things to compare inconsistent, the type must be factored in because they are otherwise identical.
			// It's not clear that accounting for this case is worthwhile when the hash codes are almost strictly used to determine loose equality.
			// That is, it's only a problem if the same thing comes up with a different hashcode, which is why consistency is something we're testing for in general.
			// Different things coming up with the same hashcode is expected and that's why it falls back to full equality, where the type will be taken into consideration.
			hasher = new Hasher();
			hasher2 = new Hasher();
			Assert.DoesNotThrow(() => {
				hasher.
					Combine(new int[] { 5, 4, 3, 2, 1 }).
					Combine(8).
					Combine(null).
					Combine(new int[] { 1, 2, 3, 4, 5 });

				hasher2.
					Combine(new List<int>() { 5, 4, 3, 2, 1 }).
					Combine(8).
					Combine(null).
					Combine(new List<int>() { 1, 2, 3, 4, 5 });
			});
			Assert.Equal(hasher.Hash(), hasher2.Hash());
		}
	}
}
