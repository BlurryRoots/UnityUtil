using System;
using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace States {

		/// <summary>
		/// Manages states.
		/// </summary>
		/// <typeparam name="TTransitionEnum">Enum type representing the type of a state.</typeparam>
		public class StateManager<TTransitionEnum>
		where TTransitionEnum : struct, System.IConvertible, IComparable {

			/// <summary>
			/// Delegate used to handle a state change event.
			/// </summary>
			/// <param name="from">State exited.</param>
			/// <param name="to">State entered.</param>
			public delegate void StateChanagedEvent (TTransitionEnum from, TTransitionEnum to);

			/// <summary>
			/// Gets invoked if state has been change.
			/// </summary>
			public event StateChanagedEvent StateChanged;

			/// <summary>
			/// Gets the current state.
			/// </summary>
			public TTransitionEnum CurrentState {
				get {
					return this.state;
				}
			}

			/// <summary>
			/// Updates the state machine.
			/// </summary>
			/// <param name="dt">Time since last update in seconds.</param>
			public void OnUpdate (float dt) {
#if DEBUG
				this.CheckIfPoolHasEveryStateOrThrow ();
#endif
				// transition to the next state
				var currentStateObject = this.statePool[this.state];
				var next = currentStateObject.OnUpdate (dt);
				this.ChangeState (next);
			}

			/// <summary>
			/// Creates a new state.
			/// </summary>
			/// <param name="initialState"></param>
			public StateManager (TTransitionEnum initialState) {
				this.state = initialState;
				this.statePool = new Dictionary<TTransitionEnum, IState<TTransitionEnum>> ();
			}

			/// <summary>
			/// Changes the current state. Does nothing if given state equals current state.
			/// </summary>
			/// <param name="next">State to change to.</param>
			private void ChangeState (TTransitionEnum next) {
				// Is the next state different from the current state?
				var isNotEqual = !EqualityComparer<TTransitionEnum>.Default.Equals (next, this.state);
				// If so transition to the next state
				if (isNotEqual) {
					// Exit current state
					this.statePool[this.state].OnExit ();
					// Change to next state
					var oldState = this.state;
					this.state = next;
					// Enter next state
					this.statePool[this.state].OnEnter ();

					// Tell everyone that state has been changed!
					this.OnStateChanged (oldState, this.state);
				}
			}

			/// <summary>
			/// Invokes the StateChanged event.
			/// </summary>
			/// <param name="from">State exited.</param>
			/// <param name="to">State entered.</param>
			private void OnStateChanged (TTransitionEnum from, TTransitionEnum to) {
				if (null != this.StateChanged) {
					this.StateChanged (from, to);
				}
			}

#if DEBUG
			/// <summary>
			/// Holding enumarble with all fields in state enum.
			/// </summary>
			private IEnumerable<TTransitionEnum> stateTypeValues =
					BlurryRoots.Common.EnumTypeEnumerator<TTransitionEnum>.GetValues ();

			/// <summary>
			/// Checks if <see cref="statePool"/> holds all necessary state objects.
			/// <exception cref="System.Exception">Is thrown if not every state type is covered.</exception>
			/// </summary>
			private void CheckIfPoolHasEveryStateOrThrow () {
				var missingStateList = new System.Text.StringBuilder ();

				foreach (var v in stateTypeValues) {
					if (!this.statePool.ContainsKey (v)) {
						missingStateList.Append (v);
						missingStateList.Append ("\n");
					}
				}

				if (0 < missingStateList.Length) {
					throw new Exception (
						"State pool doesn't hold a state object of types:" + missingStateList.ToString ()
					);
				}
			}
#endif

			/// <summary>
			/// Current state.
			/// </summary>
			private TTransitionEnum state;
			/// <summary>
			/// Holds all state instances.
			/// </summary>
			private Dictionary<TTransitionEnum, IState<TTransitionEnum>> statePool;

		}

	}
}