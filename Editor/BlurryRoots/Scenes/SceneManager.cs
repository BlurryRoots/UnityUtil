using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

class SceneManager : EditorWindow {
	private const string MenuTitle = "SceneManager";

	private static SceneInfo[] scenes;

	[MenuItem ("Window/" + MenuTitle)]
	public static void ShowWindow () {
		var editorWindow = EditorWindow.GetWindow<SceneManager> ();
		editorWindow.OnCreate ();
		editorWindow.Focus ();
	}

	void OnCreate () {
		this.titleContent.text = "Scene Loader";
		this.minSize = new Vector2 (128f, 240f);

		this.OnLocateScenes ();
	}

	void OnDestroy () {
		Debug.Log (this + " got destroyed!");
	}

	void OnLocateScenes () {
		var info = new DirectoryInfo (Application.dataPath);
		var sceneFiles = info.GetFiles ("*.unity", SearchOption.AllDirectories);

		scenes = new SceneInfo[sceneFiles.Length];
		for (var i = 0; i < sceneFiles.Length; ++i) {
			var file = sceneFiles[i];

			var seperators = new string[] { "." };
			var options = System.StringSplitOptions.RemoveEmptyEntries;
			var splitName = file.Name.Split (seperators, options);
			var fileName = splitName[0].ToString ();
			var filePath = file.FullName;

			scenes[i] = new SceneInfo (fileName, filePath);
		}
	}

	static Vector2 scrollPosition;
	void OnGUI () {
		GUILayout.Space (6f);


		if (GUILayout.Button ("Locate Scenes")) {
			OnLocateScenes ();
		}

		GUILayout.Space (12f);

		GUILayout.Label ("Scenes in project:");

		scrollPosition = GUILayout.BeginScrollView (scrollPosition, false, true);
		foreach (var scene in scenes) {
			GUILayout.BeginHorizontal ();

			var buttonContent = new GUIContent ("Open");
			var buttonSize = GUI.skin.button.CalcSize (buttonContent);
			var width = GUILayout.Width (buttonSize.x);
			if (GUILayout.Button (buttonContent, width)) {
				if (EditorApplication.SaveCurrentSceneIfUserWantsTo ()) {
					EditorApplication.OpenScene (scene.Path);
				}
			}

			GUILayout.Label (scene.Name);

			GUILayout.EndHorizontal ();
		}
		GUILayout.EndScrollView ();

		GUILayout.Space (12f);

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.Label ("Thanks for using Scene Loader.");
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.Label ("www.cryzen.com");
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
	}

}

struct SceneInfo {

	public string Name;
	public string Path;

	public SceneInfo (string name, string path) {
		this.Name = name;
		this.Path = path;
	}

}