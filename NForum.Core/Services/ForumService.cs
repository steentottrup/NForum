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

	public class ForumService : IForumService {
		protected readonly ICategoryRepository categoryRepo;
		protected readonly IForumRepository forumRepo;
		protected readonly IUserProvider userProvider;
		protected readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;
		protected readonly IPermissionService permService;

		public ForumService(IUserProvider userProvider,
							ICategoryRepository categoryRepo,
							IForumRepository forumRepo,
							IEventPublisher eventPublisher,
							ILogger logger,
							IPermissionService permService) {

			this.userProvider = userProvider;
			this.categoryRepo = categoryRepo;
			this.forumRepo = forumRepo;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
			this.permService = permService;
		}

		/// <summary>
		/// Method for creating a new forum.
		/// </summary>
		/// <param name="category">The parent category of the forum.</param>
		/// <param name="name">The name of the forum.</param>
		/// <param name="sortOrder">The sort order/placement of the forum.</param>
		/// <returns>The newly created forum.</returns>
		public Forum Create(Category category, String name, Int32 sortOrder) {
			return this.Create(category, name, String.Empty, sortOrder);
		}

		/// <summary>
		/// Method for creating a new forum.
		/// </summary>
		/// <param name="category">The parent category of the forum.</param>
		/// <param name="name">The name of the forum.</param>
		/// <param name="description">The description of the forum.</param>
		/// <param name="sortOrder">The sort order/placement of the forum.</param>
		/// <returns>The newly created forum.</returns>
		public Forum Create(Category category, String name, String description, Int32 sortOrder) {
			return this.Create(category, null, name, description, sortOrder);
		}

		/// <summary>
		/// Method for creating a new forum.
		/// </summary>
		/// <param name="category">The parent category of the forum.</param>
		/// <param name="parentForum">The (optional) parent forum of the forum.</param>
		/// <param name="name">The name of the forum.</param>
		/// <param name="description">The description of the forum.</param>
		/// <param name="sortOrder">The sort order/placement of the forum.</param>
		/// <returns>The newly created forum.</returns>
		public Forum Create(Category category, Forum parentForum, String name, String description, Int32 sortOrder) {
			if (category == null) {
				throw new ArgumentNullException("category");
			}
			category = this.categoryRepo.Read(category.Id);
			if (category == null) {
				throw new ArgumentException("category does not exist");
			}

			this.logger.WriteFormat("Create called on ForumService, Name: {0}, Description: {1}, Sort Order: {2}, category id: {3}", name, description, sortOrder, category.Id);
			if (!this.permService.CanCreateForum(this.userProvider.CurrentUser, category)) {
				this.logger.WriteFormat("User does not have permissions to create a new forum in category {1}, name: {0}", name, category.Id);
				throw new PermissionException("forum, create");
			}

			Forum f = new Forum {
				Name = name,
				SortOrder = sortOrder,
				Description = description,
				CategoryId = category.Id
			};
			if (parentForum != null) {
				parentForum = this.forumRepo.Read(parentForum.Id);
				if (parentForum == null) {
					throw new ArgumentException("parentForum does not exist");
				}
				f.ParentForumId = parentForum.Id;
			}
			// TODO: Custom properties?

			this.forumRepo.Create(f);
			this.logger.WriteFormat("Forum created in ForumService, Id: {0}", f.Id);
			this.eventPublisher.Publish<ForumCreated>(new ForumCreated {
				Forum = f
			});
			this.logger.WriteFormat("Create events in ForumService fired, Id: {0}", f.Id);

			return f;
		}

		/// <summary>
		/// Method for creating a new forum.
		/// </summary>
		/// <param name="parentForum">The parent forum of the forum.</param>
		/// <param name="name">The name of the forum.</param>
		/// <param name="sortOrder">The sort order/placement of the forum.</param>
		/// <returns>The newly created forum.</returns>
		public Forum Create(Forum parentForum, String name, Int32 sortOrder) {
			return this.Create(parentForum, name, String.Empty, sortOrder);
		}

		/// <summary>
		/// Method for creating a new forum.
		/// </summary>
		/// <param name="parentForum">The (optional) parent forum of the forum.</param>
		/// <param name="name">The name of the forum.</param>
		/// <param name="description">The description of the forum.</param>
		/// <param name="sortOrder">The sort order/placement of the forum.</param>
		/// <returns>The newly created forum.</returns>
		public Forum Create(Forum parentForum, String name, String description, Int32 sortOrder) {
			if (parentForum == null) {
				throw new ArgumentNullException("parentForum");
			}
			return this.Create(this.categoryRepo.Read(parentForum.CategoryId), parentForum, name, description, sortOrder);
		}

		/// <summary>
		/// Method for reading a forum by its id.
		/// </summary>
		/// <param name="id">Id of the forum to read.</param>
		/// <returns></returns>
		public Forum Read(Int32 id) {
			this.logger.WriteFormat("Read called on ForumService, Id: {0}", id);
			Forum forum = this.forumRepo.Read(id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, forum, CRUD.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the forum, id: {0}", forum.Id);
				throw new PermissionException("forum, read");
			}

			return forum;
		}

		/// <summary>
		/// Method for reading a forum by its name.
		/// </summary>
		/// <param name="name">The name of the forum to read.</param>
		/// <returns></returns>
		public Forum Read(String name) {
			this.logger.WriteFormat("Read called on ForumService, name: {0}", name);
			Forum forum = this.forumRepo.ByName(name);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, forum, CRUD.Read)) {
				this.logger.WriteFormat("User does not have permissions to read the forum, name: {0}", forum.Name);
				throw new PermissionException("forum, read");
			}

			return forum;
		}

		/// <summary>
		/// Method for reading forums by their parent category.
		/// </summary>
		/// <param name="cateogry">The parent category of the forums to read.</param>
		/// <returns></returns>
		public IEnumerable<Forum> Read(Category category) {
			if (category == null) {
				throw new ArgumentNullException("category");
			}
			category = this.categoryRepo.Read(category.Id);
			if (category == null) {
				throw new ArgumentException("category does not exist");
			}

			this.logger.WriteFormat("Read called on ForumService, category id: {0}, name: {1}", category.Id, category.Name);
			return this.permService.GetAccessible(
							this.userProvider.CurrentUser,
							this.forumRepo
								.ByCategory(category)
								.ToList()
						);
		}

		/// <summary>
		/// Method for reading all forums.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Forum> Read() {
			this.logger.Write("Read called on ForumService");
			return this.permService.GetAccessible(
							this.userProvider.CurrentUser,
							this.forumRepo
								.ReadAll()
								.ToList()
						);
		}

		/// <summary>
		/// Method for updating a forum.
		/// </summary>
		/// <param name="forum">The changed forum.</param>
		/// <returns>The updated forum.</returns>
		public Forum Update(Forum forum) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			this.logger.WriteFormat("Update called on ForumService, Id: {0}", forum.Id);
			// Let's get the forum from the data-storage!
			Forum oldForum = this.Read(forum.Id);
			if (oldForum == null) {
				this.logger.WriteFormat("Update forum failed, no forum with the given id was found, Id: {0}", forum.Id);
				throw new ArgumentException("forum does not exist");
			}
			Forum originalForum = oldForum.Clone() as Forum;
			Boolean changed = false;
			if (forum.Name != oldForum.Name) {
				oldForum.Name = forum.Name;
				changed = true;
			}
			if (forum.SortOrder != oldForum.SortOrder) {
				oldForum.SortOrder = forum.SortOrder;
				changed = true;
			}
			if (forum.Description != oldForum.Description) {
				oldForum.Description = forum.Description;
				changed = true;
			}
			if (forum.CustomProperties != oldForum.CustomProperties) {
				oldForum.CustomProperties = forum.CustomProperties;
				changed = true;
			}

			if (changed) {
				oldForum = this.forumRepo.Update(oldForum);
				this.logger.WriteFormat("Forum updated in ForumService, Id: {0}", forum.Id);
				this.eventPublisher.Publish<ForumUpdated>(new ForumUpdated {
					Forum = originalForum,
					UpdatedForum = oldForum
				});
				this.logger.WriteFormat("Update events in ForumService fired, Id: {0}", forum.Id);
			}
			return oldForum;
		}

		/// <summary>
		/// Method for deleting a forum.
		/// </summary>
		/// <param name="forum">The forum to delete.</param>
		public void Delete(Forum forum) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			this.logger.WriteFormat("Delete called on ForumService, Id: {0}", forum.Id);
			if (!this.permService.HasAccess(this.userProvider.CurrentUser, forum, CRUD.Delete)) {
				this.logger.WriteFormat("User does not have permissions to delete a forum, id: {1}, name: {0}", forum.Name, forum.Id);
				throw new PermissionException("forum, delete");
			}

			// TODO: Delete topics/posts/etc
			this.forumRepo.Delete(forum);
			this.eventPublisher.Publish<ForumDeleted>(new ForumDeleted {
				Forum = forum
			});
			this.logger.WriteFormat("Delete events in ForumService fired, Id: {0}", forum.Id);
		}
	}
}