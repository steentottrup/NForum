using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;
using System.Collections.Generic;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;

namespace NForum.Core.Services {

	public class ForumService : IForumService {
		protected readonly IDataStore dataStore;
		protected readonly IPermissionService permissionService;
		protected readonly ILoggingService loggingService;
		protected readonly IUserProvider userProvider;
		protected readonly IEventPublisher eventPublisher;

		public ForumService(IDataStore dataStore, IPermissionService permissionService, Abstractions.Services.ILoggingService loggingService, IUserProvider userProvider, IEventPublisher eventPublisher) {
			this.dataStore = dataStore;
			this.permissionService = permissionService;
			this.loggingService = loggingService;
			this.userProvider = userProvider;
			this.eventPublisher = eventPublisher;

			// TODO: Log all contructor param types!
		}

		public Forum Create(IAuthenticatedUser currentUser, String categoryId, String name, Int32 sortOrder, String description) {
			throw new NotImplementedException();
		}

		public Forum CreateSubForum(IAuthenticatedUser currentUser, String forumId, String name, Int32 sortOrder, String description) {
			throw new NotImplementedException();
		}

		public Boolean Delete(IAuthenticatedUser currentUser, String forumId) {
			throw new NotImplementedException();
		}

		public IEnumerable<Forum> FindAll(IAuthenticatedUser currentUser) {
			throw new NotImplementedException();
		}

		public Forum FindById(IAuthenticatedUser currentUser, String forumId) {
			throw new NotImplementedException();
		}

		public Forum FindForumPlus2Levels(String forumId) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			return this.dataStore.FindForumPlus2Levels(forumId);
		}

		public Forum Update(IAuthenticatedUser currentUser, String forumId, String name, Int32 sortOrder, String description) {
			throw new NotImplementedException();
		}
	}
}
