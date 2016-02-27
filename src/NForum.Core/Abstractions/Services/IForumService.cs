using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IForumService {
		Forum Create(IAuthenticatedUser currentUser, String name, Int32 sortOrder, String description, String categoryId);
		Forum CreateSubForum(IAuthenticatedUser currentUser, String name, Int32 sortOrder, String description, String forumId);
		Forum FindById(IAuthenticatedUser currentUser, String forumId);
		IEnumerable<Forum> FindAll(IAuthenticatedUser currentUser);
		Forum Update(IAuthenticatedUser currentUser, String forumId, String name, Int32 sortOrder, String description);
		Boolean Delete(IAuthenticatedUser currentUser, String forumId);

		Forum FindForumPlus2Levels(/* Permissions/user */String forumId);
	}
}
