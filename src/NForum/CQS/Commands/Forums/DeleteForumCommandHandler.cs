using CreativeMinds.CQS.Commands;
using NForum.Datastores;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Forums {

	public class DeleteForumCommandHandler : CommandWithStatusHandler<DeleteForumCommand> {
		protected readonly IForumDatastore forums;

		public DeleteForumCommandHandler(IForumDatastore forums, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.forums = forums;
		}

		public override void Execute(DeleteForumCommand command) {
			// Permissions have been checked and parameters validated!
			if (!command.DeleteChildren) {
				this.forums.DeleteById(command.Id);
			}
			else {
				this.forums.DeleteWithSubElementsById(command.Id);
			}

			this.SetTaskStatus(command.TaskId, command.Id, "Forum");
		}
	}
}
