using UnityEngine;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Procedural {

		[System.Serializable]
		public class Chain<TValueType> : List<IChainLink<TValueType>>, IChainLink<TValueType> {

			public TValueType Process (TValueType input) {
				var result = input;

				foreach (var node in this) {
					result = node.Process (result);
				}

				return result;
			}

			public Chain<TValueType> Link (IChainLink<TValueType> element) {
				this.Add (element);

				return this;
			}

		}

	}
}