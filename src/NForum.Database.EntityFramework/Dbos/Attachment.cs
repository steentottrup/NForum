using System;

namespace NForum.Database.EntityFramework.Dbos {
	public class Attachment {
		public Guid Id { get; set; }
		public String OriginalFilename { get; set; }
		public String Url { get; set; }
		public Int64 Size { get; set; }
		public String FileType { get; set; }
		public Int32? DownloadCount { get; set; }
		public DateTime Created { get; set; }

		public Guid MessageId { get; set; }
		public virtual Message Message { get; set; }
	}
}