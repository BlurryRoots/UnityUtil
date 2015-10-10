using UnityEngine;
using System.Collections;

namespace BlurryRoots {
	namespace Inputs {

		public class InputAxisEvent {

			public string Axis;

			public InputAxisEvent (string axis) {
				this.Axis = axis;
			}
		}

		public class InputAxisPressedEvent : InputAxisEvent {

			public float CurrentValue;

			public InputAxisPressedEvent (string axis, float cur)
				: base (axis) {
				this.CurrentValue = cur;
			}

		}

		public class InputAxisDownEvent : InputAxisPressedEvent {

			public InputAxisDownEvent (string axis, float cur)
				: base (axis, cur) {
				//
			}

		}

		public class InputAxisUpEvent : InputAxisPressedEvent {

			public InputAxisUpEvent (string axis, float cur)
				: base (axis, cur) {
				//
			}

		}

		public class InputAxisChangedEvent : InputAxisPressedEvent {

			public float PreviousValue;

			public InputAxisChangedEvent (string axis, float prev, float cur)
				: base (axis, cur) {
				this.PreviousValue = prev;
			}

		}

	}
}