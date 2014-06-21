using System;

namespace NForum.Core.Events.Payloads {

	public class TopicStateUpdated {

		public TopicStateUpdated(Topic original) {
			this.OriginalTopic = original;
		}

		public Topic OriginalTopic { get; private set; }
		public Topic UpdatedTopic { get; set; }
	}
}