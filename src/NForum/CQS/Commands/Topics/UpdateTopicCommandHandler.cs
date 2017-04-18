using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.Commands.Topics {

	public class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand> {
		protected readonly ITopicDatastore topics;
		protected readonly IPrincipal user;
		protected readonly IUserProvider userProvider;

		public UpdateTopicCommandHandler(ITopicDatastore topics, IPrincipal user, IUserProvider userProvider) {
			this.topics = topics;
			this.user = user;
			this.userProvider = userProvider;
		}

		public void Execute(UpdateTopicCommand command) {
			// Permissions have been checked and parameters validated!
			ITopicDto dto = this.topics.ReadById(command.Id);
			if (dto == null) {
				throw new TopicNotFoundException(command.Id);
			}

			Topic t = new Topic(dto);
			t.SetEditor(this.userProvider.GetAuthor(this.user));
			t.SetSubject(command.Subject);
			t.SetContent(command.Content);
			t.ChangeType(command.Type);
			// TODO:
			//t.ClearAndAddProperties(command.Properties);

			this.topics.Update(t);
		}
	}
}
