using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Forums {

	public class UpdateForumCommandHandler : CommandWithStatusHandler<UpdateForumCommand> {
		protected readonly IForumDatastore forums;

		public UpdateForumCommandHandler(IForumDatastore forums, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.forums = forums;
		}

		public override void Execute(UpdateForumCommand command) {
			// Permissions have been checked and parameters validated!
			IForumDto dto = this.forums.ReadById(command.Id);
			if (dto == null) {
				throw new ForumNotFoundException(command.Id);
			}

			Forum f = new Forum(dto);
			f.Name = command.Name;
			f.SortOrder = command.SortOrder;
			f.Description = command.Description;
			f.ClearAndAddProperties(f.Properties);

			this.forums.Update(f);

			this.SetTaskStatus(command.TaskId, command.Id, "Forum");
		}
	}
}
