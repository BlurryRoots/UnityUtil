using System.Collections.Generic;

namespace BlurryRoots.Procedural {

	[System.Serializable]
	public class DeferredChain<TValueType> : IDeferredChainLink<TValueType> {

		public event FinishedProcessing<TValueType> OnFinishedProcessing;

		public void StartProcessing (TValueType input) {
			this.cachedValue = input;
		}

		public void Update (float deltaTime) {
			if (this.elements.Count > this.currentElement) {
				var current = this.elements[this.currentElement];
				current.Update (deltaTime);
			}
			else {
				if (null != this.OnFinishedProcessing) {
					this.OnFinishedProcessing (this.cachedValue);
				}
			}
		}

		/// <summary>
		/// Links (apends) a new chain link element.
		/// </summary>
		/// <param name="element">Element to link to chain.</param>
		/// <returns>Reference to chain itself.</returns>
		public DeferredChain<TValueType> Link (IDeferredChainLink<TValueType> element) {
			element.OnFinishedProcessing += this.OnLinkIsFinished;

			this.elements.Add (element);

			return this;
		}

		/// <summary>
		/// Get a reference to all elements linked in this chain.
		/// </summary>
		public List<IDeferredChainLink<TValueType>> Elements {
			get { return this.elements; }
		}

		/// <summary>
		/// Creates a new DeferredChain.
		/// </summary>
		public DeferredChain () {
			this.elements = new List<IDeferredChainLink<TValueType>> ();
		}

		private void OnLinkIsFinished (TValueType result) {
			++this.currentElement;
			this.cachedValue = result;
		}

		/// <summary>
		/// Elements in this chain.
		/// </summary>
		private List<IDeferredChainLink<TValueType>> elements;
		private int currentElement;
		private TValueType cachedValue;

	}

}