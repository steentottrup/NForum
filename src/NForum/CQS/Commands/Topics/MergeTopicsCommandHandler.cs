using CreativeMinds.CQS.Commands;
using NForum.Core.Dtos;
using NForum.CQS.Commands.Replies;
using NForum.Datastores;
using NForum.Domain;
using NForum.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.CQS.Commands.Topics {

	public class MergeTopicsCommandHandler : CommandWithStatusHandler<MergeTopicsCommand> {
		private readonly ITopicDatastore topics;
		private readonly IReplyDatastore replies;
		private readonly ICommandDispatcher commandDispatcher;

		public MergeTopicsCommandHandler(ITopicDatastore topics, IReplyDatastore replies, ICommandDispatcher dispatcher, ITaskDatastore taskDatastore) : base(taskDatastore) {
			this.topics = topics;
			this.replies = replies;
			this.commandDispatcher = dispatcher;
		}

		public override void Execute(MergeTopicsCommand command) {
			// Nothing special to do here, permissions have been checked and parameters validated!
			ITopicDto rootTopic = this.topics.ReadById(command.DestinationTopicId);
			if (rootTopic == null) {
				// TODO:
				throw new ArgumentNullException("DestionationTopicId");
			}

			List<ITopicDto> sources = new List<ITopicDto>();
			command.SourceTopicIds.Distinct().ToList().ForEach((t) => {
				ITopicDto topic = this.topics.ReadById(t);
				if (topic == null) {
					// TODO:
					throw new ArgumentException($"Could not locate a topic with id {t}");
				}
				sources.Add(topic);
			});

			if (command.CreateReplyForTopics) {
				sources.ForEach((s) => {
					this.commandDispatcher.Dispatch<CreateReplyCommand>(new CreateReplyCommand {
						Content = s.Content,
						// TODO:
						State = ReplyState.None,
						Subject = s.Subject,
						TopicId = rootTopic.Id
					});
				});
			}

			this.replies.MergeTopics(rootTopic.Id, sources.Select(s => s.Id));

			command.SourceTopicIds.ToList().ForEach((topicId) => {
				this.SetTaskStatus(command.TaskId, topicId, "Topic");
			});

			sources.ForEach((s) => {
				this.commandDispatcher.Dispatch<DeleteTopicCommand>(new DeleteTopicCommand {
					Id = s.Id
				});
			});
		}
	}
}
