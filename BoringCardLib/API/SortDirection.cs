using System;

namespace BoringCardLib {
	/// <summary>
	/// Represents a sort direction.
	/// </summary>
	public enum SortDirection {
		/// <summary>
		/// Ascending sort (0 is before 9, A is before Z).
		/// </summary>
		Ascending,
		/// <summary>
		/// Descending sort (9 is before 0, Z is before A).
		/// </summary>
		Descending,
	}
}