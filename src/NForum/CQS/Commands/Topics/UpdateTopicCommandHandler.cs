using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand> {
		protected readonly ITopicDatastore topics;

		public UpdateTopicCommandHandler(ITopicDatastore topics) {
			this.topics = topics;
		}

		public void Execute(UpdateTopicCommand command) {
			// Permissions have been checked and parameters validated!
			//ITopicDto dto = this.topics.ReadById(command.Id);
			//if (dto == null) {
			//	// TODO:
			//	throw new ArgumentException("Forum does not exist");
			//}

			//Topic t = new Topic(dto);
			//t.Subject = command.Subject;

			//f.ClearAndAddProperties(f.Properties);

			//this.forums.Update(f);

			throw new NotImplementedException();
		}
	}
}
