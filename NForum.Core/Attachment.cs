using System;

namespace NForum.Core {

	public class Attachment {
		public Int32 Id { get; set; }
		public String Filename { get; set; }
		public Int32 PostId { get; set; }
		public Int32 Size { get; set; }
		//public Int32 DownloadCount { get; set; }
		public String Path { get; set; }
		public Int32 AuthorId { get; set; }
		private DateTime Created { get; set; }

		public virtual User Author { get; set; }
		public virtual Post Post { get; set; }
	}
}