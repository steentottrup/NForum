using NForum.Core.Abstractions.Events;
using System;

namespace NForum.Tests.Common {

	public class FakeEventPublisher : IEventPublisher {
		public void Publish<TPayload>(TPayload payload) where TPayload : class {
		}
	}
}
