using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots.Procedural.Implementation {

	[System.Serializable]
	public class PrimitveGenerator : IChainLink<List<GameObject>> {

		public List<GameObject> Process (List<GameObject> input) {
			var objects = new List<GameObject> ();
			if (null != input) {
				objects.AddRange (input);
			}

			for (var i = 0u; i < this.amount; ++i) {
				objects.Add (GameObject.CreatePrimitive (this.type));
			}

			return objects;
		}

		public PrimitiveType Type {
			get { return this.type; }
		}
		public uint Amount {
			get { return this.amount; }
		}

		public PrimitveGenerator (PrimitiveType type, uint amount) {
			this.type = type;
			this.amount = amount;
		}

		[SerializeField]
		private PrimitiveType type;
		[SerializeField]
		private uint amount;

	}

}
