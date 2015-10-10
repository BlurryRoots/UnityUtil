using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof (MultiTextureSprite))]
public class MultiTextureSpriteInspector : Editor {

	public float LayerDistance = 0.1f;

	public override void OnInspectorGUI () {
		if (GUILayout.Button ("Arrange")) {
			this.OnArrange ();
		}

		base.OnInspectorGUI ();
	}

	void OnArrange () {
		var mts = (MultiTextureSprite)this.target;
		var parentPosition = mts.gameObject.transform.position;
		var zOffset = 0f;
		var layers = mts.Layers;

		foreach (var layer in layers) {
			var position = parentPosition;

			position.z += zOffset;

			layer.gameObject.transform.position = position;

			// Negative because it is 'towards' the camera
			zOffset -= this.LayerDistance;
		}
	}

}
