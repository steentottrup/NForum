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
			return this.set
					.FirstOrDefault(ft => ft.UserId == user.Id && ft.ForumId == forum.Id);
		}

		public IEnumerable<ForumTracker> ByUser(User user) {
			return this.set
					.Where(ft => ft.UserId == user.Id)
					.ToList();
		}

		public IEnumerable<ForumTracker> ByUserAndParentForum(User user, Forum forum) {
			return this.set
					.Include(ft => ft.Forum)
					.Where(ft => ft.UserId == user.Id && ft.Forum.ParentForumId == forum.Id)
					.ToList();
		}
	}
}