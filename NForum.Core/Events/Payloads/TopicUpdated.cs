using System;

namespace NForum.Core.Events.Payloads {

	public class TopicUpdated {

		public TopicUpdated(Topic original) {
			this.Topic = original;
		}

		public Topic Topic { get; private set; }
		public Topic UpdatedTopic { get; set; }
	}
}