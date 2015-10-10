using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Commands {

		/// <summary>
		/// Used to queue commands for later executaion.
		/// </summary>
		public class CommandQueue : ICommand {

			/// <summary>
			/// Enqueues given command into the queue.
			/// </summary>
			/// <param name="command">Command to enqueue.</param>
			public void Enqueue (ICommand command) {
				this.commands.Enqueue (command);
			}

			/// <summary>
			/// Executes all queued commands.
			/// </summary>
			public void Execute () {
				while (0 < this.commands.Count) {
					var command = this.commands.Dequeue ();
					command.Execute ();
				}
			}

			/// <summary>
			/// Creates a new CommandQueue
			/// </summary>
			public CommandQueue () {
				this.commands = new Queue<ICommand> ();
			}

			/// <summary>
			/// Holding all queued commands.
			/// </summary>
			private Queue<ICommand> commands;

		}

	}
}