using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots.Procedural {

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

	[System.Serializable]
	public class CircularPlacementMutator : IChainLink<List<GameObject>> {

		public List<GameObject> Process (List<GameObject> input) {
			var results = new List<GameObject> ();

			var steps = (2f * Mathf.PI) / input.Count;
			for (var i = 0; i < input.Count; ++i) {
				var obj = input[i];
				var angle = i * steps;

				var x = Mathf.Cos (angle) * this.radius + obj.transform.position.x;
				var y = obj.transform.position.y;
				var z = Mathf.Sin (angle) * this.radius + obj.transform.position.z;

				obj.transform.position = new Vector3 (x, y, z);
				results.Add (obj);
			}

			return results;
		}

		public CircularPlacementMutator (float radius) {
			this.radius = radius;
		}

		[SerializeField]
		float radius;

	}

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