using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IFollowForumRepository : IRepository<FollowForum> {
		IEnumerable<FollowForum> ByForum(Forum forum);
		FollowForum ByUserAndForum(Forum forum, User user);
	}
}