using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlurryRoots.Procedural {

    public interface IChain<TValueType, TLinkType> {

        /// <summary>
		/// Links (apends) a new chain link element.
		/// </summary>
		/// <param name="element">Element to link to chain.</param>
		/// <returns>Reference to chain itself.</returns>
		IChain<TValueType, TLinkType> Link (TLinkType element);

        /// <summary>
        /// Get a reference to all elements linked in this chain.
        /// </summary>
        List<TLinkType> Elements {
            get;
        }

    }

}
