using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Events;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Events.Payloads;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class ForumService : IForumService {
		protected readonly IBoardRepository boardRepo;
		protected readonly ICategoryRepository categoryRepo;
		protected readonly IForumRepository forumRepo;
		protected readonly IUserProvider userProvider;
		protected readonly ILogger logger;
		protected readonly IEventPublisher eventPublisher;

		public ForumService(IUserProvider userProvider,
							IBoardRepository boardRepo,
							ICategoryRepository categoryRepo,
							IForumRepository forumRepo,
							ILogger logger,
							IEventPublisher eventPublisher) {

			this.userProvider = userProvider;
			this.boardRepo = boardRepo;
			this.categoryRepo = categoryRepo;
			this.forumRepo = forumRepo;
			this.logger = logger;
			this.eventPublisher = eventPublisher;
		}

		private Boolean IsBoardAdministrator(User user, Board board) {
			if (user == null) {
				return false;
			}
			// TODO:
			return true;
		}

		private Boolean IsSolutionAdministrator(User user) {
			if (user == null) {
				return false;
			}
			// TODO:
			return true;
		}

		public Forum Create(Category category, String name, Int32 sortOrder) {
			return this.Create(category, null, name, String.Empty, sortOrder);
		}

		public Forum Create(Category category, String name, String description, Int32 sortOrder) {
			return this.Create(category, null, name, description, sortOrder);
		}

		public Forum Create(Category category, Forum parentForum, String name, String description, Int32 sortOrder) {
			this.logger.WriteFormat("Create called on ForumService, Name: {0}, Description: {1}, Sort Order: {2}, category id: {3}", name, description, sortOrder, category.Id);
			if (!this.IsSolutionAdministrator(this.userProvider.CurrentUser) &&
				!this.IsBoardAdministrator(this.userProvider.CurrentUser, this.boardRepo.Read(category.BoardId))) {

				this.logger.WriteFormat("User does not have permissions to create a new forum, name: {0}", name);
				throw new PermissionException("solution admin/board admin");
			}

			Forum f = new Forum {
				Name = name,
				SortOrder = sortOrder,
				Description = description,
				CategoryId = category.Id
			};
			if (parentForum != null) {
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

		public Forum Create(Forum parentForum, String name, Int32 sortOrder) {
			return this.Create(this.categoryRepo.Read(parentForum.CategoryId), parentForum, name, String.Empty, sortOrder);
		}

		public Forum Create(Forum parentForum, String name, String description, Int32 sortOrder) {
			return this.Create(this.categoryRepo.Read(parentForum.CategoryId), parentForum, name, description, sortOrder);
		}

		/// <summary>
		/// Method for reading a forum by its id.
		/// </summary>
		/// <param name="id">Id of the forum to read.</param>
		/// <returns></returns>
		public Forum Read(Int32 id) {
			return this.forumRepo.Read(id);
		}

		/// <summary>
		/// Method for reading a forum by its name.
		/// </summary>
		/// <param name="name">The name of the forum to read.</param>
		/// <returns></returns>
		public Forum Read(String name) {
			return this.forumRepo.ByName(name);
		}

		public IEnumerable<Forum> Read(Category category) {
			return this.forumRepo.ByCategory(category);
		}

		public IEnumerable<Forum> Read(Board board) {
			return this.forumRepo.ByBoard(board);
		}

		public Forum Update(Forum forum) {
			if (forum == null) {
				throw new ArgumentNullException("forum");
			}
			if (!this.IsSolutionAdministrator(this.userProvider.CurrentUser) &&
				!this.IsBoardAdministrator(this.userProvider.CurrentUser, this.boardRepo.Read(this.categoryRepo.Read(forum.CategoryId).BoardId))) {

				throw new PermissionException("solution admin/board admin");
			}

			Forum oldForum = this.Read(forum.Id);
			Forum originalForum = oldForum.Clone() as Forum;
			if (oldForum != null) {
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
			this.logger.WriteFormat("Update forum failed, no forum with the given id was found, Id: {0}", forum.Id);
			// TODO:
			throw new ApplicationException();
		}

		/// <summary>
		/// Method for deleting a forum.
		/// </summary>
		/// <param name="forum">The forum to delete.</param>
		public void Delete(Forum forum) {
			this.logger.WriteFormat("Delete called on ForumService, Id: {0}", forum.Id);
			if (!this.IsSolutionAdministrator(this.userProvider.CurrentUser) &&
				!this.IsBoardAdministrator(this.userProvider.CurrentUser, this.boardRepo.Read(this.categoryRepo.Read(forum.CategoryId).BoardId))) {

				this.logger.WriteFormat("User does not have permissions to delete a forum, Id: {0}", forum.Id);
				throw new PermissionException("solution admin/board admin");
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