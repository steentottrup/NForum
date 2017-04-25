using System;

namespace NForum.CQS.Commands.Topics {

	public class SplitTopicCommand : CommandWithStatus {
		/// <summary>
		/// The id of the topic that will be split into 2.
		/// </summary>
		public String TopicId { get; set; }
		/// <summary>
		/// The id of the reply that will be the new topic.
		/// </summary>
		public String TopicReplyId { get; set; }
		/// <summary>
		/// The ids of the replies that will moved to the new topic.
		/// </summary>
		public String[] NewTopicReplyIds { get; set; }
	}
}
