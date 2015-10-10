using UnityEngine;

public class UniformRandomNumberGenerator : IRandomNumberGenerator {

	/// <summary>
	/// Gets or sets the current seed used to generate numbers.
	/// </summary>
	public int Seed {
		get {
			return this.seed;
		}
		set {
			this.seed = value;
			this.rng = new System.Random (this.seed);
		}
	}

	/// <summary>
	/// Range value [min, max].
	/// </summary>
	/// <param name="min">Lower bound.</param>
	/// <param name="max">Upper bound.</param>
	/// <returns>Ranged value.</returns>
	public int Range (int min, int max) {
		if (min > max) {
			throw new System.Exception ("Minimal value excees maxium value :/");
		}

		var interval = max - min;
		var value = Mathf.FloorToInt (this.Float () * interval);

		return value + min;
	}
	
	/// <summary>
	/// Range value [min, max].
	/// </summary>
	/// <param name="min">Lower bound.</param>
	/// <param name="max">Upper bound.</param>
	/// <returns>Ranged value.</returns>
	public float Range (float min, float max) {
		if (min > max) {
			throw new System.Exception ("Minimal value excees maxium value :/");
		}

		var interval = max - min;
		var value = this.Float () * interval;

		return value + min;
	}

	/// <summary>
	/// Generates a float in [0, 1].
	/// </summary>
	/// <returns>Float value.</returns>
	public float Float () {
		var v = this.rng.NextDouble ();
		return (float)v;
	}

	/// <summary>
	/// Creates a new rng with uniform generation patter. Seed is initalize from given number.
	/// </summary>
	/// <param name="seed">Seed to use for this rng.</param>
	public UniformRandomNumberGenerator (int seed) {
		this.seed = seed;
		this.rng = new System.Random (this.seed);
	}

	private int seed;
	private System.Random rng;

}