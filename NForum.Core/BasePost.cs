using System;
using System.Xml.Linq;

namespace NForum.Core {

	public abstract class BasePost {
		public Int32 ForumId { get; set; }
		public Int32 TopicId { get; set; }
		public Int32 AuthorId { get; set; }
		public Int32 EditorId { get; set; }

		public Int32? ParentPostId { get; set; }

		public DateTime Created { get; set; }
		public DateTime Changed { get; set; }

		public PostState State { get; set; }

		public String Subject { get; set; }
		public String Message { get; set; }

		public String CustomProperties { get; set; }
		public XDocument CustomData { get; set; }
	}
}