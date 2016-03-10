using System;

namespace NForum.Database.EntityFramework.Dbos {

	public class Reply {
		public Guid Id { get; set; }
		public NForum.Core.ReplyState State { get; set; }
		public String CustomData { get; set; }

		public Int32 Indent { get; set; }

		public Guid? ParentReplyId { get; set; }
		public virtual Reply ParentReply { get; set; }

		public Guid MessageId { get; set; }
		public virtual Message Message { get; set; }

		public Guid TopicId { get; set; }
		public virtual Topic Topic { get; set; }
	}
}
