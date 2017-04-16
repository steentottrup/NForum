using System;
using System.Security.Principal;

namespace NForum.Domain.Abstractions {

	public interface IElementPermissionCheck {
		Boolean HasPermissions(String forumId, Permissions permission, IPrincipal user);
		Boolean HasPermissions(String forumId, Int32 permission, IPrincipal user);
	}
}
