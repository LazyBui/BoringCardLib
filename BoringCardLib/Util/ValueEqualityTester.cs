using System;
using System.Collections;
using System.Collections.Generic;

namespace BoringCardLib {
	internal sealed class ValueEqualityTester {
		private bool AnyUnequal { get; set; }

		public ValueEqualityTester() { AnyUnequal = false; }

		private static Type sCollectionType = typeof(ICollection);
		private static Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type type) { return sCollectionType.IsAssignableFrom(type) || sCollectionGenericType.IsAssignableFrom(type); }

		public ValueEqualityTester Equal(object left, object right) {
			if (AnyUnequal || object.ReferenceEquals(left, right)) return this;

			if (left.IsNull() || right.IsNull()) return Equal(false);

			Type leftType = left.GetType();
			Type rightType = right.GetType();
			bool isCollectionType = IsCollectionType(leftType);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(rightType)) return Equal(false);

			if (isCollectionType) {
				if (leftType != rightType) {
					// This is a complicated subject, but we'll say that if the collection types are not equal, they are not value-equal
					// So for example, a List<int> and an int[] won't be equal even if they both have 1, 2, 3
					// I think this is okay
					return Equal(false);
				}

				var leftCollection = left as ICollection;
				var rightCollection = right as ICollection;
				if (leftCollection.Count != rightCollection.Count) return Equal(false);

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					Equal(leftIter.Current, rightIter.Current);
					if (AnyUnequal) return this;
				}
			}
			else {
				var equalityChecker = left as IComparable;
				if (equalityChecker.IsNotNull()) {
					if (equalityChecker.CompareTo(right) != 0) return Equal(false);
				}
				else if (!left.Equals(right)) return Equal(false);
			}
			return this;
		}

		public ValueEqualityTester Equal<TElement>(TElement left, TElement right, IEqualityComparer<TElement> comparer) {
			if (AnyUnequal || object.ReferenceEquals(left, right)) return this;

			if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null)) return Equal(false);
			if (comparer.GetHashCode(left) != comparer.GetHashCode(right)) return Equal(false);
			if (!comparer.Equals(left, right)) return Equal(false);
			return this;
		}

		public ValueEqualityTester Equal(bool isEqual) {
			if (isEqual) return this;
			AnyUnequal = true;
			return this;
		}

		public bool All() { return !AnyUnequal; }
	}
}
