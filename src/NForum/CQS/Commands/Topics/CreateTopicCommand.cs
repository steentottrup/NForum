using CreativeMinds.CQS.Commands;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class CreateTopicCommand : CommandWithStatus {
		public String ForumId { get; set; }
		public String Subject { get; set; }
		public String Content { get; set; }
		public TopicState State { get; set; }
		public TopicType Type { get; set; }
	}
}
