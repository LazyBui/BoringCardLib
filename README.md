BoringCardLib
=============

A .NET library designed to provide a basic framework for working with playing cards. There are no specific games provided with the library, it's only a set of primitives that will greatly improve the speed of implementing any particular game.

There is an emphasis on immutable design in the API and strong guarantees can be provided as such. The only mutable classes are `CardSet` and the `IRandomSource` providers, but you don't have to use them. You can use `ImmutableCardSet` if you would prefer. Additionally, many parts of the API allow you to specify things like your own randomization provider, which will give you much greater control over threading consistency and the use of local state.


Example Usage
=============

```
var deck = CardSet.MakeStandardDeck();
var players = deck.
	Shuffle().
	Draw(4 * 7).
	DistributeByPiles(4).Piles.
	ToArray();
// At this point, you have an array of 4 players worth of CardSet classes with 7 cards each.
// deck will contain all the leftover cards.
// You would then proceed to show their hands in the UI or begin to implement other portions of the game rules.

// For example, we'll implement some simple Go Fish turn logic.
// populate requestedRank
var matches = players[0].GetMatches(requestedRank);
bool gameEndingPossiblyEnding = false;

if (!matches.AnyCards) {
	if (deck.Count > 0) {
		players[2].Append(deck.Draw());
		if (players[2].Last.Rank == requestedRank) {
			var newMatch = players[2].GetMatches(requestedRank);
			playerMatches[2].Append(newMatch);
			players[2].Discard(newMatch);
			gameEndingPossiblyEnding = true;
			if (players[2].AnyCards) {
				// Another turn
			}
		}
	}
}
else {
	players[0].Discard(matches);
	players[2].Append(matches);
	// We can guarantee at this point that we will only have 2 cards, because if the player already had 2 cards, they would have had a match
	var completedMatch = players[2].GetMatches(requestedRank);
	playerMatches[2].Append(completedMatch);
	players[2].Discard(completedMatch);

	gamePossiblyEnding = true;
}

if (gamePossiblyEnding && !players.Any(p => p.Count > 0)) {
	// Game is over, tally up books
	var finalScores = playerMatches.
		Select((p, i) => new { Index = i, Matches = p.Count / 2 }).
		OrderByDescending(v => v.Matches);

	var winner = finalScores.First();
	// Congratulate
	return;
}

// Turn ending logic
```

This isn't a particularly compelling example in terms of utility, you could do most of this in about the same space with a `List<>` of some card class and a couple extension methods.

However, it's a reasonable example of the basics and how to use the library.