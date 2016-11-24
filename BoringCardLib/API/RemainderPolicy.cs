using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringCardLib {
	public enum RemainderPolicy {
		/// <summary>
		/// Distributes the remainder over piles until there are none left.
		/// This means that some piles will have different quantities.
		/// </summary>
		Distribute,
		/// <summary>
		/// Creates a separate pile from the remainder.
		/// All specified piles will have the same quantity and there will be an extra pile.
		/// </summary>
		SeparatePile,
		/// <summary>
		/// Leaves the remainder in the original pile.
		/// All specified piles will have the same quantity and there will only be the number of piles specified.
		/// </summary>
		NoRemainder,
	}
}