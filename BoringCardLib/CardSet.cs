using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Represents a mutable set of playing cards.
	/// </summary>
	public sealed class CardSet : IEnumerable<Card>, IEnumerable {
		private List<Card> mCards = null;
		private static readonly Suit[] sSuitOrdering = new[] {
			Suit.Hearts,
			Suit.Clubs,
			Suit.Diamonds,
			Suit.Spades,
		};
		private static readonly Rank[] sRankOrdering = new[] {
			Rank.Ace,
			Rank.Two,
			Rank.Three,
			Rank.Four,
			Rank.Five,
			Rank.Six,
			Rank.Seven,
			Rank.Eight,
			Rank.Nine,
			Rank.Ten,
			Rank.Jack,
			Rank.Queen,
			Rank.King,
		};

		/// <summary>
		/// Indicates whether there are any cards in the set.
		/// </summary>
		public bool AnyCards { get { return Count > 0; } }
		/// <summary>
		/// Indicates whether how many cards in the set.
		/// </summary>
		public int Count { get { return mCards.Count; } }
		/// <summary>
		/// Gets the first card of the set.
		/// </summary>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card First {
			get {
				if (Count == 0) throw new InvalidOperationException("Must have cards to view the first");
				return mCards[0];
			}
		}
		/// <summary>
		/// Gets the last card of the set.
		/// </summary>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card Last {
			get {
				if (Count == 0) throw new InvalidOperationException("Must have cards to view the last");
				return mCards[mCards.Count - 1];
			}
		}
		/// <summary>
		/// Gets the element at the specified zero-based index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified zero-based index.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not in the range [0, <see cref="Count" />).</exception>
		public Card this[int index] { get { return mCards[index]; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="CardSet" /> class.
		/// </summary>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public CardSet(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards = new List<Card>(cards);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CardSet" /> class.
		/// </summary>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public CardSet(params Card[] cards) : this(cards as IEnumerable<Card>) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CardSet" /> class.
		/// </summary>
		/// <param name="capacity">The expected capacity of the set to minimize reallocations.</param>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="capacity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public CardSet(int capacity, IEnumerable<Card> cards) {
			capacity.ThrowIfZeroOrLess(nameof(capacity));
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards = new List<Card>(capacity);
			mCards.AddRange(cards);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CardSet" /> class.
		/// </summary>
		/// <param name="capacity">The expected capacity of the set to minimize reallocations.</param>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="capacity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public CardSet(int capacity, params Card[] cards) : this(capacity, cards as IEnumerable<Card>) { }

		/// <summary>
		/// Generates a standard deck with the specified adjustments.
		/// </summary>
		/// <param name="jokerCount">Indicates how many jokers the deck should contain.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="CardSet" /> instance representing a standard deck.</returns>
		/// <exception cref="ArgumentException"><paramref name="jokerCount" /> is negative.</exception>
		public static CardSet MakeStandardDeck(int jokerCount = 0, Func<Card, Card> initializeValues = null) {
			jokerCount.ThrowIfNegative(nameof(jokerCount));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			int suits = 0;
			foreach (var suit in sSuitOrdering) {
				if (suit == Suit.Joker) continue;
				cards.AddRange(suits > 1 ?
					MakeFullSuit(suit, initializeValues: initializeValues).Reverse<Card>() :
					MakeFullSuit(suit, initializeValues: initializeValues));
				suits++;
			}
			if (jokerCount > 0) {
				for (int i = 0; i < jokerCount; i++) {
					cards.Add(initializeValues(Card.Joker.Duplicate()));
				}
			}
			return new CardSet(cards);
		}

		/// <summary>
		/// Generates a standard full suit with the specified adjustments.
		/// </summary>
		/// <param name="suit">The suit to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="CardSet" /> instance representing a standard suit.</returns>
		/// <exception cref="ArgumentException"><paramref name="suit" /> is invalid or <see cref="Suit.Joker" />.</exception>
		public static CardSet MakeFullSuit(Suit suit, Func<Card, Card> initializeValues = null) {
			suit.ThrowIfInvalid(nameof(suit));
			if (suit == Suit.Joker) throw new ArgumentException("Must not be a joker", nameof(suit));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			foreach (var rank in sRankOrdering) {
				if (rank == Rank.Joker) continue;
				cards.Add(initializeValues(new Card(suit, rank)));
			}
			return new CardSet(cards);
		}

		/// <summary>
		/// Generates a standard full rank with the specified adjustments.
		/// </summary>
		/// <param name="rank">The rank to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="CardSet" /> instance representing a standard rank.</returns>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid or <see cref="Rank.Joker" />.</exception>
		public static CardSet MakeFullRank(Rank rank, Func<Card, Card> initializeValues = null) {
			rank.ThrowIfInvalid(nameof(rank));
			if (rank == Rank.Joker) throw new ArgumentException("Must not be a joker", nameof(rank));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			foreach (var suit in sSuitOrdering) {
				if (suit == Suit.Joker) continue;
				cards.Add(initializeValues(new Card(suit, rank)));
			}
			return new CardSet(cards);
		}

		/// <summary>
		/// Generates a set of jokers.
		/// </summary>
		/// <param name="jokerCount">The quantity of jokers to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="CardSet" /> instance representing the jokers.</returns>
		/// <exception cref="ArgumentException"><paramref name="jokerCount" /> is 0 or less.</exception>
		public static CardSet MakeJokers(int jokerCount, Func<Card, Card> initializeValues = null) {
			jokerCount.ThrowIfZeroOrLess(nameof(jokerCount));
			if (initializeValues == null) initializeValues = c => c;

			var cards = new List<Card>();
			for (int i = 0; i < jokerCount; i++) {
				cards.Add(initializeValues(Card.Joker.Duplicate()));
			}
			return new CardSet(cards);
		}

		/// <summary>
		/// Adds cards to the set before <see cref="First" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Prepend(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			for (int i = cards.Count() - 1; i >= 0; i--) {
				mCards.Insert(0, cards.ElementAt(i));
			}
		}

		/// <summary>
		/// Adds cards to the set before <see cref="First" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Prepend(params Card[] cards) {
			Prepend(cards as IEnumerable<Card>);
		}

		/// <summary>
		/// Adds cards to the set after <see cref="Last" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Append(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			mCards.AddRange(cards.ToList());
		}

		/// <summary>
		/// Adds cards to the set after <see cref="Last" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Append(params Card[] cards) {
			Append(cards as IEnumerable<Card>);
		}

		/// <summary>
		/// Adds cards to the set at the specified index.
		/// </summary>
		/// <param name="index">The index to insert cards at.</param>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="index" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Insert(int index, IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			index.ThrowIfOutsideRange(0, Count, nameof(index));
			mCards =
				mCards.Take(index).
				Concat(cards).
				Concat(mCards.Skip(index)).
				ToList();
		}

		/// <summary>
		/// Adds cards to the set at the specified index.
		/// </summary>
		/// <param name="index">The index to insert cards at.</param>
		/// <param name="cards">The cards to add to the set.</param>
		/// <exception cref="ArgumentException"><paramref name="index" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public void Insert(int index, params Card[] cards) {
			Insert(index, cards as IEnumerable<Card>);
		}

		/// <summary>
		/// Randomizes the ordering of the cards in the set using a <see cref="DefaultRandomSource" /> instance.
		/// </summary>
		/// <param name="times">The quantity of times to shuffle.</param>
		/// <exception cref="ArgumentException"><paramref name="times" /> is 0 or less.</exception>
		public void Shuffle(int times = 1) {
			Shuffle(new DefaultRandomSource(), times: times);
		}

		/// <summary>
		/// Randomizes the ordering of the cards in the set using the specified <see cref="IRandomSource" />.
		/// </summary>
		/// <param name="sampler">The source of randomness.</param>
		/// <param name="times">The quantity of times to shuffle.</param>
		/// <exception cref="ArgumentException"><paramref name="times" /> is 0 or less.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sampler" /> is null.</exception>
		public void Shuffle(IRandomSource sampler, int times = 1) {
			sampler.ThrowIfNull(nameof(sampler));
			times.ThrowIfZeroOrLess(nameof(times));

			int n = mCards.Count;
			do {
				for (int i = 0; i < n; i++) {
					var temp = mCards[i];
					int r = i + sampler.SampleInt32(n - i);
					mCards[i] = mCards[r];
					mCards[r] = temp;
				}
			} while (--times > 0);
		}

		/// <summary>
		/// Retrieves a random card from the set using a <see cref="DefaultRandomSource" /> instance.
		/// </summary>
		/// <returns>A random <see cref="Card" /> instance from the set.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card GetRandom() {
			return GetRandom(new DefaultRandomSource());
		}

		/// <summary>
		/// Retrieves a random card from the set using the specified <see cref="IRandomSource" />.
		/// </summary>
		/// <param name="sampler">The source of randomness.</param>
		/// <returns>A random <see cref="Card" /> instance from the set.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="sampler" /> is null.</exception>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card GetRandom(IRandomSource sampler) {
			sampler.ThrowIfNull(nameof(sampler));
			if (Count == 0) throw new InvalidOperationException("Must have cards to return a random card");
			return this[sampler.SampleInt32(Count)];
		}

		/// <summary>
		/// Determines whether the current set of cards has the specified <see cref="Card" /> using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <returns>true if the current set of cards contains <paramref name="card" />, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public bool Contains(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.Contains(card);
		}
		
		/// <summary>
		/// Determines whether the current set of cards has the specified <see cref="Card" /> using a specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <param name="comparer">The comparer to check with.</param>
		/// <returns>true if the current set of cards contains <paramref name="card" />, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public bool Contains(Card card, IEqualityComparer<Card> comparer) {
			card.ThrowIfNull(nameof(card));
			comparer.ThrowIfNull(nameof(comparer));
			return mCards.Contains(card, comparer);
		}
		
		/// <summary>
		/// Searches for the specified <see cref="Card" /> and returns a zero-based index of the first matched card using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <returns>A zero-based if the current set of cards contains <paramref name="card" />, otherwise -1.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public int IndexOf(Card card) {
			card.ThrowIfNull(nameof(card));
			return mCards.IndexOf(card);
		}

		/// <summary>
		/// Searches for the specified <see cref="Card" /> and returns a zero-based index of the first matched card using a specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <param name="comparer">The comparer to check with.</param>
		/// <returns>A zero-based if the current set of cards contains <paramref name="card" />, otherwise -1.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public int IndexOf(Card card, IEqualityComparer<Card> comparer) {
			card.ThrowIfNull(nameof(card));
			comparer.ThrowIfNull(nameof(comparer));
			for (int i = 0; i < mCards.Count; i++) {
				if (comparer.Equals(mCards[i], card)) {
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Draws one card from the top of the set, leaving the set without it.
		/// </summary>
		/// <returns>The result of <see cref="First" />.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card Draw() {
			if (mCards.Count == 0) throw new InvalidOperationException("Must have cards to draw");
			var result = mCards[0];
			mCards.RemoveAt(0);
			return result;
		}

		/// <summary>
		/// Draws the specified number of cards from the top of the set, leaving the set without them.
		/// If <paramref name="cards" /> is greater than <see cref="Count" />, it returns all cards remaining.
		/// </summary>
		/// <param name="cards">The quantity of cards to draw.</param>
		/// <returns>A new <see cref="CardSet" /> instance representing all cards in the set if <paramref name="cards" /> is greater than <see cref="Count" />, <see cref="Count" /> cards otherwise.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is 0 or less.</exception>
		public CardSet Draw(int cards) {
			cards.ThrowIfZeroOrLess(nameof(cards));
			if (cards >= Count) {
				var pile = new CardSet(mCards);
				mCards.Clear();
				return pile;
			}

			var result = mCards.Take(cards).ToList();
			for (int i = 0; i < cards; i++) {
				mCards.RemoveAt(0);
			}
			return new CardSet(result);
		}

		/// <summary>
		/// Replaces the specified card with the specified replacement using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to find.</param>
		/// <param name="replacement">The card to replace with.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replacement" /> is null.</exception>
		public ReplaceResult Replace(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(DefaultCardComparer.Instance, new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		/// <summary>
		/// Replaces the specified cards with the specified replacements using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ReplaceResult Replace(params ReplaceRequest[] requests) {
			return Replace(DefaultCardComparer.Instance, requests as IEnumerable<ReplaceRequest>);
		}

		/// <summary>
		/// Replaces the specified cards with the specified replacements using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ReplaceResult Replace(IEnumerable<ReplaceRequest> requests) {
			return Replace(DefaultCardComparer.Instance, requests);
		}

		/// <summary>
		/// Replaces the specified card with the specified replacement using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="card">The card to find.</param>
		/// <param name="replacement">The card to replace with.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replacement" /> is null.</exception>
		public ReplaceResult Replace(IEqualityComparer<Card> comparer, Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(comparer, new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		/// <summary>
		/// Replaces the specified cards with the specified replacements using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ReplaceResult Replace(IEqualityComparer<Card> comparer, params ReplaceRequest[] requests) {
			return Replace(comparer, requests as IEnumerable<ReplaceRequest>);
		}

		/// <summary>
		/// Replaces the specified cards with the specified replacements using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>A <see cref="ReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ReplaceResult Replace(IEqualityComparer<Card> comparer, IEnumerable<ReplaceRequest> requests) {
			comparer.ThrowIfNull(nameof(comparer));
			requests.ThrowIfNullOrEmptyOrContainsNulls(nameof(requests));

			var req = requests.
				Select((r, i) => new { Item = r, Index = i }).
				ToList();

			var replacements = new List<Card>();
			var found = new List<Card>();
			for (int i = 0; i < mCards.Count; i++) {
				var c = mCards[i];
				var replacement = req.FirstOrDefault(card => comparer.Equals(c, card.Item.Card));
				if (replacement == null) continue;

				mCards[i] = replacement.Item.Replacement;
				replacements.Add(replacement.Item.Card);
				req.RemoveAt(replacement.Index);
				found.Add(replacement.Item.Card);

				if (req.Count > 0) {
					for (int j = replacement.Index; j < req.Count; j++) {
						var value = req[j];
						req[j] = new { Item = value.Item, Index = value.Index - 1 };
					}
				}
				else {
					// No sense in looking for more, we've exhausted them
					break;
				}
			}

			return new ReplaceResult(
				replacements,
				req.Select(r => r.Item.Card));
		}

		/// <summary>
		/// Removes the first instance of the specified card from the set using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to remove.</param>
		/// <returns>A <see cref="DiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public DiscardResult Discard(Card card) {
			card.ThrowIfNull(nameof(card));
			return Discard(DefaultCardComparer.Instance, new DiscardRequest(quantity: 1, card: card));
		}

		/// <summary>
		/// Removes the specified quantity of cards from the set based on <paramref name="direction" />.
		/// </summary>
		/// <param name="card">The card to remove.</param>
		/// <param name="direction">The direction that the set should be iterated.</param>
		/// <returns>A <see cref="DiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="quantity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		public DiscardResult Discard(int quantity, Direction direction = Direction.FirstToLast) {
			quantity.ThrowIfZeroOrLess(nameof(quantity));
			direction.ThrowIfInvalid(nameof(direction));
			return Discard(DefaultCardComparer.Instance, new DiscardRequest(direction: direction, quantity: quantity));
		}

		/// <summary>
		/// Removes all cards that match the properties of the request from the set using a <see cref="DefaultCardComparer" /> if applicable.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>A <see cref="DiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public DiscardResult Discard(DiscardRequest request) {
			return Discard(DefaultCardComparer.Instance, request);
		}

		/// <summary>
		/// Removes the first instance of the specified card from the set using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="card">The card to remove.</param>
		/// <returns>A <see cref="DiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public DiscardResult Discard(IEqualityComparer<Card> comparer, Card card) {
			card.ThrowIfNull(nameof(card));
			return Discard(comparer, new DiscardRequest(quantity: 1, card: card));
		}

		/// <summary>
		/// Removes all cards that match the properties of the request from the set using the specified <see cref="IEqualityComparer{Card}" /> if applicable.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="request">The request.</param>
		/// <returns>A <see cref="DiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public DiscardResult Discard(IEqualityComparer<Card> comparer, DiscardRequest request) {
			comparer.ThrowIfNull(nameof(comparer));
			request.ThrowIfNull(nameof(request));
			bool hasQuantity = request.Quantity != null;
			bool hasSkip = request.Skip > 0;
			bool hasOffset = request.Offset > 0;
			if (hasOffset && request.Offset >= Count) throw new ArgumentException($"Must have an {nameof(DiscardRequest.Offset)} of less than the count", nameof(request));
			bool hasRank = request.Ranks.Any();
			bool hasSuit = request.Suits.Any();
			bool hasValues = request.Values.Any() || request.NullValues;
			bool hasSpecificCards = request.Cards.Any();
			bool hasSpecificCardsOnly = hasSpecificCards && !hasValues && !hasSuit && !hasRank;
			int quantity = request.Quantity.GetValueOrDefault(-1);
			var suits = request.Suits;
			var ranks = request.Ranks;
			var cards = request.Cards.ToList();
			var values = request.Values;
			var result = new List<Card>();

			if (hasSkip) {
				if (hasQuantity && quantity >= Count) {
					int i = request.Offset;
					do {
						result.Add(mCards[i]);
						mCards.RemoveAt(i);
						i += request.Skip;
					} while (i < mCards.Count);
				}
				else {
					int discarded = 0;

					// This will ensure that we discard everything possible
					quantity = hasQuantity ?
						quantity :
						Count;

					switch (request.Direction) {
						case Direction.FirstToLast: {
							int i = request.Offset;
							do {
								result.Add(mCards[i]);
								mCards.RemoveAt(i);
								i += request.Skip;
								discarded++;
							} while (i < mCards.Count && discarded < quantity);
							break;
						}
						case Direction.LastToFirst: {
							int i = mCards.Count - 1 - request.Offset;
							do {
								result.Add(mCards[i]);
								mCards.RemoveAt(i);
								i -= (request.Skip + 1);
								discarded++;
							} while (i >= 0 && discarded < quantity);
							break;
						}
						default: throw new NotImplementedException();
					}
				}
			}
			else if (!(hasRank || hasSuit || hasSpecificCards || hasValues)) {
				if (!hasQuantity || (quantity >= Count && hasOffset)) {
					switch (request.Direction) {
						case Direction.FirstToLast:
							result.AddRange(mCards.Skip(request.Offset));
							mCards.RemoveRange(request.Offset, mCards.Count - request.Offset);
							break;
						case Direction.LastToFirst:
							result.AddRange(mCards.Take(mCards.Count - request.Offset));
							mCards.RemoveRange(0, mCards.Count - request.Offset);
							break;
						default: throw new NotImplementedException();
					}
				}
				else if (quantity >= Count) {
					result.AddRange(mCards);
					mCards.Clear();
				}
				else {
					int startPoint = -1;
					switch (request.Direction) {
						case Direction.FirstToLast: startPoint = request.Offset; break;
						case Direction.LastToFirst: startPoint = mCards.Count - request.Offset - quantity; break;
						default: throw new NotImplementedException();
					}
					result.AddRange(mCards.Skip(startPoint).Take(quantity));
					mCards.RemoveRange(startPoint, quantity);
				}
			}
			else {
				Func<Card, int> getDiscardIndex = card => {
					for (int i = 0; i < cards.Count; i++) {
						if (comparer.Equals(cards[i], card)) {
							return i;
						}
					}
					return -1;
				};

				Func<Card, bool> isDiscard = card => {
					if (hasSpecificCardsOnly) return false;

					bool rankValid = !hasRank || ranks.Contains(card.Rank);
					bool suitValid = !hasSuit || suits.Contains(card.Suit);
					bool valueValid =
						(!hasValues || (card.Value != null && values.Contains(card.Value.GetValueOrDefault()))) ||
						(request.NullValues && card.Value == null);
					return rankValid && suitValid && valueValid;
				};

				switch (request.Direction) {
					case Direction.FirstToLast:
						for (int i = 0; i < mCards.Count; i++) {
							var card = mCards[i];
							int discardIndex = getDiscardIndex(card);
							if (discardIndex >= 0 || isDiscard(card)) {
								result.Add(card);
								mCards.RemoveAt(i);
								if (discardIndex >= 0) cards.RemoveAt(discardIndex);
								i--;
								if (hasQuantity && result.Count == quantity) break;
							}
						}
						break;
					case Direction.LastToFirst:
						for (int i = mCards.Count - 1; i >= 0; i--) {
							var card = mCards[i];
							int discardIndex = getDiscardIndex(card);
							if (discardIndex >= 0 || isDiscard(card)) {
								result.Add(card);
								mCards.RemoveAt(i);
								if (discardIndex >= 0) cards.RemoveAt(discardIndex);
								if (hasQuantity && result.Count == quantity) break;
							}
						}
						break;
					default: throw new NotImplementedException();
				}
			}

			return new DiscardResult(
				result,
				cards);
		}

		/// <summary>
		/// Distributes the entire set into the specified quantity of piles.
		/// If the quantity of cards is not evenly divisible, the remainder will be distributed to each pile until exhausted.
		/// </summary>
		/// <param name="numberOfPiles">The desired quantity of piles.</param>
		/// <returns>A <see cref="DistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentException"><paramref name="numberOfPiles" /> is 1 or less.</exception>
		public DistributeResult DistributeByPiles(int numberOfPiles) {
			return Distribute(new DistributeRequest(numberOfPiles: numberOfPiles));
		}

		/// <summary>
		/// Distributes the entire set into piles composed of the specified quantity of cards.
		/// If the quantity of cards is not evenly divisible, the remainder will be distributed to each pile until exhausted.
		/// </summary>
		/// <param name="numberOfCards">The desired quantity of cards per pile.</param>
		/// <returns>A <see cref="DistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentException"><paramref name="numberOfCards" /> is 0 or less.</exception>
		public DistributeResult DistributeByCards(int numberOfCards) {
			return Distribute(new DistributeRequest(numberOfCards: numberOfCards));
		}

		/// <summary>
		/// Distributes the set based on the properties of the request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>A <see cref="DistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public DistributeResult Distribute(DistributeRequest request) {
			request.ThrowIfNull(nameof(request));

			int numberOfPiles = request.Piles.HasValue ?
				request.Piles.Value :
				(Count / request.Cards.Value);

			var groups = new List<CardSet>(numberOfPiles + 1);
			int remainder = Count % numberOfPiles;
			switch (request.Distribution) {
				case DistributionPolicy.Alternating:
					for (int i = 0; i < numberOfPiles; i++) {
						groups.Add(new CardSet());
					}
					if (remainder != 0 && request.Remainder == RemainderPolicy.SeparatePile) {
						groups.Add(new CardSet());
					}

					int maxCards = Count - remainder;
					for (int i = 0; i < Count; i++) {
						int group = i % numberOfPiles;
						if (i >= maxCards) {
							bool done = false;
							switch (request.Remainder) {
								case RemainderPolicy.Distribute: groups[group].Append(mCards[i]); break;
								case RemainderPolicy.NoRemainder: done = true; break;
								case RemainderPolicy.SeparatePile: groups[numberOfPiles].Append(mCards[i]); break;
								default: throw new NotImplementedException();
							}
							if (done) break;
						}
						else {
							groups[group].Append(mCards[i]);
						}
					}
					break;
				case DistributionPolicy.Heap:
					int chunkPoint = Count / numberOfPiles;
					int basePoint = 0;
					switch (request.Remainder) {
						case RemainderPolicy.Distribute:
							int lastSequence = numberOfPiles - 1;
							for (int i = 0; i < numberOfPiles; i++) {
								if (i == lastSequence) {
									groups.Add(new CardSet(mCards.Skip(basePoint)));
								}
								else {
									int add = i < remainder ? 1 : 0;
									groups.Add(new CardSet(mCards.Skip(basePoint).Take(chunkPoint + add)));
									basePoint += chunkPoint + add;
								}
							}
							break;
						case RemainderPolicy.SeparatePile:
						case RemainderPolicy.NoRemainder:
							for (int i = 0; i < numberOfPiles; i++) {
								groups.Add(new CardSet(mCards.Skip(basePoint).Take(chunkPoint)));
								basePoint += chunkPoint;
							}
							if (remainder != 0 && request.Remainder != RemainderPolicy.NoRemainder) {
								groups.Add(new CardSet(mCards.Skip(basePoint)));
							}
							break;
						default: throw new NotImplementedException();
					}
					break;
				default: throw new NotImplementedException();
			}

			if (request.Remainder != RemainderPolicy.NoRemainder || remainder == 0) {
				mCards.Clear();
			}
			else {
				mCards = mCards.Skip(Count - remainder).ToList(); 
			}

			return new DistributeResult(groups.AsReadOnly());
		}

		/// <summary>
		/// Splits the set in two from the middle.
		/// No cards remain in this instance after this operation is complete.
		/// If there is an odd number of cards, the extra card will be a part of <see cref="SplitResult.Bottom" />.
		/// </summary>
		/// <returns>A <see cref="SplitResult" /> instance.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is less than 2.</exception>
		public SplitResult Split() {
			return Split(Count / 2);
		}

		/// <summary>
		/// Splits the set in two at the specified zero-based index.
		/// No cards remain in this instance after this operation is complete.
		/// </summary>
		/// <param name="splitPoint">The zero-based index where the set should be split.</param>
		/// <returns>A <see cref="SplitResult" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="splitPoint" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is less than 2.</exception>
		public SplitResult Split(int splitPoint) {
			splitPoint.ThrowIfOutsideRange(0, Count, nameof(splitPoint));
			if (Count < 2) throw new InvalidOperationException("Must have at least 2 items to perform a split");

			var result = new SplitResult(
				new CardSet(mCards.Take(splitPoint)),
				new CardSet(mCards.Skip(splitPoint)));
			mCards.Clear();
			return result;
		}

		/// <summary>
		/// Reverses the ordering of the cards in the set.
		/// </summary>
		public void Reverse() {
			mCards = mCards.Reverse<Card>().ToList();
		}

		/// <summary>
		/// Creates a copy of the current <see cref="CardSet" />.
		/// </summary>
		/// <returns>A new <see cref="CardSet" /> instance with the same cards as the current instance.</returns>
		public CardSet Duplicate() {
			return new CardSet(mCards);
		}

		/// <summary>
		/// Sorts the cards in the set with the specified <see cref="IComparer{Card}"/>.
		/// </summary>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public void Sort(IComparer<Card> comparer, SortDirection direction = SortDirection.Ascending) {
			comparer.ThrowIfNull(nameof(comparer));
			direction.ThrowIfInvalid(nameof(direction));
			IEnumerable<Card> intermediate = mCards;

			switch (direction) {
				case SortDirection.Ascending:
					intermediate = intermediate.OrderBy(c => c, comparer);
					break;
				case SortDirection.Descending:
					intermediate = intermediate.OrderByDescending(c => c, comparer);
					break;
				default: throw new NotImplementedException();
			}

			mCards = intermediate.ToList();
		}

		/// <summary>
		/// Sorts the cards in the set with the specified ordering function.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		public void Sort<TResult>(Func<Card, TResult> sort, SortDirection direction = SortDirection.Ascending) {
			sort.ThrowIfNull(nameof(sort));
			direction.ThrowIfInvalid(nameof(direction));
			IEnumerable<Card> intermediate = mCards;

			switch (direction) {
				case SortDirection.Ascending:
					intermediate = intermediate.OrderBy(sort);
					break;
				case SortDirection.Descending:
					intermediate = intermediate.OrderByDescending(sort);
					break;
				default: throw new NotImplementedException();
			}

			mCards = intermediate.ToList();
		}

		/// <summary>
		/// Sorts the cards in the set with the specified ordering function.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		public void Sort<TResult>(Func<Card, int, TResult> sort, SortDirection direction = SortDirection.Ascending) {
			sort.ThrowIfNull(nameof(sort));
			direction.ThrowIfInvalid(nameof(direction));
			var intermediate = mCards.Select((c, i) => new { Index = i, Card = c });

			switch (direction) {
				case SortDirection.Ascending:
					intermediate = intermediate.OrderBy(c => sort(c.Card, c.Index));
					break;
				case SortDirection.Descending:
					intermediate = intermediate.OrderByDescending(c => sort(c.Card, c.Index));
					break;
				default: throw new NotImplementedException();
			}

			mCards = intermediate.
				Select(c => c.Card).
				ToList();
		}

		/// <summary>
		/// Sorts the cards in the set with the specified ordering function and specified <see cref="IComparer{TResult}"/>.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public void Sort<TResult>(Func<Card, TResult> sort, IComparer<TResult> comparer, SortDirection direction = SortDirection.Ascending) {
			sort.ThrowIfNull(nameof(sort));
			comparer.ThrowIfNull(nameof(comparer));
			direction.ThrowIfInvalid(nameof(direction));
			IEnumerable<Card> intermediate = mCards;

			switch (direction) {
				case SortDirection.Ascending:
					intermediate = intermediate.OrderBy(sort, comparer);
					break;
				case SortDirection.Descending:
					intermediate = intermediate.OrderByDescending(sort, comparer);
					break;
				default: throw new NotImplementedException();
			}

			mCards = intermediate.ToList();
		}

		/// <summary>
		/// Sorts the cards in the set with the specified ordering function and specified <see cref="IComparer{TResult}"/>.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public void Sort<TResult>(Func<Card, int, TResult> sort, IComparer<TResult> comparer, SortDirection direction = SortDirection.Ascending) {
			sort.ThrowIfNull(nameof(sort));
			comparer.ThrowIfNull(nameof(comparer));
			direction.ThrowIfInvalid(nameof(direction));
			var intermediate = mCards.Select((c, i) => new { Index = i, Card = c });

			switch (direction) {
				case SortDirection.Ascending:
					intermediate = intermediate.OrderBy(c => sort(c.Card, c.Index), comparer);
					break;
				case SortDirection.Descending:
					intermediate = intermediate.OrderByDescending(c => sort(c.Card, c.Index), comparer);
					break;
				default: throw new NotImplementedException();
			}

			mCards = intermediate.
				Select(c => c.Card).
				ToList();
		}

		/// <summary>
		/// Returns a new immutable copy of the cards in the current <see cref="CardSet" />.
		/// </summary>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		public ImmutableCardSet AsImmutable() {
			return new ImmutableCardSet(mCards);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="CardSet" />.
		/// </summary>
		/// <returns>An enumerator for the <see cref="CardSet" />.</returns>
		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="CardSet" />.
		/// </summary>
		/// <returns>An enumerator for the <see cref="CardSet" />.</returns>
		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}