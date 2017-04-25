using System;

namespace NForum.CQS.Commands.Forums {

	public class UpdateForumCommand : CommandWithStatus {
		public String Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
