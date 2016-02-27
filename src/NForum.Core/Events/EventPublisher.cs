using NForum.Core.Abstractions.Events;
using System;

namespace NForum.Core.Events {

	public class EventPublisher : IEventPublisher {

		public void Publish<TPayload>(TPayload payload) where TPayload : class {
			// TODO:
		}
	}
}
