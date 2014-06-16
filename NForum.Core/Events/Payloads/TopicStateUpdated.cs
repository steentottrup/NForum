using System;

namespace NForum.Core.Events.Payloads {

	public class TopicStateUpdated {
		public Topic Topic { get; set; }
		public Topic UpdatedTopic { get; set; }
	}
}