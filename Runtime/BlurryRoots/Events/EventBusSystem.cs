using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Events {

		/// <summary>
		/// System handling the processing and distribution of raised events.
		/// </summary>
		public class EventBusSystem : MonoBehaviour {

			public EventBus EventBus {
				get;
				private set;
			}

			// Handle events in late update, so every change has already been made.
			void LateUpdate () {
				this.EventBus.DispatchRaisedEvents ();
			}

			public EventBusSystem () {
				this.EventBus = new EventBus ();
			}

		}

	}
}