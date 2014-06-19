using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace NForum.Core.Services {

	public class PermissionService : IPermissionService {

		public Boolean HasAccess(User user, Forum forum) {
			return this.HasAccess(user, forum, CRUD.Read);
		}

		public Boolean HasAccess(User user, Forum forum, CRUD access) {
			return true;
		}

		public Boolean HasAccess(User user, Forum forum, Int64 accessMask) {
			return true;
		}

		public IEnumerable<Category> GetAccessible(User user, IEnumerable<Category> categories) {
			return categories;
		}

		public IEnumerable<Forum> GetAccessible(User user, IEnumerable<Forum> forums) {
			return forums;
		}

		public Boolean HasAccess(User user, Category category) {
			return this.HasAccess(user, category, CRUD.Read);
		}

		public Boolean HasAccess(User user, Category category, CRUD access) {
			return true;
		}

		public Boolean CanCreateCategory(User user) {
			return true;
		}

		public Boolean CanCreateForum(User user, Category category) {
			return true;
		}

		public AccessFlag GetAccessFlag(User user, Forum forum) {
			return AccessFlag.Create | AccessFlag.Delete | AccessFlag.Moderator |
					AccessFlag.Poll | AccessFlag.Priority | AccessFlag.Priority |
					AccessFlag.Read | AccessFlag.Reply | AccessFlag.Update |
					AccessFlag.Upload | AccessFlag.Vote;
		}
	}
}