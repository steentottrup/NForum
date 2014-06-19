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
		protected readonly IUserProvider userProvider;
		protected readonly ITopicRepository topicRepo;
		protected readonly IForumRepository forumRepo;
		protected readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;
		protected readonly IForumConfigurationService confService;
		protected readonly IPermissionService permService;

		public TopicService(IUserProvider userProvider,
							IForumRepository forumRepo,
							ITopicRepository topicRepo,
							IEventPublisher eventPublisher,
							ILogger logger,
							IPermissionService permService,
							IForumConfigurationService confService) {

			this.userProvider = userProvider;
			this.topicRepo = topicRepo;
			this.forumRepo = forumRepo;
			this.confService = confService;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
			this.permService = permService;
		}

		/// <summary>
		/// Method for creating a new topic.
		/// </summary>
		/// <param name="forum">The parent forum of the topic.</param>
		/// <param name="subject">The subject of the topic.</param>
		/// <param name="message">The content/message of the topic.</param>
		/// <returns>The newly created topic.</returns>
		public Topic Create(Forum forum, String subject, String message) {
			return this.Create(forum, subject, message, TopicType.Regular);
		}

		/// <summary>
		/// Method for creating a new topic.
		/// </summary>
		/// <param name="forum">The parent forum of the topic.</param>
		/// <param name="subject">The subject of the topic.</param>
		/// <param name="message">The content/message of the topic.</param>
		/// <param name="type">The type of the topic </param>
		/// <returns>The newly created topic.</returns>
		public Topic Create(Forum forum, String subject, String message, TopicType type) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException("subject");
			}
			if (String.IsNullOrWhiteSpace(message)) {
				throw new ArgumentNullException("message");
			}
			forum = this.forumRepo.Read(forum.Id);
			if (forum == null) {
				throw new ArgumentException("forum does not exist");
			}

			this.logger.WriteFormat("Create called on TopicService, subject: {0}, forum id: {1}", subject, forum.Id);
			AccessFlag flag = this.permService.GetAccessFlag(this.userProvider.CurrentUser, forum);
			if ((flag & AccessFlag.Create) != AccessFlag.Create) {
				this.logger.WriteFormat("User does not have permissions to create a new topic in forum {1}, subject: {0}", subject, forum.Id);
				throw new PermissionException("topic, create");
			}
			if (type != TopicType.Regular && (flag & AccessFlag.Priority) != AccessFlag.Priority) {
				this.logger.WriteFormat("User does not have permissions to set topic type on new topic in forum {1}, subject: {0}", subject, forum.Id);
				throw new PermissionException("topic, type");
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
			// TODO: Custom properties?

			this.topicRepo.Create(t);
			this.logger.WriteFormat("Topic created in TopicService, Id: {0}", t.Id);
			this.eventPublisher.Publish<TopicCreated>(new TopicCreated {
				Topic = t
			});
			this.logger.WriteFormat("Create events in TopicService fired, Id: {0}", t.Id);

			return t;
		}

		/// <summary>
		/// Method for reading a topic by its id.
		/// </summary>
		/// <param name="id">Id of the topic to read.</param>
		/// <returns></returns>
		public Topic Read(Int32 id) {
			this.logger.WriteFormat("Read called on TopicService, Id: {0}", id);
			Topic topic = this.topicRepo.Read(id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, topic.Forum, (Int64)AccessFlag.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the topic, id: {0}", topic.Id);
				throw new PermissionException("topic, read");
			}

			return topic;
		}

		/// <summary>
		/// Method for reading a topic by its subject.
		/// </summary>
		/// <param name="name">The subject of the topic to read.</param>
		/// <returns></returns>
		public Topic Read(String subject) {
			this.logger.WriteFormat("Read called on TopicService, subject: {0}", subject);
			Topic topic = this.topicRepo.BySubject(subject);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, topic.Forum, (Int64)AccessFlag.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the topic, subject: {0}", topic.Subject);
				throw new PermissionException("topic, read");
			}

			return topic;
		}

		/// <summary>
		/// Method for reading a "page" full of topic.
		/// </summary>
		/// <param name="forum">The forum where the topics should be read from.</param>
		/// <param name="pageIndex">The page.</param>
		/// <returns></returns>
		public IEnumerable<Topic> Read(Forum forum, Int32 pageIndex) {
			return this.Read(forum, pageIndex, false, false);
		}

		/// <summary>
		/// Method for reading a "page" full of topic.
		/// </summary>
		/// <param name="forum">The forum where the topics should be read from.</param>
		/// <param name="pageIndex">The page.</param>
		/// <param name="includeQuarantined">True if quarantined topics should be included.</param>
		/// <param name="includeDeleted">True if deleted topics should be included.</param>
		/// <returns></returns>
		public IEnumerable<Topic> Read(Forum forum, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted) {
			this.logger.Write("Read called on TopicService");
			// We need to know how many topics to show per page, let's get the configuration!
			Int32 topicsPerPage = this.confService.Read().TopicsPerPage();
			// Let the repo get the topics, and return them!
			return this.topicRepo.Read(forum, topicsPerPage, pageIndex, includeQuarantined, includeDeleted);
		}

		/// <summary>
		/// Method for updating a topic.
		/// </summary>
		/// <param name="topic">The changed topic.</param>
		/// <returns>The updated topic.</returns>
		public Topic Update(Topic topic) {
			return this.Update(topic, false);
		}

		protected Topic Update(Topic topic, Boolean updateType) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			this.logger.WriteFormat("Update called on TopicService, Id: {0}", topic.Id);
			// Let's get the topic from the data-storage!
			Topic oldTopic = this.Read(topic.Id);
			if (oldTopic == null) {
				this.logger.WriteFormat("Update topic failed, no topic with the given id was found, Id: {0}", topic.Id);
				throw new ArgumentException("topic does not exist");
			}
			Topic originalTopic = oldTopic.Clone() as Topic;

			// Author with "update" access or moderator?
			AccessFlag flag = this.permService.GetAccessFlag(this.userProvider.CurrentUser, oldTopic.Forum);
			if ((flag & AccessFlag.Priority) != AccessFlag.Update) {
				this.logger.WriteFormat("User does not have permissions to update a topic, id: {1}, subject: {0}", topic.Subject, topic.Id);
				throw new PermissionException("topic, update");
			}

			Boolean changed = false;
			if (updateType && ((flag & AccessFlag.Priority) != AccessFlag.Priority)) {
				this.logger.WriteFormat("User does not have permissions to change type on a topic, id: {1}, subject: {0}", topic.Subject, topic.Id);
				throw new PermissionException("topic, update");
			}
			else {
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

		/// <summary>
		/// Method for updating state on a topic.
		/// </summary>
		/// <param name="topic">The topic to update.</param>
		/// <param name="newState">The new state of the topic.</param>
		/// <returns></returns>
		public Topic Update(Topic topic, TopicState newState) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			this.logger.WriteFormat("Update called on TopicService, Id: {0}", topic.Id);
			// Let's get the topic from the data-storage!
			Topic oldTopic = this.Read(topic.Id);
			if (oldTopic == null) {
				this.logger.WriteFormat("Update topic failed, no topic with the given id was found, Id: {0}", topic.Id);
				throw new ArgumentException("topic does not exist");
			}

			Topic originalTopic = oldTopic.Clone() as Topic;
			// Has state changed?
			if (oldTopic.State != newState) {
				oldTopic.State = newState;

				oldTopic.Editor = this.userProvider.CurrentUser;
				oldTopic.EditorId = this.userProvider.CurrentUser.Id;
				oldTopic.Changed = DateTime.UtcNow;
				oldTopic = this.topicRepo.Update(oldTopic);
				this.logger.WriteFormat("Topic updated in TopicService, Id: {0}", oldTopic.Id);
				this.eventPublisher.Publish<TopicStateUpdated>(new TopicStateUpdated {
					OriginalTopic = originalTopic,
					UpdatedTopic = oldTopic
				});
				this.logger.WriteFormat("Update events in TopicService fired, Id: {0}", oldTopic.Id);
			}
			return oldTopic;
		}

		/// <summary>
		/// Method for deleting a topic.
		/// </summary>
		/// <param name="topic">The topic to delete.</param>
		public void Delete(Topic topic) {
			if (topic == null) {
				throw new ArgumentNullException("topic");
			}
			this.logger.WriteFormat("Delete called on TopicService, Id: {0}", topic.Id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, topic.Forum, (Int64)AccessFlag.Delete)) {
				this.logger.WriteFormat("User does not have permissions to delete a topic, id: {1}, subject: {0}", topic.Subject, topic.Id);
				throw new PermissionException("topic, delete");
			}
			// TODO: posts, attachments, etc
			this.topicRepo.Delete(topic);
			this.eventPublisher.Publish<TopicDeleted>(new TopicDeleted {
				Topic = topic
			});
			this.logger.WriteFormat("Delete events in TopicService fired, Id: {0}", topic.Id);
		}
	}
}