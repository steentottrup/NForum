using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IPermissionService {
		Boolean HasAccess(IAuthenticatedUser user, Forum forum);
		Boolean HasAccess(IAuthenticatedUser user, Forum forum, CRUD access);
		Boolean HasAccess(IAuthenticatedUser user, Forum forum, Int64 accessMask);
		Boolean HasAccess(IAuthenticatedUser user, Category category);
		Boolean HasAccess(IAuthenticatedUser user, Category category, CRUD access);

		Boolean CanCreateCategory(IAuthenticatedUser user);
		Boolean CanCreateForum(IAuthenticatedUser user, Category category);

		IEnumerable<Category> GetAccessible(IAuthenticatedUser user, IEnumerable<Category> categories);
		IEnumerable<Forum> GetAccessible(IAuthenticatedUser user, IEnumerable<Forum> forums);

		AccessFlag GetAccessFlag(IAuthenticatedUser user, Forum forum);
	}
}
