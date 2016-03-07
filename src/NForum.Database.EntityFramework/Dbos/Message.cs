using System;
using System.Collections.Generic;

namespace NForum.Database.EntityFramework.Dbos {

	public class Message {
		public Guid Id { get; set; }
		public MessageState State { get; set; }
		public String Subject { get; set; }
		public String Text { get; set; }

		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Guid AuthorId { get; set; }
		public virtual ForumUser Author { get; set; }
		public Guid EditorId { get; set; }
		public virtual ForumUser Editor { get; set; }

		public String EditReason { get; set; }
		public String DeleteReason { get; set; }

		public virtual ICollection<Attachment> Attachments { get; set; }
	}
}