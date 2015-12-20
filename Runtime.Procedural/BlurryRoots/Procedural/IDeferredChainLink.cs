namespace BlurryRoots.Procedural {

	/// <summary>
	/// Basis for any chain element meant to be linked together and has to be
	/// processed over more than one frame.
	/// </summary>
	/// <typeparam name="TValueType">Type of value to process.</typeparam>
	public interface IDeferredChainLink<TValueType> {

		void StartProcessing (TValueType input);

		void Update (float deltaTime);

		event FinishedProcessing<TValueType> OnFinishedProcessing;

	}

	public delegate void FinishedProcessing<TValueType> (TValueType output);

}