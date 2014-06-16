using System;

namespace NForum.Core.Events.Payloads {

	public class TopicDeleted {
		public Topic Topic { get; set; }
	}
}