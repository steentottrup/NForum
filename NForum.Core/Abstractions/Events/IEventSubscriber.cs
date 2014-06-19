using System;

namespace NForum.Core.Abstractions.Events {

	/// <summary>
	/// Interface for event subscriber classes.
	/// </summary>
	public interface IEventSubscriber {
		/// <summary>
		/// Method for handling the event here and now.
		/// </summary>
		/// <param name="payload"></param>
		/// <param name="request">The current request context.</param>
		void Handle(Object payload, IRequest request);
		///// <summary>
		///// Can this listener only have one event of the given type at a given moment in time?
		///// </summary>
		//Boolean UniqueEvent { get; }
		/// <summary>
		/// Priority, in ascending order.
		/// </summary>
		Byte Priority { get; }
	}

	/// <summary>
	/// A generic interface for event subscriber classes.
	/// </summary>
	/// <typeparam name="TPayload"></typeparam>
	public interface IEventSubscriber<in TPayload> : IEventSubscriber where TPayload : class {
		/// <summary>
		/// The generic handle method.
		/// </summary>
		/// <param name="payload"></param>
		/// <param name="request">The current request context.</param>
		void Handle(TPayload payload, IRequest request);
	}

	/// <summary>
	/// A catch-all interface for event subscriber classes.
	/// </summary>
	public interface ICatchAllEventSubscriber : IEventSubscriber { }
}