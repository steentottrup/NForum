using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class ForumTrackerRepository : RepositoryBase<ForumTracker>, IForumTrackerRepository {

		public ForumTrackerRepository(UnitOfWork uow)
			: base(uow) {
		}

		public ForumTracker ByUserAndForum(User user, Forum forum) {
			return this.Set
					.FirstOrDefault(ft => ft.UserId == user.Id && ft.ForumId == forum.Id);
		}

		public IEnumerable<ForumTracker> ByUser(User user) {
			return this.Set
					.Where(ft => ft.UserId == user.Id)
					.ToList();
		}

		public IEnumerable<ForumTracker> ByUserAndParentForum(User user, Forum forum) {
			return this.Set
					.Include(ft => ft.Forum)
					.Where(ft => ft.UserId == user.Id && ft.Forum.ParentForumId == forum.Id)
					.ToList();
		}

		public IEnumerable<ForumTracker> ByUserAndForums(User user, IEnumerable<Forum> forums) {
			return this.Set
					.Include(ft => ft.Forum)
					.Where(ft => ft.UserId == user.Id && ft.Forum.ParentForumId.HasValue && forums.Select(f => f.Id).Contains(ft.Forum.ParentForumId.Value) == true)
					.ToList();
		}
	}
}