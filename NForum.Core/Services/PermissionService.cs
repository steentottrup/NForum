using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class PermissionService : IPermissionService {

		public Boolean HasAccess(User user, Forum forum) {
			return this.HasAccess(user, forum, CRUD.Read);
		}

		public Boolean HasAccess(User user, Forum forum, CRUD access) {
			// TODO:
			return true;
		}

		public Boolean HasAccess(User user, Forum forum, Int64 accessMask) {
			// TODO:
			return true;
		}

		public IEnumerable<Category> GetAccessible(User user, IEnumerable<Category> categories) {
			// TODO:
			return categories;
		}

		public IEnumerable<Forum> GetAccessible(User user, IEnumerable<Forum> forums) {
			// TODO:
			return forums;
		}

		public Boolean HasAccess(User user, Category category) {
			return this.HasAccess(user, category, CRUD.Read);
		}

		public Boolean HasAccess(User user, Category category, CRUD access) {
			// TODO:
			return true;
		}

		public Boolean CanCreateCategory(User user) {
			// TODO:
			return true;
		}

		public Boolean CanCreateForum(User user, Category category) {
			// TODO:
			return true;
		}

		public AccessFlag GetAccessFlag(User user, Forum forum) {
			// TODO:
			return AccessFlag.Create | AccessFlag.Delete | AccessFlag.Moderator |
					AccessFlag.Poll | AccessFlag.Priority | AccessFlag.Priority |
					AccessFlag.Read | AccessFlag.Reply | AccessFlag.Update |
					AccessFlag.Upload | AccessFlag.Vote;
		}
	}
}