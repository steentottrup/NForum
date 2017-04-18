using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Forums {

	public class UpdateForumCommandHandler : ICommandHandler<UpdateForumCommand> {
		protected readonly IForumDatastore forums;

		public UpdateForumCommandHandler(IForumDatastore forums) {
			this.forums = forums;
		}

		public void Execute(UpdateForumCommand command) {
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
		}
	}
}
