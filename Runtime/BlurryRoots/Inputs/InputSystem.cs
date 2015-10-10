using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BlurryRoots.Events;

namespace BlurryRoots {
	namespace Inputs {

		public class InputSystem : MonoBehaviour {

			public EventSystem EventSystem;

			public InputManager InputManager {
				get;
				private set;
			}

			// Use this for initialization
			void Awake () {
				this.InputManager.RegisterAxis ("Horizontal");
				this.InputManager.AxisPressed += InputManager_AxisPressed;
				this.EventSystem.EventManager.Subscribe<InputAxisPressedEvent> (this.OnAxisPressed);
			}

			void InputManager_AxisPressed (InputAxisPressedEvent e) {
				this.EventSystem.EventManager.Raise (e);
			}

			void OnAxisPressed (InputAxisPressedEvent e) {
				Debug.Log (e);
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