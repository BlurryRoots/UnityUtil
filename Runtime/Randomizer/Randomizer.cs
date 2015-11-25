using UnityEngine;

namespace BlurryRoots {
	namespace Randomizer {

		/// <summary>
		/// Helper class for random number generation.
		/// </summary>
		public sealed class Randomizer : IRandomNumberGenerator {

			/// <summary>
			/// Gets a reference to the global rng.
			/// </summary>
			public static IRandomNumberGenerator Global {
				get {
					return Randomizer.globalRng;
				}
			}

			public static int GenerateSeed () {
				var v = Randomizer.Global.Float ();
				var max = Mathf.FloorToInt (int.MaxValue * v);
				var min = Mathf.FloorToInt (int.MinValue * v);

				return Randomizer.Global.Range (min, max);
			}

			/// <summary>
			/// Creates a new independend rng with a uniform generation pattern.
			/// Seed from global rng is used to initialize this rng.
			/// </summary>
			/// <returns>A new rng.</returns>
			public static IRandomNumberGenerator CreateUniform () {
				var seed = Randomizer.Global.Seed;

				return new UniformRandomNumberGenerator (seed);
			}

			/// <summary>
			/// Creates a new independend rng with a uniform generation pattern.
			/// </summary>
			/// <param name="seed">Seed to init this rng.</param>
			/// <returns>A new rng.</returns>
			public static IRandomNumberGenerator CreateUniform (int seed) {
				return new UniformRandomNumberGenerator (seed);
			}

			/// <summary>
			/// Creates a new independend rng with a non-uniform generation pattern.
			/// </summary>
			/// <returns>A new rng.</returns>
			public static IRandomNumberGenerator CreateNonUniform () {
				return null;
			}

			/// <summary>
			/// Gets or sets the current seed used to generate numbers.
			/// </summary>
			public int Seed {
				get {
					return Random.seed;
				}

				set {
					Random.seed = value;
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
				return Random.value;
			}

			/// <summary>
			/// Holds static reference to global rng wrapper.
			/// </summary>
			private static IRandomNumberGenerator globalRng = new Randomizer ();
			
		}

	}
}