using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Forums {

	public class MoveForumCommand : ICommand {
		public String Id { get; set; }
		public String DestinationCategoryId { get; set; }
		public String DestinationForumId { get; set; }
	}
}
