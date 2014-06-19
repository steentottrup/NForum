using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Core.Services {

	public class CategoryService : ICategoryService {
		protected readonly ICategoryRepository categoryRepo;
		protected readonly IUserProvider userProvider;
		protected readonly IEventPublisher eventPublisher;
		protected readonly ILogger logger;
		protected readonly IPermissionService permService;

		public CategoryService(IUserProvider userProvider,
					ICategoryRepository categoryRepo,
					IEventPublisher eventPublisher,
					ILogger logger,
					IPermissionService permService) {

			this.categoryRepo = categoryRepo;
			this.userProvider = userProvider;
			this.eventPublisher = eventPublisher;
			this.logger = logger;
			this.permService = permService;
		}

		/// <summary>
		/// Method for creating a new category.
		/// </summary>
		/// <param name="name">The name of the category.</param>
		/// <param name="description">The description of the category.</param>
		/// <param name="sortOrder">The sort order/placement of the category.</param>
		/// <returns>The newly created category.</returns>
		public Category Create(String name, String description, Int32 sortOrder) {
			this.logger.WriteFormat("Create called on CategoryService, Name: {0}, Description: {1}, Sort Order: {2}", name, description, sortOrder);
			if (!this.permService.CanCreateCategory(this.userProvider.CurrentUser)) {
				this.logger.WriteFormat("User does not have permissions to create a new category, name: {0}", name);
				throw new PermissionException("category, create");
			}

			Category c = new Category {
				Name = name,
				SortOrder = sortOrder,
				Description = description
			};

			this.categoryRepo.Create(c);
			this.logger.WriteFormat("Category created in CategoryService, Id: {0}", c.Id);
			this.eventPublisher.Publish<CategoryCreated>(new CategoryCreated {
				Category = c
			});
			this.logger.WriteFormat("Create events in CategoryService fired, Id: {0}", c.Id);

			return c;
		}

		/// <summary>
		/// Method for reading a category by its id.
		/// </summary>
		/// <param name="id">Id of the category to read.</param>
		/// <returns></returns>
		public Category Read(Int32 id) {
			this.logger.WriteFormat("Read called on CategoryService, Id: {0}", id);
			Category category = this.categoryRepo.Read(id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, category, CRUD.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the category, id: {0}", category.Id);
				throw new PermissionException("category, read");
			}

			return category;
		}

		/// <summary>
		/// Method for reading a category by its name.
		/// </summary>
		/// <param name="name">The name of the category to read.</param>
		/// <returns></returns>
		public Category Read(String name) {
			this.logger.WriteFormat("Read called on CategoryService, name: {0}", name);
			Category category = this.categoryRepo.ByName(name);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, category, CRUD.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the category, name: {0}", category.Name);
				throw new PermissionException("category, read");
			}

			return category;
		}

		/// <summary>
		/// Method for reading all categories.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Category> Read() {
			this.logger.Write("Read called on CategoryService");
			return this.permService.GetAccessible(
							this.userProvider.CurrentUser,
							this.categoryRepo
								.ReadAll()
								.ToList()
						);
		}

		/// <summary>
		/// Method for updating a category.
		/// </summary>
		/// <param name="category">The changed category.</param>
		/// <returns>The updated category.</returns>
		public Category Update(Category category) {
			if (category == null) {
				throw new ArgumentNullException("category");
			}
			this.logger.WriteFormat("Update called on CategoryService, Id: {0}", category.Id);
			// Let's get the category from the data-storage!
			Category oldCategory = this.Read(category.Id);
			if (oldCategory == null) {
				this.logger.WriteFormat("Update category failed, no category with the given id was found, Id: {0}", category.Id);
				throw new ArgumentException("category does not exist");
			}
			Category originalCategory = oldCategory.Clone() as Category;
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, oldCategory, CRUD.Update)) {
				this.logger.WriteFormat("User does not have permissions to update a category, id: {1}, name: {0}", category.Name, category.Id);
				throw new PermissionException("category, update");
			}

			Boolean changed = false;
			if (category.Name != oldCategory.Name) {
				oldCategory.Name = category.Name;
				changed = true;
			}
			if (category.SortOrder != oldCategory.SortOrder) {
				oldCategory.SortOrder = category.SortOrder;
				changed = true;
			}
			if (category.Description != oldCategory.Description) {
				oldCategory.Description = category.Description;
				changed = true;
			}
			if (category.CustomProperties != oldCategory.CustomProperties) {
				oldCategory.CustomProperties = category.CustomProperties;
				changed = true;
			}

			if (changed) {
				oldCategory = this.categoryRepo.Update(oldCategory);
				this.logger.WriteFormat("Board updated in CategoryService, Id: {0}", category.Id);
				this.eventPublisher.Publish<CategoryUpdated>(new CategoryUpdated {
					Category = originalCategory,
					UpdatedCategory = oldCategory
				});
				this.logger.WriteFormat("Update events in CategoryService fired, Id: {0}", category.Id);
			}
			return oldCategory;
		}

		/// <summary>
		/// Method for deleting a category.
		/// </summary>
		/// <param name="category">The category to delete.</param>
		public void Delete(Category category) {
			if (category == null) {
				throw new ArgumentNullException("category");
			}
			this.logger.WriteFormat("Delete called on CategoryService, Id: {0}", category.Id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, category, CRUD.Delete)) {
				this.logger.WriteFormat("User does not have permissions to delete a category, id: {1}, name: {0}", category.Name, category.Id);
				throw new PermissionException("category, delete");
			}
			// TODO: forums, topics, posts, attachments, etc
			this.categoryRepo.Delete(category);
			this.eventPublisher.Publish<CategoryDeleted>(new CategoryDeleted {
				Category = category
			});
			this.logger.WriteFormat("Delete events in CategoryService fired, Id: {0}", category.Id);
		}
	}
}