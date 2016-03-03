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
		// TODO: Author/Editor
		public Boolean ModeratorChanged { get; set; }
		public String EditReason { get; set; }
		public String DeleteReason { get; set; }

		public virtual ICollection<Attachment> Attachments { get; set; }
	}
}