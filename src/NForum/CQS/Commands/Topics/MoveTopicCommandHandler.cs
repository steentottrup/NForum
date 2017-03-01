using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Infrastructure;
using System;

namespace NForum.CQS.Commands.Topics {

	public class MoveTopicCommandHandler : ICommandHandler<MoveTopicCommand> {
		protected readonly ITopicDatastore topics;
		protected readonly IForumDatastore forums;
		protected readonly ICommandDispatcher commandDispatcher;
		protected readonly ITaskDatastore tasks;

		public MoveTopicCommandHandler(IForumDatastore forums, ITopicDatastore topics, ICommandDispatcher commandDispatcher, ITaskDatastore tasks) {
			this.topics = topics;
			this.forums = forums;
			this.commandDispatcher = commandDispatcher;
			this.tasks = tasks;
		}

		public void Execute(MoveTopicCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			ITopicDto topic = this.topics.ReadById(command.TopicId);
			if (topic == null) {
				// TODO:
				throw new ArgumentNullException("TopicId");
			}
			IForumDto forum = this.forums.ReadById(command.DestinationForumId);
			if (forum == null) {
				// TODO:
				throw new ArgumentNullException("ForumId");
			}
			// Moving to another forum?
			if (topic.Forum.Id != forum.Id) {
				// Create a new topic or move the old one?
				if (command.CreateMovedTopic) {
					CreateTopicCommand ctc = new CreateTopicCommand {
						Content = topic.Content,
						ForumId = forum.Id,
						State = topic.State,
						Subject = topic.Subject,
						Type = topic.Type
					};
					this.commandDispatcher.Dispatch<CreateTopicCommand>(ctc);

					// TODO:
					String newTopicId = this.tasks.GetTaskStatus(ctc.TaskId).Item1;

					this.commandDispatcher.Dispatch<MergeTopicsCommand>(new MergeTopicsCommand {
						CreateReplyForTopics = false,
						SourceTopicIds = new String[] { topic.Id },
						DestinationTopicId = newTopicId
					});

					// TODO: Change Type on original topic !!!!!
				}
				else {
					this.topics.Move(topic.Id, forum.Id);
				}
			}
		}
	}
}
