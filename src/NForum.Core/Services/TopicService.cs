using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;
using System.Collections.Generic;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;

namespace NForum.Core.Services {

	public class TopicService : ITopicService {
		protected readonly IDataStore dataStore;
		protected readonly IPermissionService permissionService;
		protected readonly ILoggingService loggingService;
		protected readonly IUserProvider userProvider;
		protected readonly IEventPublisher eventPublisher;

		public TopicService(IDataStore dataStore, IPermissionService permissionService, ILoggingService loggingService, IUserProvider userProvider, IEventPublisher eventPublisher) {
			this.dataStore = dataStore;
			this.permissionService = permissionService;
			this.loggingService = loggingService;
			this.userProvider = userProvider;
			this.eventPublisher = eventPublisher;
		}

		public Topic Create(String forumId, String subject, String text, TopicType? type = default(TopicType?)) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException(nameof(subject));
			}
			if (String.IsNullOrWhiteSpace(text)) {
				throw new ArgumentNullException(nameof(text));
			}

			if (!type.HasValue) {
				type = TopicType.Regular;
			}

			this.loggingService.Application.DebugWriteFormat("Create called on TopicService, ForumId: {0}, Subject: {1}, Text: {2}, Type: {3}", forumId, subject, text, type);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /* TODO !currentUser.Can(this.permissionService)*/) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to create a new topic, ForumId: {0}", forumId);
				throw new PermissionException("create topic", currentUser);
			}

			if (type.Value != TopicType.Regular) {
				// TODO: Permissions!
			}

			Topic output = this.dataStore.CreateTopic(currentUser.Id, forumId, subject, text, type.Value);
			this.loggingService.Application.DebugWriteFormat("Topic created in TopicService, Id: {0}", output.Id);

			// TODO:
			TopicCreated afterEvent = new TopicCreated {
				//Subject = output.Subject,
				//Text = output.Text,
				//Type = output.
				//ForumId = output.Id,
				//Author = this.userProvider.CurrentUser
			};
			this.eventPublisher.Publish<TopicCreated>(afterEvent);

			return output;
		}

		public Boolean Delete(String topicId) {
			Topic topic = this.FindById(topicId);
			if (topic == null) {
				// TODO:
				throw new ApplicationException("todo");
			}

			this.loggingService.Application.DebugWriteFormat("Delete called on TopicService, topicId: {0}", topicId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /* TODO !currentUser.Can(this.permissionService)*/) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to delete the topic, topicId: {0}", topicId);
				throw new PermissionException("delete topic", currentUser);
			}

			topic = this.Update(currentUser.Id, topicId, topic.Subject, topic.Text, topic.Type, TopicState.Deleted);

			// TODO:
			TopicDeleted afterEvent = new TopicDeleted {
				//Subject = output.Subject,
				//Text = output.Text,
				//Type = output.
				//ForumId = output.Id,
				//Author = this.userProvider.CurrentUser
			};
			this.eventPublisher.Publish<TopicDeleted>(afterEvent);

			return topic != null;
		}

		private Topic Update(String authorId, String topicId, String subject, String text, TopicType type, TopicState state) {
			return this.dataStore.UpdateTopic(authorId, topicId, subject, text, type, state);
		}

		public IEnumerable<Topic> FindAll() {
			// TODO: Do we want to implement this at all ???
			throw new NotImplementedException();
		}

		public Topic FindById(String topicId) {
			if (String.IsNullOrWhiteSpace(topicId)) {
				throw new ArgumentNullException(nameof(topicId));
			}

			this.loggingService.Application.DebugWriteFormat("FindById called on TopicService, Id: {0}", topicId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			Topic topic = this.dataStore.FindTopicById(topicId);
			// TODO: Permissions!!
			// TODO: Deleted ?? Read on forum etc

			return topic;
		}

		/// <summary>
		/// Method for updating an existing <see cref="Topic"/>.
		/// </summary>
		/// <param name="topicId">The Id of the topic.</param>
		/// <param name="subject">The updated subject.</param>
		/// <param name="text">The updated text.</param>
		/// <param name="type">The updated type.</param>
		/// <param name="state">The updated state.</param>
		/// <returns>The updated topic.</returns>
		/// <exception cref="ArgumentNullException">If the topicId or subject arguments are null/empty.</exception>
		/// <exception cref="PermissionException">If the current user does not have access.</exception>
		public Topic Update(String topicId, String subject, String text, TopicType? type = default(TopicType?), TopicState? state = default(TopicState?)) {
			if (String.IsNullOrWhiteSpace(topicId)) {
				throw new ArgumentNullException(nameof(topicId));
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException(nameof(subject));
			}

			this.loggingService.Application.DebugWriteFormat("Update called on TopicService, topicId: {0}", topicId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /* TODO !currentUser.Can(this.permissionService)*/) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to update the topic, topicId: {0}", topicId);
				throw new PermissionException("update topic", currentUser);
			}

			Topic topic = this.FindById(topicId);

			topic = this.Update(currentUser.Id, topicId, subject, text, type.HasValue ? type.Value : topic.Type, topic.State);

			// TODO:
			TopicUpdated afterEvent = new TopicUpdated {
				//Subject = output.Subject,
				//Text = output.Text,
				//Type = output.
				//ForumId = output.Id,
				//Author = this.userProvider.CurrentUser
			};
			this.eventPublisher.Publish<TopicUpdated>(afterEvent);

			return topic;
		}
	}
}
