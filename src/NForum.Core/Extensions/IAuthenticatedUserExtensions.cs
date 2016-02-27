using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core {

	public static class IAuthenticatedUserExtensions {

		public static Boolean CanCreateCategory(this IAuthenticatedUser user, IPermissionService permissions) {
			return permissions.CanCreateCategory(user);
		}

		public static Boolean CanDeleteCategory(this IAuthenticatedUser user, IPermissionService permissions) {
			// TODO:
			return false;
		}

		public static Boolean CanUpdateCategory(this IAuthenticatedUser user, IPermissionService permissions) {
			// TODO:
			return false;
		}

		public static Boolean CanViewCategory(this IAuthenticatedUser user, IPermissionService permissions, String categoryId) {
			// TODO:
			return false;
		}
	}
}
