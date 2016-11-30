using System;
using System.Collections.Generic;
using System.Linq;
using BoringCardLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BoringCardLib.Tests {
	[TestClass]
	public class DefaultCardComparerTest {
		private static readonly DefaultCardComparer Instance = DefaultCardComparer.Instance;

		[TestMethod]
		public void Construct() {
			Assert.DoesNotThrow(() => new DefaultCardComparer());
		}

		[TestMethod]
		public void Compare() {
			Assert.True(Instance.Compare(null, null) == 0);
			Assert.True(Instance.Compare(null, Card.Joker) == -1);
			Assert.True(Instance.Compare(Card.Joker, null) == 1);
			Assert.True(Instance.Compare(Card.Joker, Card.Joker) == 0);

			var aceSpades = new Card(Suit.Spades, Rank.Ace);
			var aceHearts = new Card(Suit.Hearts, Rank.Ace);
			var twoHearts = new Card(Suit.Hearts, Rank.Two);
			Assert.True(Instance.Compare(aceSpades, aceHearts) == 1);
			Assert.True(Instance.Compare(aceHearts, aceSpades) == -1);
			Assert.True(Instance.Compare(twoHearts, aceHearts) == -1);
			Assert.True(Instance.Compare(aceHearts, twoHearts) == 1);
		}

		[TestMethod]
		public void Equals() {
			Assert.True(Instance.Equals(null, null));
			Assert.False(Instance.Equals(null, Card.Joker));
			Assert.False(Instance.Equals(Card.Joker, null));
			Assert.True(Instance.Equals(Card.Joker, Card.Joker));

			var aceSpades = new Card(Suit.Spades, Rank.Ace);
			var aceHearts = new Card(Suit.Hearts, Rank.Ace);
			var twoHearts = new Card(Suit.Hearts, Rank.Two);
			Assert.False(Instance.Equals(aceSpades, aceHearts));
			Assert.False(Instance.Equals(aceHearts, aceSpades));
			Assert.False(Instance.Equals(twoHearts, aceHearts));
			Assert.False(Instance.Equals(aceHearts, twoHearts));
		}

		[TestMethod]
		public new void GetHashCode() {
			Assert.ThrowsExact<ArgumentNullException>(() => Instance.GetHashCode(null));
			Assert.True(Instance.GetHashCode(Card.Joker) == Card.Joker.GetHashCode());

			var aceSpades = new Card(Suit.Spades, Rank.Ace);
			var aceHearts = new Card(Suit.Hearts, Rank.Ace);
			var twoHearts = new Card(Suit.Hearts, Rank.Two);
			Assert.True(Instance.GetHashCode(aceSpades) == aceSpades.GetHashCode());
			Assert.True(Instance.GetHashCode(aceHearts) == aceHearts.GetHashCode());
			Assert.True(Instance.GetHashCode(twoHearts) == twoHearts.GetHashCode());

			Assert.False(Instance.GetHashCode(aceSpades) == aceHearts.GetHashCode());
			Assert.False(Instance.GetHashCode(aceSpades) == twoHearts.GetHashCode());
			Assert.False(Instance.GetHashCode(aceHearts) == aceSpades.GetHashCode());
			Assert.False(Instance.GetHashCode(aceHearts) == twoHearts.GetHashCode());
			Assert.False(Instance.GetHashCode(twoHearts) == aceSpades.GetHashCode());
			Assert.False(Instance.GetHashCode(twoHearts) == aceHearts.GetHashCode());
		}
	}
}
