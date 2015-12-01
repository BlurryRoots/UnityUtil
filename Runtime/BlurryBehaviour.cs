using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlurryRoots {

	public class BlurryBehaviour : MonoBehaviour {

		#region Event types
		/// <summary>
		/// Types of collision events.
		/// </summary>
		public enum CollisionEventType {
			Enter = 1,
			Stay = 2,
			Exit = 3,
		}

		/// <summary>
		/// Types of hierachy change events.
		/// </summary>
		public enum HierachyEventType {
			Parent,
			Children,
		}
		#endregion Event types

		#region Runtime implementations
		/// <summary>
		/// OnAwake is called when the script instance is being loaded.
		/// </summary>
		protected virtual void OnAwake () {
			//
		}

		/// <summary>
		/// OnStart is called on the frame when a script is enabled for the first
		/// time just before any of the Update methods is called. This won't be
		/// called again if component is deactivated and reactived again.
		/// </summary>
		protected virtual void OnStart () {
			//
		}

		/// <summary>
		/// Is called when visiblity of the renderer has changed. 
		/// </summary>
		/// <param name="gained">Is false when the renderer is no longer visible
		/// by any camera, true when the renderer became visible by any camera.
		/// </param>
		protected virtual void OnVisibility (bool gained) {
			//
		}

		/// <summary>
		/// Gets called when focus of application has changed.
		/// </summary>
		/// <param name="gained">
		/// Is true if focus is gained, false when lost.
		/// </param>
		protected virtual void OnFocus (bool gained) {
			//
		}

		/// <summary>
		/// Gets called when application is paused.
		/// </summary>
		protected virtual void OnPause () {
			//
		}

		/// <summary>
		/// Gets called when application is resumed.
		/// </summary>
		protected virtual void OnResume () {

		}

		/// <summary>
		/// Gets called right before application is quit.
		/// </summary>
		protected virtual void OnQuit () {
			//
		}

		/// <summary>
		/// Gets called when active state of the component has been changed.
		/// </summary>
		/// <param name="enabled">
		/// Is true when enabled, false when deactived.
		/// </param>
		protected virtual void OnActivate (bool enabled) {
			//
		}

		/// <summary>
		/// Gets called when something in the gameobjects hierachy has changed.
		/// </summary>
		/// <param name="type">Info weather parent or children have changed.</param>
		protected virtual void OnHierachyChange (HierachyEventType type) {
			//
		}

		/// <summary>
		/// Gets called when a level is loaded.
		/// </summary>
		/// <param name="level">Index number of level (scene) loaded.</param>
		protected virtual void OnLevelLoad (int level) {
			//
		}

		/// <summary>
		/// Gets called right before gameobject gets destroyed.
		/// </summary>
		protected virtual void OnDispose () {
			//
		}

		/// <summary>
		/// Gets called within a fixed time frame.
		/// </summary>
		protected virtual void OnFixedUpdate () {
			//
		}

		/// <summary>
		/// Gets called every frame.
		/// </summary>
		protected virtual void OnUpdate () {
			//
		}

		/// <summary>
		/// Gets called after the normal updates.
		/// </summary>
		protected virtual void OnLateUpdate () {
			//
		}

		/// <summary>
		/// Gets called if a 3D collision event happend.
		/// </summary>
		/// <param name="type">Collision type.</param>
		/// <param name="collision">Collsion information.</param>
		protected virtual void OnCollide3D (CollisionEventType type, Collision collision) {
			//
		}

		/// <summary>
		/// Gets called if a 2D collision event happend.
		/// </summary>
		/// <param name="type">Collision type.</param>
		/// <param name="collision">Collsion information.</param>
		protected virtual void OnCollide2D (CollisionEventType type, Collision2D collision) {
			//
		}

		/// <summary>
		/// Gets called when a collision event with a particle happens.
		/// </summary>
		/// <param name="other">Gameobject representing the particle.</param>
		protected virtual void OnCollideParticle (GameObject other) {
			//
		}

		/// <summary>
		/// Gets called when the controller hits a collider while performing a Move.
		/// This can be used to push objects when they collide with the character.
		/// </summary>
		/// <param name="hit"></param>
		protected virtual void OnCollideCharacterController (ControllerColliderHit hit) {
			//
		}

		/// <summary>
		/// Gets called when a collision with a 3D trigger happens.
		/// </summary>
		/// <param name="type">Collision type.</param>
		/// <param name="collider">Collision information.</param>
		protected virtual void OnTrigger3D (CollisionEventType type, Collider collider) {
			//
		}

		/// <summary>
		/// Gets called when a collision with a 2D trigger happens.
		/// </summary>
		/// <param name="type">Collision type.</param>
		/// <param name="collider">Collision information.</param>
		protected virtual void OnTrigger2D (CollisionEventType type, Collider2D collider) {
			//
		}
		#endregion Runtime implementations

		#region Editor implementations
		/// <summary>
		/// Gets called when component values get reset. Only gets called in editor
		/// mode.
		/// </summary>
		public virtual void OnReset () {
			//
		}

		/// <summary>
		/// Gets called when gizmos are about to be drawn.
		/// </summary>
		/// <param name="selected">Is true if gameobject selected.</param>
		public virtual void OnGizmos (bool selected) {
			//
		}

		/// <summary>
		/// Gets called when a value of this component gets changed in editor mode.
		/// </summary>
		public virtual void OnValueChanged () {
			//
		}
		#endregion Editor implementations

		#region Contructor
		/// <summary>
		/// Creates a new BlurryBehaviour.
		/// </summary>
		public BlurryBehaviour () {
			//
		}
		#endregion Constructor

		#region States
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		private void Awake () {
			this.OnAwake ();
		}

		/// <summary>
		/// Start is called on the frame when a script is enabled just before any
		/// of the Update methods is called the first time.
		/// </summary>
		private void Start () {
			this.OnStart ();
		}

		/// <summary>
		/// OnBecameVisible is called when the renderer became visible by any
		/// camera.
		/// </summary>
		private void OnBecameVisible () {
			this.OnVisibility (true);
		}

		/// <summary>
		/// OnBecameInvisible is called when the renderer is no longer visible by
		/// any camera.
		/// </summary>
		private void OnBecameInvisible () {
			this.OnVisibility (false);
		}

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		private void OnEnable () {
			this.OnActivate (true);
		}

		/// <summary>
		/// This function is called when the behaviour becomes disabled () or inactive.
		/// </summary>
		private void OnDisable () {
			this.OnActivate (false);
		}

		/// <summary>
		/// This function is called when the list of children of the transform of
		/// the GameObject has changed.
		/// </summary>
		private void OnTransformChildrenChanged () {
			this.OnHierachyChange (HierachyEventType.Children);
		}

		/// <summary>
		/// This function is called when the parent property of the transform of
		/// the GameObject has changed.
		/// </summary>
		private void OnTransformParentChanged () {
			this.OnHierachyChange (HierachyEventType.Parent);
		}

		/// <summary>
		/// This function is called after a new level was loaded.
		/// </summary>
		/// <param name="level">Level number which is loaded.</param>
		private void OnLevelWasLoaded (int level) {
			this.OnLevelLoad (level);
		}

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		private void OnDestroy () {
			this.OnDispose ();
		}
		#endregion States

		#region Updates
		/// <summary>
		/// This function is called every fixed framerate frame, if the
		/// MonoBehaviour is enabled.
		/// </summary>
		private void FixedUpdate () {
			this.OnFixedUpdate ();
		}

		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		void Update () {
			this.OnUpdate ();
		}

		/// <summary>
		/// LateUpdate is called every frame, if the Behaviour is enabled.
		/// </summary>
		private void LateUpdate () {
			this.OnLateUpdate ();
		}
		#endregion Updates

		#region Animation
		//OnAnimatorIK	Callback for setting up animation IK (inverse kinematics).
		//OnAnimatorMove	Callback for processing animation movements for modifying root motion.
		//OnJointBreak	Called when a joint attached to the same game object broke.
		#endregion Animation

		#region Application
		/// <summary>
		/// Sent to all game objects when the player gets or loses focus.
		/// </summary>
		/// <param name="lostFocus">Focus state.</param>
		private void OnApplicationFocus (bool lostFocus) {
			this.OnFocus (!lostFocus);
		}

		/// <summary>
		/// Sent to all game objects when the player pauses.
		/// </summary>
		/// <param name="isPaused">Pause state.</param>
		private void OnApplicationPause (bool isPaused) {
			if (isPaused) {
				this.OnPause ();
			}
			else {
				this.OnResume ();
			}
		}

		/// <summary>
		/// Sent to all game objects before the application is quit.
		/// </summary>
		private void OnApplicationQuit () {
			this.OnQuit ();
		}
		#endregion Application

		#region Audio
		//OnAudioFilterRead	If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
		#endregion Audio

		#region Collision
		/// <summary>
		/// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionEnter (Collision collision) {
			this.OnCollide3D (CollisionEventType.Enter, collision);
		}

		/// <summary>
		/// OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider.
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionStay (Collision collision) {
			this.OnCollide3D (CollisionEventType.Stay, collision);
		}

		/// <summary>
		/// 	OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionExit (Collision collision) {
			this.OnCollide3D (CollisionEventType.Exit, collision);
		}

		/// <summary>
		/// Sent when an incoming collider makes contact with this object's collider (2D physics only).
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionEnter2D (Collision2D collision) {
			this.OnCollide2D (CollisionEventType.Enter, collision);
		}

		/// <summary>
		/// Sent each frame where a collider on another object is touching this object's collider (2D physics only).
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionStay2D (Collision2D collision) {
			this.OnCollide2D (CollisionEventType.Stay, collision);
		}

		/// <summary>
		/// Sent when a collider on another object stops touching this object's collider (2D physics only).
		/// </summary>
		/// <param name="collision">Collision information.</param>
		private void OnCollisionExit2D (Collision2D collision) {
			this.OnCollide2D (CollisionEventType.Exit, collision);
		}

		/// <summary>
		/// OnParticleCollision is called when a particle hits a collider.
		/// </summary>
		/// <param name="other">Colliding particle game object.</param>
		private void OnParticleCollision (GameObject other) {
			this.OnCollideParticle (other);
		}

		/// <summary>
		/// OnControllerColliderHit is called when the controller hits a collider while performing a Move.
		/// </summary>
		/// <param name="hit">Hit information.</param>
		private void OnControllerColliderHit (ControllerColliderHit hit) {
			this.OnCollideCharacterController (hit);
		}
		#endregion Collision

		#region Trigger
		/// <summary>
		/// OnTriggerEnter is called when the Collider other enters the trigger.
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerEnter (Collider other) {
			this.OnTrigger3D (CollisionEventType.Enter, other);
		}

		/// <summary>
		/// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerStay (Collider other) {
			this.OnTrigger3D (CollisionEventType.Stay, other);
		}

		/// <summary>
		/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerExit (Collider other) {
			this.OnTrigger3D (CollisionEventType.Exit, other);
		}

		/// <summary>
		/// Sent when another object enters a trigger collider attached to this object (2D physics only).
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerEnter2D (Collider2D other) {
			this.OnTrigger2D (CollisionEventType.Enter, other);
		}

		/// <summary>
		/// Sent each frame where another object is within a trigger collider attached to this object (2D physics only).
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerStay2D (Collider2D other) {
			this.OnTrigger2D (CollisionEventType.Stay, other);
		}

		/// <summary>
		/// Sent when another object leaves a trigger collider attached to this object (2D physics only).
		/// </summary>
		/// <param name="other">Collision information.</param>
		private void OnTriggerExit2D (Collider2D other) {
			this.OnTrigger2D (CollisionEventType.Exit, other);
		}
		#endregion Trigger

		#region Network
		//OnConnectedToServer	Called on the client when you have successfully connected to a server.
		//OnDisconnectedFromServer	Called on the client when the connection was lost or you disconnected from the server.
		//OnFailedToConnect	Called on the client when a connection attempt fails for some reason.
		//OnFailedToConnectToMasterServer	Called on clients or servers when there is a problem connecting to the MasterServer.
		//OnMasterServerEvent	Called on clients or servers when reporting events from the MasterServer.
		//OnNetworkInstantiate	Called on objects which have been network instantiated with Network.Instantiate.
		//OnPlayerConnected	Called on the server whenever a new player has successfully connected.
		//OnPlayerDisconnected	Called on the server whenever a player disconnected from the server.
		//OnSerializeNetworkView	Used to customize synchronization of variables in a script watched by a network view.
		//OnServerInitialized	Called on the server whenever a Network.InitializeServer was invoked and has completed.
		#endregion Network

		#region EditorOrInspector
		/// <summary>
		/// Reset to default values.
		/// </summary>
		private void Reset () {
			this.OnReset ();
		}

		//OnDrawGizmos	Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn.
		private void OnDrawGizmos () {
			this.OnGizmos (false);
		}

		//OnDrawGizmosSelected	Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected.
		private void OnDrawGizmosSelected () {
			this.OnGizmos (true);
		}

		//OnValidate	This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
		private void OnValidate () {
			this.OnValueChanged ();
		}
		#endregion EditorOrInspector

		#region GUI
		//OnGUI	OnGUI is called for rendering and handling GUI events.		
		#endregion GUI

		#region MouseInput
		//OnMouseDown	OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
		//OnMouseDrag	OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
		//OnMouseEnter	Called when the mouse enters the GUIElement or Collider.
		//OnMouseExit	Called when the mouse is not any longer over the GUIElement or Collider.
		//OnMouseOver	Called every frame while the mouse is over the GUIElement or Collider.
		//OnMouseUp	OnMouseUp is called when the user has released the mouse button.
		//OnMouseUpAsButton	OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed.
		#endregion MouseInput

		#region Rendering
		//OnPostRender	OnPostRender is called after a camera finished rendering the scene.
		//OnPreCull	OnPreCull is called before a camera culls the scene.
		//OnPreRender	OnPreRender is called before a camera starts rendering the scene.
		//OnRenderImage	OnRenderImage is called after all rendering is complete to render image.
		//OnRenderObject	OnRenderObject is called after camera has rendered the scene.
		//OnWillRenderObject	OnWillRenderObject is called once for each camera if the object is visible.
		#endregion Rendering

	}

}