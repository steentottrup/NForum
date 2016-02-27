using NForum.Core.Abstractions.Events;
using System;

namespace NForum.Tests.BaseTests {

	public class FakeEventPublisher : IEventPublisher {
		public void Publish<TPayload>(TPayload payload) where TPayload : class {
		}
	}
}
