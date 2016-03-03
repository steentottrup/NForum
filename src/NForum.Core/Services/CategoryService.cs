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

		/// <summary>
		/// Use this method to create a new <see cref="Category"/>.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="sortOrder">The sort order.</param>
		/// <param name="description">The description.</param>
		/// <returns>The created category.</returns>
		/// <exception cref="ArgumentNullException">If the name parameter or null/empty string.</exception>
		/// <exception cref="PermissionException">If the current user does not have the required permissions.</exception>
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

			Category output = this.dataStore.CreateCategory(name, sortOrder, description);
			this.loggingService.Application.DebugWriteFormat("Category created in CategoryService, Id: {0}", output.Id);

			CategoryCreated afterEvent = new CategoryCreated {
				Name = output.Name,
				CategoryId = output.Id,
				Author = this.userProvider.CurrentUser
			};
			this.eventPublisher.Publish<CategoryCreated>(afterEvent);

			return output;
		}

		/// <summary>
		/// Method for deleting an existing <see cref="Category"/>.
		/// </summary>
		/// <param name="categoryId">Id of the category.</param>
		/// <returns>True if the category was deleted, false otherwise.</returns>
		/// <exception cref="ArgumentNullException">If the categoryId parameter is null/empty.</exception>
		/// <exception cref="PermissionException">If the current user does not have the required permissions.</exception>
		public Boolean Delete(String categoryId) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}

			this.loggingService.Application.DebugWriteFormat("Delete called on CategoryService, Id: {0}", categoryId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null || !currentUser.CanDeleteCategory(this.permissionService)) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to delete a category, id: {0}", categoryId);
				throw new PermissionException("delete category", currentUser);
			}

			return this.dataStore.DeleteCategory(categoryId);
		}

		public IEnumerable<Category> FindAll() {
			this.loggingService.Application.DebugWrite("FindAll called on CategoryService");

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			// TODO: Permissions!!
			return this.dataStore.FindAll();
		}

		public Category FindById(String categoryId) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}

			this.loggingService.Application.DebugWriteFormat("FindById called on CategoryService, Id: {0}", categoryId);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			// TODO: Permissions!!
			return this.dataStore.FindCategoryById(categoryId);
		}

		/// <summary>
		/// Use this method to update an existing <see cref="Category"/>.
		/// </summary>
		/// <param name="categoryId">Id of the category</param>
		/// <param name="name">The updated name.</param>
		/// <param name="sortOrder">The updated sort order.</param>
		/// <param name="description">The updated description.</param>
		/// <returns>The updated category.</returns>
		/// <exception cref="ArgumentNullException">If the name or categoryId parameters or null/empty strings.</exception>
		/// <exception cref="PermissionException">If the current user does not have the required permissions.</exception>
		public Category Update(String categoryId, String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(categoryId)) {
				throw new ArgumentNullException(nameof(categoryId));
			}
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}

			this.loggingService.Application.DebugWriteFormat("Update called on CategoryService, Id: {0}, Name: {1}, Description: {2}, Sort Order: {3}", categoryId, name, description, sortOrder);

			IAuthenticatedUser currentUser = this.userProvider.CurrentUser;
			if (currentUser == null || !currentUser.CanUpdateCategory(this.permissionService)) {
				this.loggingService.Application.DebugWriteFormat("User does not have permissions to update a category, Id: {0}", categoryId);
				throw new PermissionException("create category", currentUser);
			}

			Category output = this.dataStore.UpdateCategory(categoryId, name, sortOrder, description);
			this.loggingService.Application.DebugWriteFormat("Category updated in CategoryService, Id: {0}", output.Id);

			CategoryUpdated afterEvent = new CategoryUpdated {
				Name = output.Name,
				CategoryId = output.Id,
				Author = this.userProvider.CurrentUser
			};
			this.eventPublisher.Publish<CategoryUpdated>(afterEvent);

			return output;
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
	}
}
