using System;

namespace NForum.CQS.Commands.Forums {

	public class CreateForumCommand : CommandWithStatus {
		public String CategoryId { get; set; }
		public String ParentForumId { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
