using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoringCardLib;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class ValueEqualityTesterTest {
		[TestMethod]
		public void ValueEqualityTester() {
			Assert.DoesNotThrow(() => new ValueEqualityTester());
		}

		[TestMethod]
		public void BoolOverload() {
			ValueEqualityTester tester = null;
			bool equal = false;

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(1 == 1));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(1 != 1));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);
		}

		[TestMethod]
		public void ObjectOverload() {
			ValueEqualityTester tester = null;
			bool equal = false;

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(5, 5));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(5, 6));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.
				Equal(5, 5).
				Equal(5, 6));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(null, "hi"));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(null, null));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new int[] { }, new List<int>() { }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new int[] { }, new int[] { }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new int[] { 1 }, new int[] { }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new int[] { 1 }, new int[] { 1 }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new[] { "abc" }, new[] { "abc" }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(new[] { "abc" }, new[] { string.Empty }));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);
		}

		[TestMethod]
		public void ObjectOverloadComparer() {
			ValueEqualityTester tester = null;
			bool equal = false;

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(null, null, StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal(null, "hi", StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.False(equal);

			tester = new ValueEqualityTester();
			Assert.DoesNotThrow(() => tester.Equal("HI", "hi", StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => equal = tester.All());
			Assert.True(equal);
		}		
	}
}
