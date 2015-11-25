using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots.Procedural {

	[System.Serializable]
	public class OddEvenSelector : IChainLink<List<GameObject>> {

		public List<GameObject> Process (List<GameObject> input) {
			var results = new List<GameObject> ();

			for (var i = 0; i < input.Count; ++i) {
				if (this.selector (i)) {
					results.Add (input[i]);
				}
			}

			return results;
		}

		public OddEvenSelector (bool selectOdd) {
			if (selectOdd) {
				selector = (int i) => { return (i + 1) % 2 != 0; };
			}
			else {
				selector = (int i) => { return (i + 1) % 2 == 0; };
			}
		}

		[SerializeField]
		private System.Predicate<int> selector;

	}

}