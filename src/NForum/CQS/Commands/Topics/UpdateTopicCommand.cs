using CreativeMinds.CQS.Commands;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class UpdateTopicCommand : CommandWithStatus {
		public String Id { get; set; }
		public String Subject { get; set; }
		public String Content { get; set; }
		public TopicType Type { get; set; }
	}
}
