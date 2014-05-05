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

		public CategoryService(IUserProvider userProvider,
					ICategoryRepository categoryRepo,
					IEventPublisher eventPublisher,
					ILogger logger) {

			this.categoryRepo = categoryRepo;
			this.userProvider = userProvider;
			this.eventPublisher = eventPublisher;
			this.logger = logger;
		}

		private Boolean IsSolutionAdministrator(User user) {
			if (user == null) {
				return false;
			}
			// TODO:
			return true;
		}

		/// <summary>
		/// Method for creating a new category.
		/// </summary>
		/// <param name="board">The parent board of the category.</param>
		/// <param name="name">The name of the category.</param>
		/// <param name="description">The description of the category.</param>
		/// <param name="sortOrder">The sort order/placement of the category.</param>
		/// <returns>The newly created category.</returns>
		public Category Create(Board board, String name, String description, Int32 sortOrder) {
			if (board == null) {
				throw new ArgumentNullException("board");
			}
			this.logger.WriteFormat("Create called on CategoryService, Name: {0}, Description: {1}, Sort Order: {2}, board id: {3}", name, description, sortOrder, board.Id);
			if (!this.IsSolutionAdministrator(this.userProvider.CurrentUser)) {
				this.logger.WriteFormat("User does not have permissions to create a new category, name: {0}", name);
				throw new PermissionException("solution admin");
			}

			Category c = new Category {
				Name = name,
				SortOrder = sortOrder,
				Description = description,
				BoardId = board.Id
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
			return this.categoryRepo.Read(id);
		}

		/// <summary>
		/// Method for reading a category by its name.
		/// </summary>
		/// <param name="name">The name of the category to read.</param>
		/// <returns></returns>
		public Category Read(String name) {
			this.logger.WriteFormat("Read called on CategoryService, name: {0}", name);
			return this.categoryRepo.ByName(name);
		}

		/// <summary>
		/// Method for reading categories by their parent board.
		/// </summary>
		/// <param name="board">The parent board of the categories to read.</param>
		/// <returns></returns>
		public IEnumerable<Category> Read(Board board) {
			if (board == null) {
				throw new ArgumentNullException("board");
			}
			this.logger.WriteFormat("Read called on CategoryService, board Id: {0}", board.Id);
			return this.categoryRepo.ReadAll().Where(c => c.BoardId == board.Id).ToList();
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
			Category originalCategory = oldCategory.Clone() as Category;
			if (!this.IsSolutionAdministrator(this.userProvider.CurrentUser)) {
				this.logger.WriteFormat("User does not have permissions to update a category, name: {0}", category.Name);
				throw new PermissionException("solution admin");
			}

			if (oldCategory != null) {
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
			this.logger.WriteFormat("Update category failed, no category with the given id was found, Id: {0}", category.Id);
			// TODO:
			throw new ApplicationException();
		}

		/// <summary>
		/// Method for deleting a category.
		/// </summary>
		/// <param name="category">The category to delete.</param>
		public void Delete(Category category) {
			this.logger.WriteFormat("Delete called on CategoryService, Id: {0}", category.Id);
			// TODO: Delete forums, topics, posts, access masks etc etc.
			this.categoryRepo.Delete(category);
			this.eventPublisher.Publish<CategoryDeleted>(new CategoryDeleted {
				Category = category
			});
			this.logger.WriteFormat("Delete events in CategoryService fired, Id: {0}", category.Id);
		}
	}
}