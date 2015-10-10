using System;
using System.Collections;

namespace BlurryRoots {
	namespace States {

		/// <summary>
		/// Describes a state.
		/// </summary>
		/// <typeparam name="TTransitionEnum">Enum type identifying the state used on transition.</typeparam>
		public interface IState<TTransitionEnum>
		where TTransitionEnum : struct, System.IConvertible, IComparable {

			/// <summary>
			/// Called when this state is entered.
			/// </summary>
			void OnEnter ();

			/// <summary>
			/// Called when this state is updated.
			/// </summary>
			/// <param name="dt">Time since last update in seconds.</param>
			/// <returns>Enum identifying next state.</returns>
			TTransitionEnum OnUpdate (float dt);

			/// <summary>
			/// Called when this state exits.
			/// </summary>
			void OnExit ();

		}

	}
}