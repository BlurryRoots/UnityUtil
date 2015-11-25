using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BlurryRoots.Events;

namespace BlurryRoots {
	namespace Inputs {

		public class InputSystem : MonoBehaviour {

			public EventBusSystem EventBusSystem;

			public InputManager InputManager {
				get;
				private set;
			}

			void Awake () {
				this.InputManager.AxisChanged += this.OnAxisEvent<InputAxisChangedEvent>;
				this.InputManager.AxisDown += this.OnAxisEvent<InputAxisDownEvent>;
				this.InputManager.AxisUp += this.OnAxisEvent<InputAxisUpEvent>;
				this.InputManager.AxisPressed += this.OnAxisEvent<InputAxisPressedEvent>;
			}

			void OnAxisEvent<TAxisEvent> (TAxisEvent e) {
				this.EventBusSystem.EventBus.Raise (e);
			}

			void Update () {
				this.InputManager.OnUpdate ();
			}

			public InputSystem () {
				this.InputManager = new InputManager ();
			}

		}

	}
}