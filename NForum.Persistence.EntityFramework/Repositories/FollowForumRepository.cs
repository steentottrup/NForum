using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class FollowForumRepository : RepositoryBase<FollowForum>, IFollowForumRepository {

		public FollowForumRepository(UnitOfWork uow)
			: base(uow) {
		}

		public IEnumerable<FollowForum> ByForum(Forum forum) {
			return this.set.Include(ff => ff.User)
						.Where(ff => ff.ForumId == forum.Id)
						.ToList();
		}

		public FollowForum ByUserAndForum(Forum forum, User user) {
			return this.set.FirstOrDefault(ff => ff.ForumId == forum.Id && ff.UserId == user.Id);
		}
	}
}