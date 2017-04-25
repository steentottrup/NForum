using NForum.Datastores;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Forums {

	public class MoveForumCommandHandler : CommandWithStatusHandler<MoveForumCommand> {
		protected readonly IForumDatastore forums;

		public MoveForumCommandHandler(IForumDatastore forums, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.forums = forums;
		}

		public override void Execute(MoveForumCommand command) {
			// Permissions have been checked and parameters validated!
			if (String.IsNullOrWhiteSpace(command.DestinationForumId)) {
				this.forums.MoveToCategory(command.Id, command.DestinationCategoryId);
			}
			else {
				this.forums.MoveToForum(command.Id, command.DestinationCategoryId);
			}

			this.SetTaskStatus(command.TaskId, command.Id, "Forum");
		}
	}
}
