using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: REWRITE THIS MESS!!

namespace BlurryRoots {
	namespace Procedural {

		#region stinking lame generators
		public class ConstantNoiseGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// combine white noise with given value
				for (var i = 0; i < input.Count; ++i) {
					input[i] = input[i] * baseNumbers[i % baseNumbers.Length];
				}

				// how many elements must be added?
				var missingElements = this.count - input.Count;
				for (var i = 0u; i < missingElements; ++i) {
					input.Add (baseNumbers[i % baseNumbers.Length]);
				}

				return input;
			}

			public ConstantNoiseGenerator (uint count) {
				this.count = count;
			}

			private uint count;
			private static float[] baseNumbers = {
				0.618f,
				0.31415f,
				0.9f,
				1f,
				0.4f,
				0.1f,
			};

		}

		public class WhiteNoiseGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// combine white noise with given value
				for (var i = 0; i < input.Count; ++i) {
					input[i] = input[i] * this.rng.Float ();
				}

				// how many elements must be added?
				var missingElements = this.count - input.Count;

				// append missing values
				for (var i = 0; i < missingElements; ++i) {
					input.Add (this.rng.Float ());
				}

				return input;
			}

			public WhiteNoiseGenerator (int seed, uint count) {
				this.rng = new UniformRandomNumberGenerator (seed);
				this.count = count;
			}

			private IRandomNumberGenerator rng;
			private uint count;

		}

		public class NeighbourMutator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				for (var index = 0; index < input.Count; ++index) {
					var neighbourIndex = index + 1;
					if (neighbourIndex < input.Count) {
						input[index] = this.smoothFunction (input[index], input[neighbourIndex]);
					}
				}

				return input;
			}

			public NeighbourMutator (System.Func<float, float, float> smoothFunction) {
				this.smoothFunction = smoothFunction;
			}

			System.Func<float, float, float> smoothFunction;

		}

		public class RedNoiseGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// create a smoothed white noise
				return this.noise.Process (input);
			}

			public RedNoiseGenerator (int seed, uint count, float smoothFactor) {
				smoothFactor = smoothFactor > 1f
					? 1f
					: (smoothFactor < -1f)
						? -1f
						: smoothFactor
						;

				this.noise = new Chain<IList<float>> ();
				this.noise.Add (new WhiteNoiseGenerator (seed, count));
				this.noise.Add (new NeighbourMutator ((float a, float b) => {
					return smoothFactor * (a + b);
				}));
			}

			private Chain<IList<float>> noise;

		}

		public class VioletNoiseGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// create a smoothed white noise
				return this.noise.Process (input);
			}

			public VioletNoiseGenerator (int seed, uint count, float smoothFactor) {
				smoothFactor = smoothFactor > 1f
					? 1f
					: (smoothFactor < -1f)
						? -1f
						: smoothFactor
						;

				this.noise = new Chain<IList<float>> ();
				this.noise.Add (new WhiteNoiseGenerator (seed, count));
				this.noise.Add (new NeighbourMutator ((float a, float b) => {
					return smoothFactor * Mathf.Abs (1f - (a + b));
				}));
			}

			private Chain<IList<float>> noise;

		}
		#endregion stinking lame generators

		#region kinda cool generators based on sine waves
		public class SineNoiseGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// generate a random offset
				var phase = this.rng.Range (0, 2f * Mathf.PI);

				// combine previous values with sine values
				for (var i = 0; i < input.Count; ++i) {
					input[i] = input[i] * this.Sine (this.frequency, i, input.Count, phase);
				}

				// how many elements must be added?
				var missingElements = this.count - input.Count;

				// append missing values
				for (var i = 0; i < missingElements; ++i) {
					input.Add (this.Sine (this.frequency, i, missingElements, phase));
				}

				return input;
			}

			public SineNoiseGenerator (int seed, uint count, float frequency) {
				this.rng = new UniformRandomNumberGenerator (seed);
				this.count = count;
				this.frequency = frequency;
			}

			private float Sine (float frequency, float x, float length, float phase) {
				const float pi2 = 2f * Mathf.PI;

				// reduce the y size to one half and
				// add an additional half to elevate 
				// the zero line, so that there are no
				// negative values
				return 0.5f + 0.5f * Mathf.Sin (pi2 * frequency * x / length + phase);
			}

			private IRandomNumberGenerator rng;
			private uint count;
			private float frequency;

		}

		public class AmplitudeMutator : IChainLink<IList<float>> {
			
			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				for (var index = 0; index < input.Count; ++index) {
					input[index] = amplitude * input[index];
				}

				return input;
			}

			public AmplitudeMutator (float amplitude) {
				this.amplitude = amplitude;
			}

			float amplitude;

		}
		
		public delegate float AmplitudeCalculation (float frequency);

		public class AccumulatedAndWeightedNoiseGenerator : IChainLink<IList<float>> {
			
			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
					for (var i = 0; i < this.count; ++i) {
						input.Add (0f);
					}
				}

				var maxValue = 0f;
				foreach (var f in frequencies) {
					var a = this.amplitude (f);
					var n = new SineNoiseGenerator (this.seed, this.count, f).Process (null);

					for (var i = 0; i < this.count; ++i) {
						input[i] = input[i] + a * n[i];
						maxValue = maxValue < input[i] ? input[i] : maxValue;
					}
				}

				// scale down the sums so they dont exceed 1f
				for (var i = 0; i < this.count; ++i) {
					input[i] = input[i] / maxValue;
				}

				return input;
			}

			public AccumulatedAndWeightedNoiseGenerator (int seed, uint count, AmplitudeCalculation amplitude) {
				this.seed = seed;
				this.count = count;
				this.amplitude = amplitude;
			}

			private int seed;
			private uint count;
			private AmplitudeCalculation amplitude;
			private static float[] frequencies = { 1, 2, 4, 8, 16, 32 };

		}
		#endregion kinda cool generators based on sine waves

		public class SimplexGenerator : IChainLink<IList<float>> {

			public IList<float> Process (IList<float> input) {
				if (null == input) {
					input = new List<float> ();
				}

				// combine previous values with sine values
				for (var i = 0; i < input.Count; ++i) {
					input[i] = input[i] * this.GetNoise (i);
				}

				// how many elements must be added?
				var missingElements = this.count - input.Count;

				// append missing values
				for (var i = 0; i < missingElements; ++i) {
					input.Add (this.GetNoise (i));
				}

				return input;
			}

			public SimplexGenerator (int seed, uint count, float frequency) {
				this.rng = new UniformRandomNumberGenerator (seed);
				this.count = count;
				this.frequency = frequency;
			}

			private float GetNoise (float x) {
				var zySeedRange = this.frequency;
				var ySeed = this.rng.Range (-zySeedRange, zySeedRange);
				var zSeed = this.rng.Range (-zySeedRange, zySeedRange);
				var diceRoll = SimplexNoise.GetNoise (x, ySeed, zSeed);

				return diceRoll;
			}

			private IRandomNumberGenerator rng;
			private uint count;
			private float frequency;

		}
	}
}

