using UnityEngine;

namespace BlurryRoots {
	namespace Common {

		/// <summary>
		/// Used by <see cref="TimedTrigger"/> when time is up.
		/// </summary>
		public delegate void TriggerCallback ();

		/// <summary>
		/// Used to invoke callback when a ceratin amount of time has passed.
		/// </summary>
		public class TimedTrigger {

			/// <summary>
			/// Gets or sets weather the trigger is active. Does not reset already passed time.
			/// </summary>
			public bool IsActive {
				get;
				set;
			}

			public float Interval {
				get { return this.timeToWait; }
				set { this.timeToWait = value; }
			}

			/// <summary>
			/// Gets called when time is up.
			/// </summary>
			public event TriggerCallback TimeIsUp;

			/// <summary>
			/// Time remaining until trigger.
			/// </summary>
			public float RemainingTime {
				get {
					return done
						? 0f
						: (this.timeToWait - this.passedTime)
						;
				}
			}

			/// <summary>
			/// Indicates if trigger has already been triggered.
			/// </summary>
			public bool HasTriggered {
				get {
					return this.done;
				}
			}

			/// <summary>
			/// Ticks a certain amount of time. Does nothing, if time has alread trigger.
			/// </summary>
			/// <param name="dt">Time to tick in seconds.</param>
			public void Tick (float dt) {
				// Stop if already trigged or paused
				if (! this.IsActive || done) {
					return;
				}

				// If the time to wait is up trigger
				if (this.timeToWait < this.passedTime) {
					// Mark this trigger as done
					// Do this before invoking callback, it could reset the trigger in it!
					this.done = true;

					// Only invoke event if one is registered.
					if (null != this.TimeIsUp) {
						this.TimeIsUp.Invoke ();
					}

					// If trigger should go on forever, reset it after triggering.
					if (this.continousTriggering) {
						this.Reset ();
					}
				}
				else {
					// Increase time passed by tick time.
					this.passedTime += dt;
				}
			}

			/// <summary>
			/// Resets the trigger to its inital state.
			/// </summary>
			public void Reset () {
				this.done = false;
				this.passedTime = 0;
			}

			/// <summary>
			/// Creates a new <see cref="TimedTrigger"/>.
			/// </summary>
			/// <param name="timeToWait">Time in seconds to wait.</param>
			/// <param name="continous">Checks weather trigger should reset itself after triggering.</param>
			public TimedTrigger (float timeToWait, bool continous = false) {
				this.timeToWait = timeToWait;
				this.IsActive = true;
				this.continousTriggering = continous;

				this.Reset ();
			}

			/// <summary>
			/// Time in seconds until trigger.
			/// </summary>
			private float timeToWait;
			/// <summary>
			/// Time already passed.
			/// </summary>
			private float passedTime;
			/// <summary>
			/// If trigger has already been triggerd.
			/// </summary>
			private bool done;
			/// <summary>
			/// Should trigger reset itself after triggering.
			/// </summary>
			private bool continousTriggering;

		}

	}
}