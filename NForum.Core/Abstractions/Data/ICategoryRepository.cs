using System;

namespace NForum.Core.Abstractions.Data {

	public interface ICategoryRepository : IRepository<Category> {
		Category ByName(String name);
		Category ByForum(Forum forum);
		Category ByTopic(Topic topic);
		//Category ByPost(Post post);
	}
}