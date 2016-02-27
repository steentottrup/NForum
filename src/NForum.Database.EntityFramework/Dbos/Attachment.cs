using System;

namespace NForum.Database.EntityFramework.Dbos {
	public class Attachment {
		public Guid Id { get; set; }



		public Guid MessageId { get; set; }
		public virtual Message Message { get; set; }
	}
}