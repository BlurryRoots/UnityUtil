using System;
using System.Collections.Generic;
using System.Linq;

namespace BlurryRoots {
	namespace Common {

		/// <summary>
		/// Helper class used to iterate enum types.
		/// </summary>
		/// <typeparam name="TEnumType">Type of enum.</typeparam>
		public static class EnumTypeEnumerator<TEnumType>
		where TEnumType : struct, System.IConvertible, IComparable {

			/// <summary>
			/// Get all enum fields as enumerable of strings.
			/// </summary>
			/// <returns>Enumerable of all fields as strings.</returns>
			public static IEnumerable<TEnumType> GetValues () {
				return Enum.GetValues (typeof (TEnumType)).Cast<TEnumType> ();
			}

			/// <summary>
			/// Gets a random enum field.
			/// </summary>
			/// <param name="rng">Random number generator used to pick a field.</param>
			/// <returns>Randomly picked field.</returns>
			public static TEnumType GetRandomValue (Random rng) {
				return EnumTypeEnumerator<TEnumType>
					.GetValues ()
					.OrderBy<TEnumType, int> ((item) => rng.Next ())
					.First ()
					;
			}

			/// <summary>
			/// Gets a random enum field, but with certain constraints.
			/// </summary>
			/// <param name="rng">Random number generator used to pick a field.</param>
			/// <param name="filter">Filter determaining if a value is passable or not.</param>
			/// <returns>Randomly picked field.</returns>
			public static TEnumType GetRandomAndConstraintValue (Random rng, System.Func<TEnumType, bool> filter) {
				return EnumTypeEnumerator<TEnumType>
					.GetValues ()
					.OrderBy<TEnumType, int> ((item) => rng.Next ())
					.Where (filter)
					.First ()
					;
			}

		}

	}
}

