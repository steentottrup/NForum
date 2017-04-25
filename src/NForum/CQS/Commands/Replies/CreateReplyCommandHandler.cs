using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Replies {

	public class CreateReplyCommandHandler : CommandWithStatusHandler<CreateReplyCommand> {
		protected readonly IReplyDatastore replies;
		protected readonly ITopicDatastore topics;

		public CreateReplyCommandHandler(ITopicDatastore topics, IReplyDatastore replies, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.topics = topics;
			this.replies = replies;
		}

		public void Execute(CreateReplyCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			ITopicDto topic = null;
			IReplyDto reply = null;
			if (!String.IsNullOrWhiteSpace(command.ParentReplyId)) {
				reply = this.replies.ReadById(command.ParentReplyId);
				if (reply == null) {
					// TODO:
					throw new ArgumentException("Parent reply not found!");
				}
				topic = this.topics.ReadById(reply.Topic.Id);
			}
			else {
				topic = this.topics.ReadById(command.TopicId);
			}
			if (topic == null) {
				// TODO:
				throw new ArgumentException("Parent topic not found!");
			}

			Reply r = null;
			if (reply != null) {
				r = new Reply(new Reply(reply), command.Subject, command.Content, command.State);
			}
			else {
				r = new Reply(new Topic(topic), command.Subject, command.Content, command.State);
			}
			this.replies.Create(r);
		}
	}
}
