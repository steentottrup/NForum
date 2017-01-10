using CreativeMinds.CQS.Commands;
using System;

namespace NForum.CQS.Commands.Forums {

	public class DeleteForumCommand : ICommand {
		public String Id { get; set; }
		public Boolean DeleteChildren { get; set; }
	}
}
