namespace BlurryRoots {
	namespace Commands {

		/// <summary>
		/// Describes a executable command.
		/// </summary>
		public interface ICommand {

			/// <summary>
			/// Executes the command.
			/// </summary>
			void Execute ();

		}

	}
}