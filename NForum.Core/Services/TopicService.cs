using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events.Payloads;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class TopicService : ITopicService {
		private readonly IUserProvider userProvider;
		private readonly ITopicRepository topicRepo;
		private readonly IBoardRepository boardRepo;
		private readonly IForumRepository forumRepo;
		private readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;

		public TopicService(IUserProvider userProvider,
							ITopicRepository topicRepo,
							IForumRepository forumRepo,
							IBoardRepository boardRepo,
							ILogger logger,
							IEventPublisher eventPublisher) {

			this.userProvider = userProvider;
			this.topicRepo = topicRepo;
			this.forumRepo = forumRepo;
			this.boardRepo = boardRepo;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
		}

		public Topic Create(Forum forum, String subject, String message) {
			// Let's get the forum from the data-storage!
			Forum oldForum = this.forumRepo.Read(forum.Id);
			return this.Create(oldForum, subject, message, TopicType.Regular);
		}

		public Topic Create(Forum forum, String subject, String message, TopicType type) {
			if (!this.HasAccess(forum.Id, AccessFlag.Create)) {
				throw new PermissionException("create access");
			}

			Topic t = new Topic {
				Author = this.userProvider.CurrentUser,
				AuthorId = this.userProvider.CurrentUser.Id,
				Changed = DateTime.UtcNow,
				Created = DateTime.UtcNow,
				Editor = this.userProvider.CurrentUser,
				EditorId = this.userProvider.CurrentUser.Id,
				Forum = forum,
				ForumId = forum.Id,
				Message = message,
				State = TopicState.None,
				Subject = subject,
				Type = type
			};

			return this.topicRepo.Create(t);
		}

		public Topic Read(Int32 id) {
			return this.topicRepo.Read(id);
		}

		public Topic Read(String subject) {
			return this.topicRepo.BySubject(subject);
		}

		public IEnumerable<Topic> Read(Forum forum, Int32 pageIndex) {
			// We need to know how many topics to show per page, let's get the board!
			Board board = this.boardRepo.ByForum(forum);
			// Let the repo get the topics, and return them!
			return this.topicRepo.Read(forum, board.TopicsPerPage, pageIndex);
		}

		public IEnumerable<Topic> Read(Forum forum, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted) {
			// We need to know how many topics to show per page, let's get the board!
			Board board = this.boardRepo.ByForum(forum);
			// Let the repo get the topics, and return them!
			return this.topicRepo.Read(forum, board.TopicsPerPage, pageIndex, includeQuarantined, includeDeleted);
		}

		public Topic Update(Topic topic) {
			return this.Update(topic, false);
		}

		private Topic Update(Topic topic, Boolean updateStateAndType) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			// Let's get the topic from the data-storage!
			Topic oldTopic = this.Read(topic.Id);
			Topic originalTopic = oldTopic.Clone() as Topic;

			if (oldTopic != null) {
				// Author with "update" access or moderator?
				if (!(this.HasAccess(oldTopic.ForumId, AccessFlag.Update) && oldTopic.AuthorId == this.userProvider.CurrentUser.Id) &&
					!this.IsModerator(oldTopic.ForumId)) {

					throw new PermissionException("Not author and not moderator");
				}

				Boolean changed = false;
				if (updateStateAndType && oldTopic.Type != topic.Type) {
					// Does the user has permissions to change type?
					if (!this.HasAccess(oldTopic.ForumId, AccessFlag.Priority)) {
						throw new PermissionException("priority");
					}
					oldTopic.Type = topic.Type;
					changed = true;
				}
				if (oldTopic.Subject != topic.Subject) {
					oldTopic.Subject = topic.Subject;
					changed = true;
				}
				if (oldTopic.Message != topic.Message) {
					oldTopic.Message = topic.Message;
					changed = true;
				}
				if (updateStateAndType && oldTopic.State != topic.State) {
					// TODO: Permissions?!??!
					oldTopic.State = topic.State;
					changed = true;
				}
				if (oldTopic.CustomProperties != topic.CustomProperties) {
					oldTopic.CustomProperties = topic.CustomProperties;
					changed = true;
				}

				if (changed) {
					oldTopic.Editor = this.userProvider.CurrentUser;
					oldTopic.EditorId = this.userProvider.CurrentUser.Id;
					oldTopic.Changed = DateTime.UtcNow;
					oldTopic = this.topicRepo.Update(oldTopic);
					this.logger.WriteFormat("Topic updated in TopicService, Id: {0}", oldTopic.Id);
					this.eventPublisher.Publish<TopicUpdated>(new TopicUpdated {
						Topic = originalTopic,
						UpdatedTopic = oldTopic
					});
					this.logger.WriteFormat("Update events in TopicService fired, Id: {0}", oldTopic.Id);
				}
				return oldTopic;
			}
			this.logger.WriteFormat("Update topic failed, no topic with the given id was found, Id: {0}", topic.Id);
			// TODO:
			throw new ApplicationException();
		}

		public void Delete(Topic topic) {
			this.topicRepo.Delete(topic);
		}

		private Boolean HasAccess(Int32 forumId, AccessFlag flag) {
			Forum forum = this.forumRepo.Read(forumId);
			// TODO:
			return true;
		}

		private Boolean IsModerator(Int32 forumId) {
			return this.HasAccess(forumId, AccessFlag.Moderator);
		}
	}
}