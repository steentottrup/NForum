using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class UIService : IUIService {
		protected readonly IDataStore dataStore;
		protected readonly IPermissionService permissionService;
		protected readonly ILoggingService loggingService;
		protected readonly IUserProvider userProvider;
		protected readonly ISettings settings;

		public UIService(IDataStore dataStore, IPermissionService permissionService, ILoggingService loggingService, IUserProvider userProvider, ISettings settings) {
			this.dataStore = dataStore;
			this.permissionService = permissionService;
			this.loggingService = loggingService;
			this.userProvider = userProvider;
			this.settings = settings;
		}

		public IEnumerable<Category> FindCategoriesPlus2Levels() {
			// TODO: Permissions!!
			return this.dataStore.FindCategoriesPlus2Levels();
		}

		public Category FindCategoryPlus2Levels(String categoryId) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			// TODO: Permissions!!
			return this.dataStore.FindCategoryPlus2Levels(categoryId);
		}

		public Forum FindForumPlus2Levels(String forumId) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			// TODO: Permissions!!
			return this.dataStore.FindForumPlus2Levels(forumId);
		}

		public IEnumerable<Topic> FindByForum(String forumId, Int32 pageIndex, Int32 pageSize, Boolean includeExtra = false) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			if (pageIndex < 0) {
				pageIndex = 0;
			}
			if (pageSize <= 0) {
				pageSize = this.settings.TopicsPerPage;
			}
			// TODO: Permissions!!
			return this.dataStore.FindByForum(forumId, pageIndex, pageSize, includeExtra);
		}

		public Int32 GetNumberOfForumPages(String forumId, Int32 pageSize) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			if (pageSize <= 0) {
				pageSize = this.settings.TopicsPerPage;
			}
			// TODO: Permissions!!
			return this.dataStore.GetNumberOfForumPages(forumId, pageSize);
		}

		public Int32 GetNumberOfTopicPages(String topicId, Int32 pageSize, Boolean includeDeleted = false) {
			if (String.IsNullOrWhiteSpace(topicId)) {
				throw new ArgumentNullException(nameof(topicId));
			}
			if (pageSize <= 0) {
				pageSize = this.settings.RepliesPerPage;
			}
			// TODO: Permissions!!
			return this.dataStore.GetNumberOfTopicPages(topicId, pageSize, includeDeleted);
		}

		public IEnumerable<Reply> FindByTopic(String topicId, Int32 pageIndex, Int32 pageSize, Boolean includeDeleted = false) {
			if (String.IsNullOrWhiteSpace(topicId)) {
				throw new ArgumentNullException(nameof(topicId));
			}
			if (pageIndex < 0) {
				pageIndex = 0;
			}
			if (pageSize <= 0) {
				pageSize = this.settings.RepliesPerPage;
			}
			// TODO: Permissions!!
			return this.dataStore.FindByTopic(topicId, pageIndex, pageSize, includeDeleted);
		}
	}
}
