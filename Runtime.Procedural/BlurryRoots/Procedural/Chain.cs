using System.Collections.Generic;

namespace BlurryRoots.Procedural {

    // TODO: split data and processing ?!

    /// <summary>
    /// Basic implementation of a chain based on a list, holding generic chain links.
    /// </summary>
    /// <typeparam name="TValueType">Type of data to be processed by the chain and its links.</typeparam>
    [System.Serializable]
	public class Chain<TValueType> : IChainLink<TValueType> {

		public TValueType Process (TValueType input) {
			var result = input;

			foreach (var node in this.elements) {
				result = node.Process (result);
			}

			return result;
		}

		/// <summary>
		/// Links (apends) a new chain link element.
		/// </summary>
		/// <param name="element">Element to link to chain.</param>
		/// <returns>Reference to chain itself.</returns>
		public Chain<TValueType> Link (IChainLink<TValueType> element) {
			this.elements.Add (element);

			return this;
		}

		/// <summary>
		/// Get a reference to all elements linked in this chain.
		/// </summary>
		public List<IChainLink<TValueType>> Elements {
			get { return this.elements; }
		}

		/// <summary>
		/// Creates a new Chain.
		/// </summary>
		public Chain () {
			this.elements = new List<IChainLink<TValueType>> ();
		}

		/// <summary>
		/// Elements in this chain.
		/// </summary>
		private List<IChainLink<TValueType>> elements;

	}

}