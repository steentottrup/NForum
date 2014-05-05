using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Core.Events {

	/// <summary>
	/// Class used for publishing event.
	/// </summary>
	public class EventPublisher : IEventPublisher {
		/// <summary>
		/// A list of event listeners.
		/// </summary>
		private readonly IEnumerable<IEventSubscriber> subscribers;
		/// <summary>
		/// A logger service.
		/// </summary>
		private readonly ILogger logger;

		/// <summary>
		/// Constructor for the EventPublisher class.
		/// </summary>
		/// <param name="subscribers">The list of event listeners.</param>
		//public EventPublisher(IEnumerable<IEventSubscriber> subscribers, ILogger logger) {
		//	this.subscribers = subscribers;
		//	this.logger = logger;
		//}
		public EventPublisher(ILogger logger) {
			this.logger = logger;
		}

		/// <summary>
		/// Publish an event with the given payload.
		/// </summary>
		/// <typeparam name="TPayload">Generic type of the payload/data.</typeparam>
		/// <param name="payload">The data detailing the event being published.</param>
		public void Publish<TPayload>(TPayload payload) where TPayload : class {
			this.logger.WriteFormat("Publishing event, {0}", payload);
			// Get the event subscribers that listen to events with the given payload.
			IEnumerable<IEventSubscriber<TPayload>> handlersForPayload = this.subscribers.OfType<IEventSubscriber<TPayload>>();
			// Run through the subscribers, in priority order!
			foreach (IEventSubscriber<TPayload> subscriber in handlersForPayload.OrderBy(h => h.Priority)) {
				try {
					this.logger.WriteFormat("Subscriber found, {0}", subscriber);
					// Let the subscriber handle it!!
					subscriber.Handle(payload);
				}
				catch (Exception ex) {
					this.logger.Write("Event subscriber failed", ex);
				}
			}
		}
	}
}