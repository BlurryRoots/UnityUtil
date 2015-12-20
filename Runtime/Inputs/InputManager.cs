using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Inputs {

		/// <summary>
		/// Manager responsible for managing input state.
		/// </summary>
		[System.Serializable]
		public class InputManager {

			/// <summary>
			/// Delegate used to register with input events.
			/// </summary>
			/// <typeparam name="TInputEvent">Type of input event.</typeparam>
			/// <param name="e">Input event.</param>
			public delegate void InputEventDelegate<TInputEvent> (TInputEvent e);

			/// <summary>
			/// Called when an axis has changed its value.
			/// </summary>
			public event InputEventDelegate<InputAxisChangedEvent> AxisChanged;
			/// <summary>
			/// Called when an axis has changed its value to be more.
			/// </summary>
			public event InputEventDelegate<InputAxisUpEvent> AxisUp;
			/// <summary>
			/// Called when an axis has changed its value to be less.
			/// </summary>
			public event InputEventDelegate<InputAxisDownEvent> AxisDown;
			/// <summary>
			/// Called when an axis has a value.
			/// </summary>
			public event InputEventDelegate<InputAxisPressedEvent> AxisPressed;

			/// <summary>
			/// Gets a list of all registered axes.
			/// </summary>
			public IList<string> RegisteredAxes {
				get {
					return new List<string> (this.axes.Keys);
				}
			}

			/// <summary>
			/// Gets or sets if Unity should smoothe input axes.
			/// </summary>
			public bool UnitySmoothing {
				get;
				set;
			}

			/// <summary>
			/// Register a certain axis.
			/// </summary>
			/// <param name="name">Name of axis.</param>
			public void RegisterAxis (string name) {
				if (!this.axes.ContainsKey (name)) {
					this.axes.Add (name, 0);
				}
			}

			/// <summary>
			/// Clears all registrations.
			/// </summary>
			public void ClearRegistrations () {
				this.axes = new Dictionary<string, float> ();
			}

			/// <summary>
			/// Updates the state of the input manager.
			/// </summary>
			public void OnUpdate () {
				// Cache the axis names so we can iterate over them and change the dictionary while doing that
				IList<string> axisNames = new List<string> (this.axes.Keys);

				foreach (string axisName in axisNames) {
					// Store the previous value and update to the new value
					float prevValue = this.axes[axisName];
					// Get current smoothed or raw value
					float curValue = this.UnitySmoothing
						? Input.GetAxis (axisName)
						: Input.GetAxisRaw (axisName)
						;

					this.axes[axisName] = curValue;

					// Current state
					// Check if the value has been changed
					var hasChanged = float.Epsilon < Mathf.Abs (prevValue - curValue);
					// If value has been change up
					//var changedUp = float.Epsilon > Mathf.Abs (curValue)
					//	&& float.Epsilon < Mathf.Abs (prevValue);
					var changedUp = 0 < curValue
						&& (0 == prevValue
							|| Mathf.Sign (prevValue) != Mathf.Sign (curValue));
					// If value has been changed down
					//var changedDown = float.Epsilon < Mathf.Abs (curValue)
					//	&& float.Epsilon < Mathf.Abs (prevValue);
					var changedDown = 0 > curValue
						&& (0 == prevValue
							|| Mathf.Sign (prevValue) != Mathf.Sign (curValue)); ;
					// Check if current value has a non zero value
					var hasValue = float.Epsilon < Mathf.Abs (curValue);

					if (hasChanged) {
						this.OnAxisChanged (axisName, prevValue, curValue);

						if (hasValue && Mathf.Abs (prevValue) == 0) {
							this.OnAxisPressed (axisName, curValue);
						}

						if (changedUp) {
							this.OnAxisUp (axisName, curValue);
						}

						if (changedDown) {
							this.OnAxisDown (axisName, curValue);
						}
					}
				}
			}

			/// <summary>
			/// Creates a new <see cref="InputManager"/>.
			/// </summary>
			public InputManager () {
				this.axes = new Dictionary<string, float> ();
			}

			/// <summary>
			/// Tries to trigger an event handler (if one is present) with given event.
			/// </summary>
			/// <typeparam name="TInputEvent">Type of event to handle.</typeparam>
			/// <param name="handler">Handler responsible for event.</param>
			/// <param name="e">Event to handle.</param>
			private void OnAxisEvent<TInputEvent> (InputEventDelegate<TInputEvent> handler, TInputEvent e) {
				if (null != handler) {
					handler.Invoke (e);
				}
			}

			/// <summary>
			/// Raises an <see cref="InputAxisChangedEvent"/>.
			/// </summary>
			/// <param name="axisName">Name of the axis.</param>
			/// <param name="prevValue">Previous value.</param>
			/// <param name="curValue">Current value.</param>
			private void OnAxisChanged (string axisName, float prevValue, float curValue) {
				this.OnAxisEvent (this.AxisChanged,
					new InputAxisChangedEvent (axisName, prevValue, curValue)
				);
			}

			/// <summary>
			/// Raises an <see cref="InputAxisDownEvent"/>.
			/// </summary>
			/// <param name="axisName">Name of the axis.</param>
			/// <param name="curValue">Current value.</param>
			private void OnAxisDown (string axisName, float curValue) {
				this.OnAxisEvent (this.AxisDown,
						new InputAxisDownEvent (axisName, curValue)
				);
			}

			/// <summary>
			/// Raises an <see cref="InputAxisUpEvent"/>.
			/// </summary>
			/// <param name="axisName">Name of the axis.</param>
			/// <param name="curValue">Current value.</param>
			private void OnAxisUp (string axisName, float curValue) {
				this.OnAxisEvent (this.AxisUp,
						new InputAxisUpEvent (axisName, curValue)
				);
			}

			/// <summary>
			/// Raises an <see cref="InputAxisPressedEvent"/>.
			/// </summary>
			/// <param name="axisName">Name of the axis.</param>
			/// <param name="curValue">Current value.</param>
			private void OnAxisPressed (string axisName, float curValue) {
				this.OnAxisEvent (this.AxisPressed,
						new InputAxisPressedEvent (axisName, curValue)
				);
			}

			/// <summary>
			/// Stores the names and vaules of the axes to be processed
			/// </summary> 
			private Dictionary<string, float> axes;

		}

	}
}