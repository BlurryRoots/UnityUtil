namespace BlurryRoots {
	namespace Procedural {

		/// <summary>
		/// Basis for any chain element meant to be linked together.
		/// </summary>
		/// <typeparam name="TValueType">Type of value to process.</typeparam>
		public interface IChainLink<TValueType> {

			/// <summary>
			/// Processes the given value and outputs the result.
			/// </summary>
			/// <param name="input">Value to process.</param>
			/// <returns>Processes value.</returns>
			TValueType Process (TValueType input);

		}

	}
}