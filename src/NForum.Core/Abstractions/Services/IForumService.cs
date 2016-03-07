using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IForumService {
		Forum Create(String categoryId, String name, Int32 sortOrder, String description);
		Forum CreateSubForum(String forumId, String name, Int32 sortOrder, String description);
		Forum FindById(String forumId);
		IEnumerable<Forum> FindAll();
		Forum Update(String forumId, String name, Int32 sortOrder, String description);
		Boolean Delete(String forumId);
	}
}
