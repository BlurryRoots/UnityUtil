using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots.Procedural.Implementation {

	[System.Serializable]
	public class ColorMutator : IChainLink<List<GameObject>> {

		public List<GameObject> Process (List<GameObject> input) {
			var results = new List<GameObject> ();

			foreach (var obj in input) {
				var mr = obj.GetComponent<MeshRenderer> ();
				if (null != mr) {
					mr.material.color = this.color;
				}

				results.Add (obj);
			}

			return results;
		}

		public ColorMutator (Color color) {
			this.color = color;
		}

		[SerializeField]
		private Color color;

	}

}