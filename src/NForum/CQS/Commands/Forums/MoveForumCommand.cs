using System;

namespace NForum.CQS.Commands.Forums {

	public class MoveForumCommand : CommandWithStatus {
		public String Id { get; set; }
		public String DestinationCategoryId { get; set; }
		public String DestinationForumId { get; set; }
	}
}
