using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IPermissionService {
		Boolean HasAccess(User user, Forum forum);
		Boolean HasAccess(User user, Forum forum, CRUD access);
		Boolean HasAccess(User user, Forum forum, Int64 accessMask);
		Boolean HasAccess(User user, Category category);
		Boolean HasAccess(User user, Category category, CRUD access);

		Boolean CanCreateCategory(User user);
		Boolean CanCreateForum(User user, Category category);

		IEnumerable<Category> GetAccessible(User user, IEnumerable<Category> categories);
		IEnumerable<Forum> GetAccessible(User user, IEnumerable<Forum> forums);

		AccessFlag GetAccessFlag(User user, Forum forum);
	}
}