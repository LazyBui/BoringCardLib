using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	/// <summary>
	/// Represents an immutable set of playing cards.
	/// </summary>
	public sealed class ImmutableCardSet : IEnumerable<Card>, IEnumerable {
		private CardSet mCards = null;

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
		public Card First { get { return mCards.First; } }
		/// <summary>
		/// Gets the last card of the set.
		/// </summary>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public Card Last { get { return mCards.Last; } }
		/// <summary>
		/// Gets the element at the specified zero-based index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified zero-based index.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not in the range [0, <see cref="Count" />).</exception>
		public Card this[int index] { get { return mCards[index]; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableCardSet" /> class.
		/// </summary>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards = new CardSet(cards);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableCardSet" /> class.
		/// </summary>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet(params Card[] cards) : this(cards as IEnumerable<Card>) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableCardSet" /> class.
		/// </summary>
		/// <param name="capacity">The expected capacity of the set to minimize reallocations.</param>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="capacity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet(int capacity, IEnumerable<Card> cards) {
			capacity.ThrowIfZeroOrLess(nameof(capacity));
			cards.ThrowIfNullOrContainsNulls(nameof(cards));
			mCards = new CardSet(capacity, cards);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableCardSet" /> class.
		/// </summary>
		/// <param name="capacity">The expected capacity of the set to minimize reallocations.</param>
		/// <param name="cards">The cards that should be in the set.</param>
		/// <exception cref="ArgumentException"><paramref name="capacity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet(int capacity, params Card[] cards) : this(capacity, cards as IEnumerable<Card>) { }

		/// <summary>
		/// Generates a standard deck with the specified adjustments.
		/// </summary>
		/// <param name="jokerCount">Indicates how many jokers the deck should contain.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="ImmutableCardSet" /> instance representing a standard deck.</returns>
		/// <exception cref="ArgumentException"><paramref name="jokerCount" /> is negative.</exception>
		public static ImmutableCardSet MakeStandardDeck(int jokerCount = 0, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeStandardDeck(
				jokerCount: jokerCount,
				initializeValues: initializeValues).
				AsImmutable();
		}

		/// <summary>
		/// Generates a standard full suit with the specified adjustments.
		/// </summary>
		/// <param name="suit">The suit to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="ImmutableCardSet" /> instance representing a standard suit.</returns>
		/// <exception cref="ArgumentException"><paramref name="suit" /> is invalid or <see cref="Suit.Joker" />.</exception>
		public static ImmutableCardSet MakeFullSuit(Suit suit, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeFullSuit(
				suit,
				initializeValues: initializeValues).
				AsImmutable();
		}

		/// <summary>
		/// Generates a standard full rank with the specified adjustments.
		/// </summary>
		/// <param name="rank">The rank to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>A <see cref="ImmutableCardSet" /> instance representing a standard rank.</returns>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid or <see cref="Rank.Joker" />.</exception>
		public static ImmutableCardSet MakeFullRank(Rank rank, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeFullRank(
				rank,
				initializeValues: initializeValues).
				AsImmutable();
		}

		/// <summary>
		/// Generates a set of jokers.
		/// </summary>
		/// <param name="jokerCount">The quantity of jokers to generate.</param>
		/// <param name="initializeValues">A transformation function that accepts a <see cref="Card" /> and returns a <see cref="Card" />. Can be used to assign point values.</param>
		/// <returns>An <see cref="ImmutableCardSet" /> instance representing the jokers.</returns>
		/// <exception cref="ArgumentException"><paramref name="jokerCount" /> is 0 or less.</exception>
		public static ImmutableCardSet MakeJokers(int jokerCount, Func<Card, Card> initializeValues = null) {
			return CardSet.MakeJokers(
				jokerCount,
				initializeValues: initializeValues).
				AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set before <see cref="First" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Prepend(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			return new ImmutableCardSet(cards.Concat(mCards));
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set before <see cref="First" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Prepend(params Card[] cards) {
			return Prepend(cards as IEnumerable<Card>);
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set after <see cref="Last" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Append(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			return new ImmutableCardSet(mCards.Concat(cards));
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set after <see cref="Last" />.
		/// </summary>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Append(params Card[] cards) {
			return Append(cards as IEnumerable<Card>);
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set at the specified index.
		/// </summary>
		/// <param name="index">The index to insert cards at.</param>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="index" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Insert(int index, IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmpty(nameof(cards));
			index.ThrowIfOutsideRange(0, Count, nameof(index));
			return new ImmutableCardSet(mCards.Take(index).
				Concat(cards).
				Concat(mCards.Skip(index)));
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards added to the set at the specified index.
		/// </summary>
		/// <param name="index">The index to insert cards at.</param>
		/// <param name="cards">The cards to add to the set.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="index" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableCardSet Insert(int index, params Card[] cards) {
			return Insert(index, cards as IEnumerable<Card>);
		}

		/// <summary>
		/// /// Returns a new <see cref="ImmutableCardSet" /> instance with a ordering randomized of the cards in the set using a <see cref="DefaultRandomSource" /> instance.
		/// </summary>
		/// <param name="times">The quantity of times to shuffle.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="times" /> is 0 or less.</exception>
		public ImmutableCardSet Shuffle(int times = 1) {
			return Shuffle(new DefaultRandomSource(), times: times);
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with a ordering randomized of the cards in the set using the specified <see cref="IRandomSource" />.
		/// </summary>
		/// <param name="sampler">The source of randomness.</param>
		/// <param name="times">The quantity of times to shuffle.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="times" /> is 0 or less.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sampler" /> is null.</exception>
		public ImmutableCardSet Shuffle(IRandomSource sampler, int times = 1) {
			sampler.ThrowIfNull(nameof(sampler));
			times.ThrowIfZeroOrLess(nameof(times));
			var newGroup = mCards.Duplicate();
			newGroup.Shuffle(sampler, times: times);
			return newGroup.AsImmutable();
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
			return mCards.GetRandom(sampler);
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
		/// Counts how many instances of the specified <see cref="Card" /> are in the current set using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <returns>The count of instances that match <paramref name="card" />.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public int CountOf(Card card) {
			return mCards.CountOf(card, DefaultCardComparer.Instance);
		}

		/// <summary>
		/// Counts how many instances of the specified <see cref="Card" /> are in the current set using the specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <param name="comparer">The comparer to check with.</param>
		/// <returns>The count of instances that match <paramref name="card" />.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public int CountOf(Card card, IEqualityComparer<Card> comparer) {
			return mCards.CountOf(card, comparer);
		}

		/// <summary>
		/// Counts how many instances of the specified <see cref="Suit" /> are in the current set.
		/// </summary>
		/// <param name="rank">The suit to check for.</param>
		/// <returns>The count of instances that match <paramref name="rank" />.</returns>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid.</exception>
		public int CountOf(Suit suit) {
			return mCards.CountOf(suit);
		}

		/// <summary>
		/// Counts how many instances of the specified <see cref="Rank" /> are in the current set.
		/// </summary>
		/// <param name="rank">The rank to check for.</param>
		/// <returns>The count of instances that match <paramref name="rank" />.</returns>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid.</exception>
		public int CountOf(Rank rank) {
			return mCards.CountOf(rank);
		}

		/// <summary>
		/// Retrieves all cards in the current set of cards that match the specified <see cref="Card" /> using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <returns>true if the current set of cards contains <paramref name="card" />, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		public ImmutableCardSet GetMatches(Card card) {
			return mCards.GetMatches(card).AsImmutable();
		}

		/// <summary>
		/// Retrieves all cards in the current set of cards that match the specified <see cref="Card" /> using a specified <see cref="IEqualityComparer{T}" />.
		/// </summary>
		/// <param name="card">The card to check for.</param>
		/// <param name="comparer">The comparer to check with.</param>
		/// <returns>All cards that match <paramref name="card" /> in the current set.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public ImmutableCardSet GetMatches(Card card, IEqualityComparer<Card> comparer) {
			return mCards.GetMatches(card, comparer).AsImmutable();
		}

		/// <summary>
		/// Retrieves all cards in the current set of cards that match the specified <see cref="Suit" />.
		/// </summary>
		/// <param name="suit">The suit to check for.</param>
		/// <returns>All cards that match the specified suit.</returns>
		/// <exception cref="ArgumentException"><paramref name="suit" /> is invalid.</exception>
		public ImmutableCardSet GetMatches(Suit suit) {
			return mCards.GetMatches(suit).AsImmutable();
		}

		/// <summary>
		/// Retrieves all cards in the current set of cards that match the specified <see cref="Rank" />.
		/// </summary>
		/// <param name="rank">The rank to check for.</param>
		/// <returns>All cards that match the specified rank.</returns>
		/// <exception cref="ArgumentException"><paramref name="rank" /> is invalid.</exception>
		public ImmutableCardSet GetMatches(Rank rank) {
			return mCards.GetMatches(rank).AsImmutable();
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
			return mCards.IndexOf(card, comparer);
		}

		/// <summary>
		/// Draws one card from the top of the set.
		/// </summary>
		/// <returns>An <see cref="ImmutableDrawSingleResult" /> instance representing the result of the draw operation.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is 0.</exception>
		public ImmutableDrawSingleResult Draw() {
			if (Count == 0) throw new InvalidOperationException("Must have cards to draw");
			return new ImmutableDrawSingleResult(
				new ImmutableCardSet(mCards.Skip(1)),
				mCards[0]);
		}

		/// <summary>
		/// Draws the specified number of cards from the top of the set.
		/// If <paramref name="cards" /> is greater than <see cref="Count" />, it returns all cards remaining.
		/// </summary>
		/// <param name="cards">The quantity of cards to draw.</param>
		/// <returns>An <see cref="ImmutableDrawResult" /> instance representing the result of the draw operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is 0 or less.</exception>
		public ImmutableDrawResult Draw(int cards) {
			cards.ThrowIfZeroOrLess(nameof(cards));
			if (cards >= Count) {
				return new ImmutableDrawResult(
					new ImmutableCardSet(),
					Duplicate());
			}

			return new ImmutableDrawResult(
				new ImmutableCardSet(mCards.Skip(cards)),
				new ImmutableCardSet(mCards.Take(cards)));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="card">The card to find.</param>
		/// <param name="replacement">The card to replace with.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replacement" /> is null.</exception>
		public ImmutableReplaceResult Replace(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(DefaultCardComparer.Instance, new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ImmutableReplaceResult Replace(params ReplaceRequest[] requests) {
			return Replace(DefaultCardComparer.Instance, requests as IEnumerable<ReplaceRequest>);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ImmutableReplaceResult Replace(IEnumerable<ReplaceRequest> requests) {
			return Replace(DefaultCardComparer.Instance, requests);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="card">The card to find.</param>
		/// <param name="replacement">The card to replace with.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="card" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="replacement" /> is null.</exception>
		public ImmutableReplaceResult Replace(IEqualityComparer<Card> comparer, Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			return Replace(comparer, new List<ReplaceRequest>() {
				new ReplaceRequest(card, replacement),
			});
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ImmutableReplaceResult Replace(IEqualityComparer<Card> comparer, params ReplaceRequest[] requests) {
			return Replace(comparer, requests as IEnumerable<ReplaceRequest>);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableReplaceResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="requests">A list of requests for replacement.</param>
		/// <returns>a new <see cref="ImmutableReplaceResult" /> instance representing the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="requests" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="requests" /> is empty or contains nulls.</exception>
		public ImmutableReplaceResult Replace(IEqualityComparer<Card> comparer, IEnumerable<ReplaceRequest> requests) {
			comparer.ThrowIfNull(nameof(comparer));
			requests.ThrowIfNullOrEmptyOrContainsNulls(nameof(requests));

			var deck = mCards.Duplicate();
			var result = deck.Replace(comparer, requests);
			return new ImmutableReplaceResult(
				deck,
				result.Replaced,
				result.NotFound);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="cards">The cards to remove.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableDiscardResult Discard(params Card[] cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			return Discard(DefaultCardComparer.Instance, new DiscardRequest(cards: cards));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="cards">The cards to remove.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableDiscardResult Discard(IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			return Discard(DefaultCardComparer.Instance, new DiscardRequest(cards: cards));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation.
		/// </summary>
		/// <param name="card">The card to remove.</param>
		/// <param name="direction">The direction that the set should be iterated.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="quantity" /> is 0 or less.</exception>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		public ImmutableDiscardResult Discard(int quantity, Direction direction = Direction.FirstToLast) {
			quantity.ThrowIfZeroOrLess(nameof(quantity));
			direction.ThrowIfInvalid(nameof(direction));
			return Discard(DefaultCardComparer.Instance, new DiscardRequest(direction: direction, quantity: quantity));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using a <see cref="DefaultCardComparer" />.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public ImmutableDiscardResult Discard(DiscardRequest request) {
			return Discard(DefaultCardComparer.Instance, request);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="cards">The cards to remove.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableDiscardResult Discard(IEqualityComparer<Card> comparer, params Card[] cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			return Discard(comparer, new DiscardRequest(cards: cards));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="cards">The cards to remove.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentException"><paramref name="cards" /> is empty or contains nulls.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cards" /> is null.</exception>
		public ImmutableDiscardResult Discard(IEqualityComparer<Card> comparer, IEnumerable<Card> cards) {
			cards.ThrowIfNullOrEmptyOrContainsNulls(nameof(cards));
			return Discard(comparer, new DiscardRequest(cards: cards));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDiscardResult" /> with the result of the operation using the specified <see cref="IEqualityComparer{Card}" />.
		/// </summary>
		/// <param name="comparer">The comparer to check with.</param>
		/// <param name="request">The request.</param>
		/// <returns>a new <see cref="ImmutableDiscardResult" /> instance with the result of the operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public ImmutableDiscardResult Discard(IEqualityComparer<Card> comparer, DiscardRequest request) {
			comparer.ThrowIfNull(nameof(comparer));
			request.ThrowIfNull(nameof(request));
			var deck = mCards.Duplicate();
			var result = deck.Discard(comparer, request);
			return new ImmutableDiscardResult(deck, result.Discarded, result.NotFound);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDistributeResult" /> with the entire set distributed into the specified quantity of piles.
		/// If the quantity of cards is not evenly divisible, the remainder will be distributed to each pile until exhausted.
		/// </summary>
		/// <param name="numberOfPiles">The desired quantity of piles.</param>
		/// <returns>A <see cref="ImmutableDistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentException"><paramref name="numberOfPiles" /> is 1 or less.</exception>
		public ImmutableDistributeResult DistributeByPiles(int numberOfPiles) {
			return Distribute(new DistributeRequest(numberOfPiles: numberOfPiles));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDistributeResult" /> with the entire set distributed into piles of the specified quantity of cards.
		/// If the quantity of cards is not evenly divisible, the remainder will be distributed to each pile until exhausted.
		/// </summary>
		/// <param name="numberOfCards">The desired quantity of cards per pile.</param>
		/// <returns>A <see cref="ImmutableDistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentException"><paramref name="numberOfCards" /> is 0 or less.</exception>
		public ImmutableDistributeResult DistributeByCards(int numberOfCards) {
			return Distribute(new DistributeRequest(numberOfCards: numberOfCards));
		}

		/// <summary>
		/// Returns an <see cref="ImmutableDistributeResult" /> with the set distributed based on the properties of the request.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>A <see cref="ImmutableDistributeResult" /> instance with the appropriate piles.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="request" /> is null.</exception>
		public ImmutableDistributeResult Distribute(DistributeRequest request) {
			return new ImmutableDistributeResult(mCards.
				Duplicate().
				Distribute(request).Piles);
		}

		/// <summary>
		/// Returns an <see cref="ImmutableSplitResult" /> instance with the result of a split of the set in two from the middle.
		/// If there is an odd number of cards, the extra card will be a part of <see cref="SplitResult.Bottom" />.
		/// </summary>
		/// <returns>A <see cref="SplitResult" /> instance.</returns>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is less than 2.</exception>
		public ImmutableSplitResult Split() {
			return new ImmutableSplitResult(mCards.Duplicate().Split());
		}

		/// <summary>
		/// Returns an <see cref="ImmutableSplitResult" /> instance with the result of a split of the set in two at the specified zero-based index.
		/// </summary>
		/// <param name="splitPoint">The zero-based index where the set should be split.</param>
		/// <returns>An <see cref="ImmutableSplitResult" /> instance.</returns>
		/// <exception cref="ArgumentException"><paramref name="splitPoint" /> is not in the range [0, <see cref="Count" />].</exception>
		/// <exception cref="InvalidOperationException"><see cref="Count" /> is less than 2.</exception>
		public ImmutableSplitResult Split(int splitPoint) {
			return new ImmutableSplitResult(mCards.Duplicate().Split(splitPoint));
		}

		/// <summary>
		/// Reverses the ordering of the cards in the set.
		/// </summary>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance with inverted ordering.</returns>
		public ImmutableCardSet Reverse() {
			var newGroup = mCards.Duplicate();
			newGroup.Reverse();
			return new ImmutableCardSet(newGroup);
		}

		/// <summary>
		/// Creates a copy of the current <see cref="ImmutableCardSet" />.
		/// </summary>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance with the same cards as the current instance.</returns>
		public ImmutableCardSet Duplicate() {
			return mCards.AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards sorted by the specified <see cref="IComparer{Card}"/>.
		/// </summary>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance that is sorted.</returns>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public ImmutableCardSet Sort(IComparer<Card> comparer, SortDirection direction = SortDirection.Ascending) {
			var @new = mCards.Duplicate();
			@new.Sort(comparer, direction: direction);
			return @new.AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards sorted by the specified ordering function.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance that is sorted.</returns>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		public ImmutableCardSet Sort<TResult>(Func<Card, TResult> sort, SortDirection direction = SortDirection.Ascending) {
			var @new = mCards.Duplicate();
			@new.Sort(sort, direction: direction);
			return @new.AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards sorted by the specified ordering function.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance that is sorted.</returns>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		public ImmutableCardSet Sort<TResult>(Func<Card, int, TResult> sort, SortDirection direction = SortDirection.Ascending) {
			var @new = mCards.Duplicate();
			@new.Sort(sort, direction: direction);
			return @new.AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards sorted by the specified ordering function and specified <see cref="IComparer{TResult}"/>.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance that is sorted.</returns>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public ImmutableCardSet Sort<TResult>(Func<Card, TResult> sort, IComparer<TResult> comparer, SortDirection direction = SortDirection.Ascending) {
			var @new = mCards.Duplicate();
			@new.Sort(sort, comparer, direction: direction);
			return @new.AsImmutable();
		}

		/// <summary>
		/// Returns a new <see cref="ImmutableCardSet" /> instance with cards sorted by the specified ordering function and specified <see cref="IComparer{TResult}"/>.
		/// </summary>
		/// <param name="sort">The criteria to use for ordering.</param>
		/// <param name="comparer">The comparer to use for ordering.</param>
		/// <param name="direction">The direction of the sort.</param>
		/// <returns>A new <see cref="ImmutableCardSet" /> instance that is sorted.</returns>
		/// <exception cref="ArgumentException"><paramref name="direction" /> is invalid.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="sort" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="comparer" /> is null.</exception>
		public ImmutableCardSet Sort<TResult>(Func<Card, int, TResult> sort, IComparer<TResult> comparer, SortDirection direction = SortDirection.Ascending) {
			var @new = mCards.Duplicate();
			@new.Sort(sort, comparer, direction: direction);
			return @new.AsImmutable();
		}

		/// <summary>
		/// Returns a new mutable copy of the cards in the current <see cref="ImmutableCardSet" />.
		/// </summary>
		/// <returns>A new <see cref="CardSet" /> instance.</returns>
		public CardSet AsMutable() {
			return mCards.Duplicate();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="ImmutableCardSet" />.
		/// </summary>
		/// <returns>An enumerator for the <see cref="ImmutableCardSet" />.</returns>
		public IEnumerator<Card> GetEnumerator() {
			return mCards.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="ImmutableCardSet" />.
		/// </summary>
		/// <returns>An enumerator for the <see cref="ImmutableCardSet" />.</returns>
		IEnumerator IEnumerable.GetEnumerator() {
			return mCards.GetEnumerator();
		}
	}
}