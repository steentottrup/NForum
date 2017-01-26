using CreativeMinds.CQS.Commands;
using NForum.Datastores;
using System;

namespace NForum.CQS.Commands.Forums {

	public class DeleteForumCommandHandler : ICommandHandler<DeleteForumCommand> {
		protected readonly IForumDatastore forums;

		public DeleteForumCommandHandler(IForumDatastore forums) {
			this.forums = forums;
		}

		public void Execute(DeleteForumCommand command) {
			// Permissions have been checked and parameters validated!
			if (!command.DeleteChildren) {
				this.forums.DeleteById(command.Id);
			}
			else {
				this.forums.DeleteWithSubElementsById(command.Id);
			}
		}
	}
}
