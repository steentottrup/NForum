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

		public Forum Create(String categoryId, String name, Int32 sortOrder, String description) {
			return this.CreateForum(categoryId, String.Empty, name, sortOrder, description);
		}

		public Forum CreateSubForum(String forumId, String name, Int32 sortOrder, String description) {
			Forum forum = this.FindById(forumId);
			return this.CreateForum(forum.CategoryId, forumId, name, sortOrder, description);
		}

		private Forum CreateForum(String categoryId, String parentForumId, String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}

			this.loggingService.Application.DebugWriteFormat("Create called on ForumService, CategoryId: {0}, Name: {1}, Description: {2}, Sort Order: {3}", categoryId, name, description, sortOrder);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /*|| !currentUser.CanCreate(this.permissionService) TODO */) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to create a new forum, name: {0}", name);
				throw new PermissionException("create forum", currentUser);
			}

			Forum output = this.dataStore.CreateForum(categoryId, parentForumId, name, sortOrder, description);
			this.loggingService.Application.DebugWriteFormat("Forum created in ForumService, Id: {0}", output.Id);

			//ForumCreated afterEvent = new ForumCreated {
			//	Name = output.Name,
			//	CategoryId = output.Id,
			//	Author = this.userProvider.CurrentUser
			//};
			//this.eventPublisher.Publish<ForumCreated>(afterEvent);

			return output;
		}

		public Boolean Delete(String forumId) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}

			this.loggingService.Application.DebugWriteFormat("Delete called on ForumService, Id: {0}", forumId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /*|| !currentUser.acc(this.permissionService) TODO */) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to delete a forum, id: {0}", forumId);
				throw new PermissionException("delete forum", currentUser);
			}

			return this.dataStore.DeleteCategory(forumId);
		}

		public IEnumerable<Forum> FindAll() {
			this.loggingService.Application.DebugWrite("FindAll called on ForumService");

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			// TODO: Permissions!!
			return this.dataStore.FindAllForums();
		}

		public Forum FindById(String forumId) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}

			this.loggingService.Application.DebugWriteFormat("FindById called on ForumService, Id: {0}", forumId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			// TODO: Permissions!!
			return this.dataStore.FindForumById(forumId);
		}

		public Forum FindForumPlus2Levels(String forumId) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			return this.dataStore.FindForumPlus2Levels(forumId);
		}

		public Forum Update(String forumId, String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(forumId)) {
				throw new ArgumentNullException(nameof(forumId));
			}
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}

			this.loggingService.Application.DebugWriteFormat("Update called on ForumService, Id: {0}, Name: {1}, Description: {2}, Sort Order: {3}", forumId, name, description, sortOrder);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null /*|| !currentUser.CanUpdateCategory(this.permissionService) TODO */) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to update a forum, Id: {0}", forumId);
				throw new PermissionException("create forum", currentUser);
			}

			Forum output = this.dataStore.UpdateForum(forumId, name, sortOrder, description);
			this.loggingService.Application.DebugWriteFormat("Forum updated in ForumService, Id: {0}", output.Id);

			//ForumUpdated afterEvent = new ForumUpdated {
			//	Name = output.Name,
			//	CategoryId = output.Id,
			//	Author = this.userProvider.CurrentUser
			//};
			//this.eventPublisher.Publish<ForumUpdated>(afterEvent);

			return output;
		}
	}
}
