using System;

namespace NForum.Core.Events.Payloads {

	public class TopicStateUpdated {
		public Topic OriginalTopic { get; set; }
		public Topic UpdatedTopic { get; set; }
	}
}