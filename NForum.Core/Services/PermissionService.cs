using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core.Services {

	public class PermissionService : IPermissionService {

		public Boolean HasAccess(User user, Forum forum) {
			return true;
		}

		public Boolean HasAccess(User user, Forum forum, CRUD access) {
			return true;
		}

		public Boolean HasAccess(User user, Forum forum, Int64 accessMask) {
			return true;
		}

		public Boolean HasAccess(User user, Board board) {
			return true;
		}

		public Boolean HasAccess(User user, Board board, CRUD access) {
			return true;
		}

		public Boolean HasAccess(User user, Category category) {
			return true;
		}

		public Boolean HasAccess(User user, Category category, CRUD access) {
			return true;
		}

		public Boolean CanCreateBoard(User user) {
			return true;
		}
	}
}