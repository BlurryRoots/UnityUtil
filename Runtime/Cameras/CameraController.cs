using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BlurryRoots {
	namespace Cameras {

		public class CameraController {

			public CameraController (Camera camera) {
				this.camera = camera;
			}

			public int CalcLayerMask (string layerName) {
				return 1 << LayerMask.NameToLayer (layerName);
			}

			public bool FireRayIntoScene (Vector3 from, float until, string layerName, out RaycastHit hit) {
				Ray ray = this.camera.ScreenPointToRay (from);
				Debug.DrawRay (ray.origin, ray.direction * 100, Color.red, 2);
				return Physics.Raycast (ray, out hit, until, this.CalcLayerMask (layerName));
			}

			protected Camera camera;
			private Ray lastRay;

		}

	}
}