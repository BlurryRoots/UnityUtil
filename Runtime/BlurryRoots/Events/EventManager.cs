using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlurryRoots {
	namespace Events {

		/// <summary>
		/// Manages event handler subscribtions and is responsible for raising, processing and distributing events.
		/// </summary>
		public class EventManager {

			/// <summary>
			/// Subscribes the given callback for being notifed when event is dispatched.
			/// </summary>
			/// <typeparam name="TEventType">Event type to subscribe to.</typeparam>
			/// <param name="callback">Handler to subscribe.</param>
			public void Subscribe<TEventType> (EventCallback<TEventType> callback) {
				var t = typeof (TEventType);

				// If there has not yet been one single subscriber to this type of event
				if (!this.dispatchers.ContainsKey (t)) {
					// Create a new dispatcher for it
					this.dispatchers.Add (t, new EventDispatcher<TEventType> ());
				}

				var dispatcher = (EventDispatcher<TEventType>)this.dispatchers[t];

				dispatcher.Subscribe (callback);
			}

			/// <summary>
			/// Stops the given callback from being notifed when event is dispatched.
			/// </summary>
			/// <typeparam name="TEventType">Event type to unsubscribe from.</typeparam>
			/// <param name="callback">Handler to unsubscribe.</param>
			public void Unsubscribe<TEventType> (EventCallback<TEventType> callback) {
				System.Type t = typeof (TEventType);

				if (this.dispatchers.ContainsKey (t)) {
					var dispatcher = (EventDispatcher<TEventType>)this.dispatchers[t];

					dispatcher.Unsubscribe (callback);
				}
				else {
					throw new System.Exception ("Could not remove " + callback + " for event type " + t + ". None registered previously!");
				}
			}

			/// <summary>
			/// Raises a new event. It is stored until <see cref="DispatchRaisedEvents"/> is called.
			/// </summary>
			/// <typeparam name="TEventType">Type of event to raise.</typeparam>
			/// <param name="e">New event.</param>
			public void Raise<TEventType> (TEventType e) {
				var t = typeof (TEventType);

				// If there has not yet been one single subscriber to this type of event
				if (!this.dispatchers.ContainsKey (t)) {
					// Exit without doing anything
					return;
				}

				var dispatcher = (EventDispatcher<TEventType>)this.dispatchers[t];
				dispatcher.Raise (e);
			}

			/// <summary>
			/// Dispatches all previously raised events.
			/// </summary>
			public void DispatchRaisedEvents () {
				// Go through all dispatchers
				foreach (var item in this.dispatchers) {
					// And let each dispatched its queued events to its subscribers
					item.Value.DispatchRaisedEvents ();
				}
			}

			/// <summary>
			/// Creates a new EventManager.
			/// </summary>
			public EventManager () {
				this.dispatchers = new Dictionary<System.Type, IEventDispatcher> ();
			}

			/// <summary>
			/// Holds all dispatchers.
			/// </summary>
			private Dictionary<System.Type, IEventDispatcher> dispatchers;

		}

		/// <summary>
		/// Delegate used to store events.
		/// </summary>
		/// <typeparam name="TEventType">Event type.</typeparam>
		/// <param name="e">Occuring event.</param>
		public delegate void EventCallback<TEventType> (TEventType e);

		/// <summary>
		/// Used to describe an event dispatcher.
		/// </summary>
		public interface IEventDispatcher {

			void DispatchRaisedEvents ();

		}

		/// <summary>
		/// Helper class used for dispatching a specific type of event.
		/// </summary>
		/// <typeparam name="TEventType">Event type to dispatch.</typeparam>
		class EventDispatcher<TEventType> : IEventDispatcher {

			/// <summary>
			/// Subscribes given callback to handle any event of type TEventType.
			/// </summary>
			/// <exception cref="System.Exception">Is thrown if callback has already been a subscriber.</exception>
			/// <param name="callback">New subscriber.</param>
			public void Subscribe (EventCallback<TEventType> callback) {
				if (this.subscribers.Contains (callback)) {
					throw new System.Exception ("Could not add " + callback + "! Has already been subscribed before!");
				}

				this.subscribers.Add (callback);
			}

			/// <summary>
			/// Unsubscribes given handler from this dispatcher.
			/// </summary>
			/// <exception cref="System.Exception">Is thrown if callback has not been a subscriber.</exception>
			/// <param name="callback">Subscriber to remove.</param>
			public void Unsubscribe (EventCallback<TEventType> callback) {
				// If this handler has previously subscribed to this dipatcher
				if (this.subscribers.Contains (callback)) {
					// Remove it
					this.subscribers.Remove (callback);
				}
				else {
					throw new System.Exception ("Could not remove " + callback + "! Has not been subcribed before!");
				}
			}

			/// <summary>
			/// Raises a new event. It is stored until <see cref="DispatchRaisedEvents"/> is called.
			/// </summary>
			/// <param name="e">New event.</param>
			public void Raise (TEventType e) {
				this.eventQueue.Enqueue (e);
			}

			/// <summary>
			/// Dispatches event to all subscribers.
			/// </summary>
			public void DispatchRaisedEvents () {
				// Makes a copy because it might be possible for events to raise new events.
				var queue = new Queue<TEventType> (this.eventQueue);
				// Clear all events in the actual queue
				this.eventQueue.Clear ();

				// While there are still events in the queue
				while (0 < queue.Count) {
					// Remove from queue
					var e = queue.Dequeue ();
					// And dispatch to subscribers
					this.Dispatch (e);
				}
			}

			/// <summary>
			/// Creates a new EventDispatcher.
			/// </summary>
			public EventDispatcher () {
				this.subscribers = new List<EventCallback<TEventType>> ();
				this.eventQueue = new Queue<TEventType> ();
			}

			/// <summary>
			/// Dispatches given event to all subscribed handlers.
			/// </summary>
			/// <param name="e">Event to process.</param>
			private void Dispatch (TEventType e) {
				// Make a copy of all handlers. It might be possible that handlers subscribe
				// or unsubscribe while being processed.
				var handlerList = new List<EventCallback<TEventType>> (this.subscribers);

				// Let all handlers process the event.
				while (0 < handlerList.Count) {
					// Fetch handler
					var handler = handlerList[0];
					// Remove him from processing list
					handlerList.RemoveAt (0);

					// Invoke handler.
					handler.Invoke (e);
				}
			}

			/// <summary>
			/// Holds all subscribers.
			/// </summary>
			private IList<EventCallback<TEventType>> subscribers;
			/// <summary>
			/// Event queue.
			/// </summary>
			private Queue<TEventType> eventQueue;

		}

	}
}