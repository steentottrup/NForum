using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IForumRepository : IRepository<Forum> {
		Forum ByName(String name);
		IEnumerable<Forum> ByCategory(Category category);
		Forum ByForum(Forum forum);
		Forum ByTopic(Topic topic);
		Forum ByPost(Post post);
	}
}