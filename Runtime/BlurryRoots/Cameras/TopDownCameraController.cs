using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BlurryRoots {
	namespace Cameras {

		public class TopDownCameraController : CameraController {

			public TopDownCameraController (Camera camera, float panSpeed, float yawSpeed)
				: base (camera) {
				this.panSpeed = panSpeed;
				this.yawSpeed = yawSpeed;
			}

			public void Pan (float value) {
				var cgo = this.camera.gameObject;
				cgo.transform.position += cgo.transform.right * value * Time.deltaTime;
			}
			public void PanLeft () {
				this.Pan (-1 * this.panSpeed);
			}
			public void PanRight () {
				this.Pan (this.panSpeed);
			}

			public void Move (float value) {
				Vector3 straight = Vector3.Cross (this.camera.transform.right, Vector3.up);
				var cgo = this.camera.gameObject;
				cgo.transform.position += straight * value * Time.deltaTime;
			}
			public void MoveForwards () {
				this.Move (this.panSpeed);
			}
			public void MoveBackwards () {
				this.Move (-1 * this.panSpeed);
			}

			public void Yaw (float value) {
				var cgo = this.camera.gameObject;
				cgo.transform.RotateAround (cgo.transform.position, Vector3.up, value);
			}
			public void YawLeft () {
				this.Yaw (-1 * this.yawSpeed);
			}
			public void YawRight () {
				this.Yaw (this.yawSpeed);
			}

			public void Rotate (float value) {
				this.camera.transform.Rotate (Vector3.up, value, Space.World);

			}

			public Vector3 Position {
				get { return this.camera.transform.position; }
				set { this.camera.transform.position = value; }
			}

			public void LookAt (Vector3 position, float playerRotation, float height, float rotation, float distance) {
				var rads = rotation - playerRotation;

				this.Position = position
						- new Vector3 (
								Mathf.Cos (rads),
								0,
								Mathf.Sin (rads)
						) * distance
						+ new Vector3 (0, height, 0)
						;

				this.camera.transform.LookAt (position);
			}

			private float panSpeed;
			private float yawSpeed;

		}

	}
}