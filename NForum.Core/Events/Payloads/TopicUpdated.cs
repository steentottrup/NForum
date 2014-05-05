using System;

namespace NForum.Core.Events.Payloads {

	public class TopicUpdated {
		public Topic Topic { get; set; }
		public Topic UpdatedTopic { get; set; }
	}
}