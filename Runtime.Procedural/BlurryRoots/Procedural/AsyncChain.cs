using System.Collections.Generic;

namespace BlurryRoots.Procedural {

	[System.Serializable]
	public class AsyncChain<TValueType> : IAsyncChainLink<TValueType> {

		public TValueType Process (float deltaTime, TValueType input) {
			var result = input;

			foreach (var node in this.elements) {
				result = node.Process (deltaTime, result);
			}

			return result;
		}

		/// <summary>
		/// Links (apends) a new chain link element.
		/// </summary>
		/// <param name="element">Element to link to chain.</param>
		/// <returns>Reference to chain itself.</returns>
		public AsyncChain<TValueType> Link (IAsyncChainLink<TValueType> element) {
			this.elements.Add (element);

			return this;
		}

		/// <summary>
		/// Get a reference to all elements linked in this chain.
		/// </summary>
		public List<IAsyncChainLink<TValueType>> Elements {
			get { return this.elements; }
		}

		/// <summary>
		/// Creates a new Chain.
		/// </summary>
		public AsyncChain () {
			this.elements = new List<IAsyncChainLink<TValueType>> ();
		}

		/// <summary>
		/// Elements in this chain.
		/// </summary>
		private List<IAsyncChainLink<TValueType>> elements;

	}

}