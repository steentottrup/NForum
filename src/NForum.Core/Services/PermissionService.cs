using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class PermissionService : IPermissionService {

		public Boolean CanCreateCategory(IAuthenticatedUser user) {
			// TODO:
			return true;
		}

		public Boolean CanCreateForum(IAuthenticatedUser user, Category category) {
			return this.HasAccess(user, category, CRUD.Create);
		}

		public AccessFlag GetAccessFlag(IAuthenticatedUser user, Forum forum) {
			// TODO:
			return AccessFlag.None;
		}

		public IEnumerable<Forum> GetAccessible(IAuthenticatedUser user, IEnumerable<Forum> forums) {
			// TODO:
			return new List<Forum>();
		}

		public IEnumerable<Category> GetAccessible(IAuthenticatedUser user, IEnumerable<Category> categories) {
			// TODO:
			return new List<Category>();
		}

		public Boolean HasAccess(IAuthenticatedUser user, Category category) {
			// TODO:
			return this.HasAccess(user, category, CRUD.Read);
		}

		public Boolean HasAccess(IAuthenticatedUser user, Forum forum) {
			return this.HasAccess(user, forum, CRUD.Read);
		}

		public Boolean HasAccess(IAuthenticatedUser user, Category category, CRUD access) {
			// TODO:
			return true;
		}

		public Boolean HasAccess(IAuthenticatedUser user, Forum forum, Int64 accessMask) {
			// TODO:
			return true;
		}

		public Boolean HasAccess(IAuthenticatedUser user, Forum forum, CRUD access) {
			// TODO:
			return true;
		}
	}
}
