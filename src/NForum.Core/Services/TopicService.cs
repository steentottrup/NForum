using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;
using System.Collections.Generic;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;

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
			//TopicCreated afterEvent = new TopicCreated {
			//	Subject = output.Subject,
			//	Text = output.Text,
			//	Type = output.
			//	ForumId = output.Id,
			//	Author = this.userProvider.CurrentUser
			//};
			//this.eventPublisher.Publish<TopicCreated>(afterEvent);

			return output;
		}

		public Boolean Delete(String topicId) {
			throw new NotImplementedException();
		}

		public IEnumerable<Topic> FindAll() {
			throw new NotImplementedException();
		}

		public Topic FindById(String topicId) {
			throw new NotImplementedException();
		}

		public Topic Update(String topicId, String subject, String text, TopicType? type = default(TopicType?)) {
			throw new NotImplementedException();
		}
	}
}
