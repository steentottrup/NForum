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
		/// The current request context.
		/// </summary>
		private readonly IState request;

		/// <summary>
		/// Constructor for the EventPublisher class.
		/// </summary>
		/// <param name="subscribers">The list of event listeners.</param>
		/// <param name="logger"></param>
		/// <param name="request"></param>
		public EventPublisher(IEnumerable<IEventSubscriber> subscribers, ILogger logger, IState request) {
			this.subscribers = subscribers;
			this.logger = logger;
			this.request = request;
		}

		/// <summary>
		/// Publish an event with the given payload.
		/// </summary>
		/// <typeparam name="TPayload">Generic type of the payload/data.</typeparam>
		/// <param name="payload">The data detailing the event being published.</param>
		public void Publish<TPayload>(TPayload payload) where TPayload : class {
			this.logger.WriteFormat("Publishing event, {0}", payload);
			// Get the event subscribers that listen to events with the given payload.
			IEnumerable<IEventSubscriber> handlersForPayload = this.subscribers.OfType<IEventSubscriber<TPayload>>();
			// Get any catch-all handlers!
			handlersForPayload = handlersForPayload.Union(this.subscribers.OfType<ICatchAllEventSubscriber>());
			// Run through the subscribers, in priority order!
			foreach (IEventSubscriber subscriber in handlersForPayload.OrderBy(h => h.Priority)) {
				try {
					this.logger.WriteFormat("Subscriber found, {0}", subscriber);
					// Let the subscriber handle it!!
					subscriber.Handle(payload, this.request);
				}
				catch (Exception ex) {
					this.logger.Write("Event subscriber failed", ex);
				}
			}
		}
	}
}