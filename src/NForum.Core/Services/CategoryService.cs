using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;
using System.Collections.Generic;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Events;
using NForum.Core.Events;

namespace NForum.Core.Services {

	public class CategoryService : ICategoryService {
		protected readonly IDataStore dataStore;
		protected readonly IPermissionService permissionService;
		protected readonly ILoggingService loggingService;
		protected readonly IUserProvider userProvider;
		protected readonly IEventPublisher eventPublisher;

		public CategoryService(IDataStore dataStore, IPermissionService permissionService, Abstractions.Services.ILoggingService loggingService, IUserProvider userProvider, IEventPublisher eventPublisher) {
			this.dataStore = dataStore;
			this.permissionService = permissionService;
			this.loggingService = loggingService;
			this.userProvider = userProvider;
			this.eventPublisher = eventPublisher;

			// TODO: Log all contructor param types!
		}

		public Category Create(String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}

			this.loggingService.Application.DebugWriteFormat("Create called on CategoryService, Name: {0}, Description: {1}, Sort Order: {2}", name, description, sortOrder);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null || !currentUser.CanCreateCategory(this.permissionService)) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to create a new category, name: {0}", name);
				throw new PermissionException("create category", currentUser);
			}

			BeforeCreateCategory beforeEvent = new BeforeCreateCategory();
			this.eventPublisher.Publish<BeforeCreateCategory>(beforeEvent);

			Category output = this.dataStore.CreateCategory(name, sortOrder, description);
			this.loggingService.Application.DebugWriteFormat("Category created in CategoryService, Id: {0}", output.Id);

			AfterCreateCategory afterEvent = new AfterCreateCategory();
			this.eventPublisher.Publish<AfterCreateCategory>(afterEvent);

			return output;
		}

		public Boolean Delete(String categoryId) {
			throw new NotImplementedException();
		}

		public IEnumerable<Category> FindAll() {
			// TODO: Permissions!!
			return this.dataStore.FindAll();
		}

		public Category FindById(String categoryId) {
			// TODO: Permissions!!
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			return this.dataStore.FindCategoryById(categoryId);
		}

		public Category Update(String categoryId, String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}
			return this.dataStore.UpdateCategory(categoryId, name, sortOrder, description);
		}

		public IEnumerable<Category> FindCategoriesPlus2Levels() {
			return this.dataStore.FindCategoriesPlus2Levels();
		}

		public Category FindCategoryPlus2Levels(String categoryId) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			return this.dataStore.FindCategoryPlus2Levels(categoryId);
		}
	}
}
