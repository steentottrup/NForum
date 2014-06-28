using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IForumTrackerRepository : IRepository<ForumTracker> {
		ForumTracker ByUserAndForum(User user, Forum forum);
		IEnumerable<ForumTracker> ByUser(User user);
		IEnumerable<ForumTracker> ByUserAndParentForum(User user, Forum forum);
		IEnumerable<ForumTracker> ByUserAndForums(User user, IEnumerable<Forum> forums);
	}
}