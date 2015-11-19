using System;
using System.Collections;
using System.Collections.Generic;

namespace BoringCardLib {
	internal sealed class ValueEqualityTester {
		private bool AnyUnequal { get; set; }

		public ValueEqualityTester() { AnyUnequal = false; }

		private static Type sCollectionType = typeof(ICollection);
		private static Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type pType) { return sCollectionType.IsAssignableFrom(pType) || sCollectionGenericType.IsAssignableFrom(pType); }

		public ValueEqualityTester Equal(object pLeftSide, object pRightSide) {
			if (AnyUnequal || object.ReferenceEquals(pLeftSide, pRightSide)) return this;

			if (pLeftSide == null || pRightSide == null) return this.Equal(false);

			Type left = pLeftSide.GetType();
			Type right = pRightSide.GetType();
			bool isCollectionType = IsCollectionType(left);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(right)) return this.Equal(false);

			if (isCollectionType) {
				if (left != right) {
					// This is a complicated subject, but we'll say that if the collection types are not equal, they are not value-equal
					// So for example, a List<int> and an int[] won't be equal even if they both have 1, 2, 3
					// I think this is okay
					return this.Equal(false);
				}

				var leftCollection = (pLeftSide as ICollection);
				var rightCollection = (pRightSide as ICollection);
				if (leftCollection.Count != rightCollection.Count) return this.Equal(false);

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					Equals(leftIter.Current, rightIter.Current);
					if (AnyUnequal) return this;
				}
			}
			else {
				var equalityChecker = pLeftSide as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRightSide) != 0) return this.Equal(false);
				}
				else if (!pLeftSide.Equals(pRightSide)) return this.Equal(false);
			}
			return this;
		}

		public ValueEqualityTester Equal<TElement>(TElement pLeftSide, TElement pRightSide, IEqualityComparer<TElement> pComparer) {
			if (AnyUnequal || object.ReferenceEquals(pLeftSide, pRightSide)) return this;

			if (pLeftSide == null || pRightSide == null) return this.Equal(false);
			if (pComparer.GetHashCode(pLeftSide) != pComparer.GetHashCode(pRightSide)) return this.Equal(false);
			if (!pComparer.Equals(pLeftSide, pRightSide)) return this.Equal(false);
			return this;
		}

		public ValueEqualityTester Equal(bool pEqual) {
			if (pEqual) return this;
			AnyUnequal = true;
			return this;
		}

		public bool All() { return !AnyUnequal; }
	}
}
