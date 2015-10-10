using UnityEngine;
using UnityEditor;
using System.Collections;
using BlurryRoots.Inputs;

[CustomEditor (typeof (InputSystem))]
[CanEditMultipleObjects]
public class InputSystemEditor : Editor {

    public void UpdateAxes () {
        var inputSystem = (InputSystem)this.target;

        this.ReadAxes (inputSystem);
    }

    public override void OnInspectorGUI () {
        base.OnInspectorGUI ();
                
        if (GUILayout.Button ("Update Axes")) {
            this.UpdateAxes ();
        }

				var inputSystem = (InputSystem)this.target;
				var sb = new System.Text.StringBuilder ();
				foreach (var axis in inputSystem.InputManager.RegisteredAxes) {
					sb.Append (axis).Append ("\n");
				}
				GUILayout.TextField (sb.ToString ());
    }

    public void ReadAxes (InputSystem inputSystem) {
        var inputManager = AssetDatabase.LoadAllAssetsAtPath ("ProjectSettings/InputManager.asset")[0];
        var obj = new SerializedObject (inputManager);
        var axisArray = obj.FindProperty ("m_Axes");

        if (axisArray.arraySize == 0) {
            Debug.LogError ("No input axes defined!");
        }

        for (int i = 0; i < axisArray.arraySize; ++i) {
            var axis = axisArray.GetArrayElementAtIndex (i);

            var name = axis.FindPropertyRelative ("m_Name").stringValue;
            //var axisVal = axis.FindPropertyRelative ("axis").intValue;
            //var inputType = (InputType)axis.FindPropertyRelative ("type").intValue;

            inputSystem.InputManager.RegisterAxis (name);
        }

        var timestamp = System.DateTime.Now;
        Debug.Log (timestamp + ": Updated " + axisArray.arraySize + " input axes.");
    }

    public enum InputType {
        KeyOrMouseButton,
        MouseMovement,
        JoystickAxis,
    };

    [MenuItem ("Assets/ReadInputManager")]
    public static void DoRead () {
        //
    }

    private bool atLeatOneUpdate;

}
