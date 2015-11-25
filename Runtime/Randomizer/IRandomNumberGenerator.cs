public interface IRandomNumberGenerator {

	/// <summary>
	/// Gets or sets the current seed used to generate numbers.
	/// </summary>
	int Seed {
		get;
		set;
	}

	/// <summary>
	/// Generates a float in [0, 1].
	/// </summary>
	/// <returns>Float value.</returns>
	float Float ();

	/// <summary>
	/// Range value [min, max].
	/// </summary>
	/// <param name="min">Lower bound.</param>
	/// <param name="max">Upper bound.</param>
	/// <returns>Ranged value.</returns>
	int Range (int min, int max);

	/// <summary>
	/// Range value [min, max].
	/// </summary>
	/// <param name="min">Lower bound.</param>
	/// <param name="max">Upper bound.</param>
	/// <returns>Ranged value.</returns>
	float Range (float min, float max);

}