using System;

namespace NForum.Core.Abstractions.Events {

	/// <summary>
	/// Interface for event publisher class.
	/// </summary>
	public interface IEventPublisher {
		/// <summary>
		/// Method used when publishing an event.
		/// </summary>
		/// <typeparam name="TPayload">The type of the payload.</typeparam>
		/// <param name="payload">The data detailing the event being published.</param>
		void Publish<TPayload>(TPayload payload) where TPayload : class;
	}
}
