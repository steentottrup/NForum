using NForum.Domain.Abstractions;
using System;
using NForum.Domain;
using System.Security.Principal;
using NForum.Infrastructure;
using NForum.Datastores;

namespace NForum.Core {

	public class ElementPermissionCheck : IElementPermissionCheck {
		protected readonly IUserProvider userProvider;
		protected readonly IPermissionDatastore permissions;

		public ElementPermissionCheck(IUserProvider userProvider, IPermissionDatastore permissions) {
			this.userProvider = userProvider;
			this.permissions = permissions;
		}

		public Boolean HasPermissions(String forumId, Int32 permission, IPrincipal user) {
			Int32 flag = this.permissions.GetPermission(forumId, this.userProvider.Get(user).GetId());

			// TODO: Test, test, test!!!!
			return (flag & permission) == permission;
		}

		public Boolean HasPermissions(String forumId, Permissions permission, IPrincipal user) {
			return this.HasPermissions(forumId, (Int32)permission, user);
		}
	}
}
