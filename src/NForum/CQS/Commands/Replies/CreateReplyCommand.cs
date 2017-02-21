using CreativeMinds.CQS.Commands;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Replies {

	public class CreateReplyCommand : ICommand {
		public String TopicId { get; set; }
		public String ParentReplyId { get; set; }
		public String Subject { get; set; }
		public String Content { get; set; }
		public ReplyState State { get; set; }
	}
}
