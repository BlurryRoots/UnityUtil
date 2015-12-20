namespace BlurryRoots.Procedural {

	/// <summary>
	/// Basis for any chain element meant to be linked together and has to be
	/// processed over more than one frame, concurrent to the other links in
	/// a chain.
	/// </summary>
	/// <typeparam name="TValueType">Type of value to process.</typeparam>
	public interface IAsyncChainLink<TValueType> {

		/// <summary>
		/// Processes input data.
		/// </summary>
		/// <param name="deltaTime">Time fraction since last processing step.</param>
		/// <param name="input">Data to be processed.</param>
		/// <returns>Processed data.</returns>
		TValueType Process (float deltaTime, TValueType input);

	}

}