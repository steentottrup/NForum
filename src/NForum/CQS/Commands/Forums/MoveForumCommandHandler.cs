using CreativeMinds.CQS.Commands;
using NForum.Datastores;
using System;

namespace NForum.CQS.Commands.Forums {

	public class MoveForumCommandHandler : ICommandHandler<MoveForumCommand> {
		protected readonly IForumDatastore forums;

		public MoveForumCommandHandler(IForumDatastore forums) {
			this.forums = forums;
		}

		public void Execute(MoveForumCommand command) {
			// Permissions have been checked and parameters validated!
			if (String.IsNullOrWhiteSpace(command.DestinationForumId)) {
				this.forums.MoveToCategory(command.Id, command.DestinationCategoryId);
			}
			else {
				this.forums.MoveToForum(command.Id, command.DestinationCategoryId);
			}
		}
	}
}
