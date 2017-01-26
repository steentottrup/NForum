using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;

namespace NForum.CQS.Commands.Topics {

	public class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommand> {
		protected readonly IForumDatastore forums;
		protected readonly ITopicDatastore topics;

		public CreateTopicCommandHandler(IForumDatastore forums, ITopicDatastore topics) {
			this.forums = forums;
			this.topics = topics;
		}

		public void Execute(CreateTopicCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			IForumDto forum = this.forums.ReadById(command.ForumId);
			if (forum == null) {
				// TODO:
				throw new ArgumentException("Parent forum not found!");
			}

			Topic t = new Topic(new Forum(forum), command.Subject, command.Content, command.Type, command.State);
			this.topics.Create(t);
		}
	}
}
