using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public sealed class ReplaceRequest {
		public Card Card { get; private set; }
		public Card Replacement { get; private set; }

		public ReplaceRequest(Card card, Card replacement) {
			card.ThrowIfNull(nameof(card));
			replacement.ThrowIfNull(nameof(replacement));
			Card = card;
			Replacement = replacement;
		}
	}
}