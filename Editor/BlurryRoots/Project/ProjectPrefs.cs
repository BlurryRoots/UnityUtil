using UnityEngine;
using UnityEditor;

namespace BlurryRoots {
	namespace Editor {

		public class ProjectPrefs : EditorWindow {

			[MenuItem ("Window/" + MenuTitle)]
			public static void ShowWindow () {
				var editorWindow = EditorWindow.GetWindow<ProjectPrefs> ();
				editorWindow.Focus ();
			}

			void OnGUI () {
				if (GUILayout.Button ("Add Int")) {
					this.storage.SetInt ("asdf" + this.storage.Count, 42 + this.storage.Count);
				}

				foreach (var entry in this.storage.GetAllEntries ()) {
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Key: " + entry.Key);
					GUILayout.Label ("Type: " + entry.Type);
					switch (entry.Type) {
						case ProjectPrefsEntry.ValueType.Bool:
							GUILayout.Label ("Value: " + entry.Bool);
							break;
						case ProjectPrefsEntry.ValueType.Float:
							GUILayout.Label ("Value: " + entry.Float);
							break;
						case ProjectPrefsEntry.ValueType.Int:
							GUILayout.Label ("Value: " + entry.Int);
							break;
						case ProjectPrefsEntry.ValueType.String:
							GUILayout.Label ("Value: " + entry.String);
							break;
					}
					GUILayout.EndHorizontal ();
				}
			}

			void OnDestroy () {
				Debug.Log (this + " got destroyed!");
			}

			public ProjectPrefs () {
				this.storage = new ProjectPrefsStorage ();
				this.titleContent.text = "ProjectPrefs";
				this.minSize = new Vector2 (128f, 240f);
			}

			private ProjectPrefsStorage storage;

			private static ProjectPrefs Instance;

			private const string MenuTitle = "Project Preferences";

		}

	}
}