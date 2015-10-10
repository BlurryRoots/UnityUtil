using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Events {

		/// <summary>
		/// System handling the processing and distribution of raised events.
		/// </summary>
		public class EventSystem : MonoBehaviour {

			public EventManager EventManager {
				get {
					return this.eventManager;
				}
			}

			void Awake () {
			}

			// Handle events in late update, so every change has already been made.
			void LateUpdate () {
				eventManager.DispatchRaisedEvents ();
			}

			public EventSystem () {
				this.eventManager = new EventManager ();
			}

			private EventManager eventManager;

		}

	}
}