﻿using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;
using System.Security.Principal;

namespace NForum.CQS.Commands.Topics {

	public class CreateTopicCommandHandler : CommandWithStatusHandler<CreateTopicCommand> {
		protected readonly IForumDatastore forums;
		protected readonly ITopicDatastore topics;
		protected readonly IPrincipal principal;

		public CreateTopicCommandHandler(IForumDatastore forums, ITopicDatastore topics, ITaskDatastore taskDatastore, IPrincipal principal) : base(taskDatastore) {
			this.forums = forums;
			this.topics = topics;
			this.principal = principal;
		}

		public override void Execute(CreateTopicCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			IForumDto forum = this.forums.ReadById(command.ForumId);
			if (forum == null) {
				// TODO:
				throw new ArgumentException("Parent forum not found!");
			}

			Topic t = new Topic(new Forum(forum), command.Subject, command.Content, command.Type, command.State);
			ITopicDto newTopic = this.topics.Create(t);

			this.SetTaskStatus(command.TaskId, newTopic.Id, "Topic");
		}
	}
}
