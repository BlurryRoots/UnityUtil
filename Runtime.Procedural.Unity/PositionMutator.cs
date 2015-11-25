using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots.Procedural.Implementation {

	[System.Serializable]
	public class PositionMutator : IChainLink<List<GameObject>> {

		public List<GameObject> Process (List<GameObject> input) {
			var results = new List<GameObject> ();

			foreach (var obj in input) {
				obj.transform.position = this.position;
				results.Add (obj);
			}

			return results;
		}

		public PositionMutator (Vector3 position) {
			this.position = position;
		}

		[SerializeField]
		private Vector3 position;

	}

}